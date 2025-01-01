using ScooterLandProjectOpg.Server.Interfaces; // Importerer interfaces til OrdreService.
using ScooterLandProjectOpg.Shared.Models; // Importerer modelklasser som Ordre.
using Microsoft.EntityFrameworkCore; // Importerer Entity Framework Core til databaseinteraktion.
using ScooterLandProjectOpg.Server.Context; // Importerer databasekonteksten til interaktion med databasen.
using ScooterLandProjectOpg.Shared.Enum; // Importerer enums som OrdreStatus.

namespace ScooterLandProjectOpg.Server.Services // Definerer namespace for tjenestelogik.
{
    // Implementerer IOrdreRepository og arver fra Repository<Ordre>.
    public class OrdreService : Repository<Ordre>, IOrdreRepository 
    {
        // Felt til at holde databasekonteksten.
        private readonly ScooterLandContext _context; 

        // Constructor, der initialiserer databasekonteksten og sender den til basisklassen.
        public OrdreService(ScooterLandContext context) : base(context) 
        {
            _context = context; // Initialiserer lokal databasekontekst.
        }

		//Henter alle ordre med en kunde, betalinger og ordreydelse detaljer.
		public async Task<IEnumerable<Ordre>> GetAllWithDetailsAsync()
		{
			return await _context.Set<Ordre>()
				.Include(o => o.Kunde)         // Indkluderer Kunde details
				.Include(o => o.Betalinger)   // Inkluderer Betalinger
				.Include(o => o.OrdreYdelse)  // Inlluderer OrdreYdelse
				.ToListAsync();
		}
		public async Task<Ordre> GetWithDetailsByIdAsync(int id)
		{
			return await _context.Ordrer
				.Include(o => o.Kunde)             // Kundeoplysninger
				.Include(o => o.Betalinger)        // Betalinger
				.Include(o => o.OrdreYdelse)
					.ThenInclude(oy => oy.Ydelse)  // Tilføj Ydelse-detaljer
				.Include(o => o.OrdreYdelse)
					.ThenInclude(oy => oy.Scooter) // Inkluder Scooter-detaljer for Ydelser
				.Include(o => o.LejeAftale)
					.ThenInclude(la => la.LejeScooter)
				.Include(o => o.OrdreProdukter)
					.ThenInclude(op => op.Produkt)
				.FirstOrDefaultAsync(o => o.OrdreId == id);
		}
		public async Task<Ordre> AddAsync(Ordre ordre)
		{
			_context.Ordrer.Add(ordre);
			await _context.SaveChangesAsync();
			return ordre;
		}
        public async Task UpdateOrdreStatusAsync(int ordreId, OrdreStatus nyStatus)
        {
            var ordre = await _context.Ordrer // Finder ordren baseret på ID.
                .Include(o => o.OrdreYdelse) // Inkluder arbejdsopgaver relateret til ordren.
                .Include(o => o.OrdreProdukter) // Inkluder produkter relateret til ordren.
                .Include(o => o.Betalinger) // Inkluder betalinger relateret til ordren.
                .Include(o => o.LejeAftale) // Inkluder lejeaftaleoplysninger.
                .ThenInclude(la => la.LejeScooter) // Inkluder scootere relateret til lejeaftalen.
                .FirstOrDefaultAsync(o => o.OrdreId == ordreId); // Finder den første ordre, der matcher ID.

            if (ordre == null) // Tjekker, om ordren blev fundet.
                throw new KeyNotFoundException($"Ordre med ID {ordreId} blev ikke fundet."); // Smider en undtagelse, hvis ikke fundet.

            ordre.Status = nyStatus; // Opdaterer ordrestatus.

            if (nyStatus == OrdreStatus.Betalt || nyStatus == OrdreStatus.Annulleret) // Hvis status er Betalt eller Annulleret.
            {
                foreach (var ydelse in ordre.OrdreYdelse) // Itererer gennem ordreydelser.
                {
                    ydelse.MekanikerId = null; // Fjerner mekanikeropgaven.
                }

                if (nyStatus == OrdreStatus.Betalt) // Hvis status er Betalt.
                {
                    if (ordre.LejeAftale != null) // Hvis ordren har en lejeaftale.
                    {
                        if (ordre.LejeAftale.LejeScooter != null) // Hvis lejeaftalen har scootere.
                        {
                            foreach (var scooter in ordre.LejeAftale.LejeScooter) // Itererer gennem scootere.
                            {
                                scooter.ErTilgængelig = true; // Marker scooteren som tilgængelig.
                                scooter.LejeId = null; // Fjerner leje-ID fra scooteren.
                            }
                            _context.LejeScootere.UpdateRange(ordre.LejeAftale.LejeScooter); // Opdaterer scootere i databasen.
                        }

                        ordre.LejeAftale.Status = LejeAftaleStatus.Afsluttet; // Marker lejeaftalen som afsluttet.
                        _context.LejeAftaler.Update(ordre.LejeAftale); // Opdaterer lejeaftalen.
                    }
                }

                if (nyStatus == OrdreStatus.Annulleret) // Hvis status er Annulleret.
                {
                    if (ordre.LejeAftale?.LejeScooter != null) // Hvis lejeaftalen har scootere.
                    {
                        foreach (var scooter in ordre.LejeAftale.LejeScooter) // Itererer gennem scootere.
                        {
                            scooter.ErTilgængelig = true; // Marker scooteren som tilgængelig.
                            scooter.LejeId = null; // Fjerner leje-ID fra scooteren.
                        }
                        _context.LejeScootere.UpdateRange(ordre.LejeAftale.LejeScooter); // Opdaterer scootere i databasen.
                    }

                    if (ordre.Betalinger != null && ordre.Betalinger.Any()) // Hvis der er betalinger relateret til ordren.
                        _context.Betalinger.RemoveRange(ordre.Betalinger); // Fjern betalinger.

                    if (ordre.OrdreProdukter != null && ordre.OrdreProdukter.Any()) // Hvis der er produkter relateret til ordren.
                        _context.OrdreProdukter.RemoveRange(ordre.OrdreProdukter); // Fjern produkter.

                    if (ordre.OrdreYdelse != null && ordre.OrdreYdelse.Any()) // Hvis der er arbejdsopgaver relateret til ordren.
                        _context.OrdreYdelser.RemoveRange(ordre.OrdreYdelse); // Fjern arbejdsopgaver.

                    ordre.LejeId = null; // Fjern leje-ID fra ordren.

                    if (ordre.LejeAftale != null) // Hvis ordren har en lejeaftale.
                        _context.LejeAftaler.Remove(ordre.LejeAftale); // Fjern lejeaftalen.

                    _context.Ordrer.Remove(ordre); // Fjern ordren.
                }
                else
                {
                    _context.Ordrer.Update(ordre); // Opdater ordren i databasen.
                }
            }
            else
            {
                _context.Ordrer.Update(ordre); // Opdater kun ordren i databasen.
            }

            await _context.SaveChangesAsync(); // Gemmer ændringer i databasen.
        }
    }
}