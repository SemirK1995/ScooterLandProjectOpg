using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;
using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Shared.Enum;
namespace ScooterLandProjectOpg.Server.Services
{
	public class OrdreService : Repository<Ordre>, IOrdreRepository
	{
		private readonly ScooterLandContext _context;

		public OrdreService(ScooterLandContext context) : base(context)
		{
			_context = context;
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
				.Include(o => o.Kunde)           // Kundeoplysninger
				.Include(o => o.Betalinger)      // Betalinger
				.Include(o => o.OrdreYdelse)
					.ThenInclude(oy => oy.Ydelse) // Tilføj Ydelse-detaljer
				.Include(o => o.LejeAftale)      // Tilføj LejeAftale
					.ThenInclude(la => la.LejeScooter) // Tilføj LejeScooter, hvis relevant
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
            // Hent ordren med relaterede data
            var ordre = await _context.Ordrer
                .Include(o => o.OrdreYdelse) // Inkluder arbejdsopgaver
                .Include(o => o.OrdreProdukter) // Inkluder ordreprodukter
                .Include(o => o.Betalinger) // Inkluder betalinger
                .Include(o => o.LejeAftale) // Inkluder lejeaftale
                .ThenInclude(la => la.LejeScooter) // Inkluder lejescootere
                .FirstOrDefaultAsync(o => o.OrdreId == ordreId);

            if (ordre == null)
                throw new KeyNotFoundException($"Ordre med ID {ordreId} blev ikke fundet.");

            // Opdater ordrestatus
            ordre.Status = nyStatus;

            // Fjern arbejdsopgaver for status Betalt eller Annulleret
            if (nyStatus == OrdreStatus.Betalt || nyStatus == OrdreStatus.Annulleret)
            {
                foreach (var ydelse in ordre.OrdreYdelse)
                {
                    ydelse.MekanikerId = null; // Fjern mekanikertildeling
                }
				if (nyStatus == OrdreStatus.Betalt)
				{
					// Frigiv lejeaftalen
					if (ordre.LejeAftale != null)
					{
						// 1: Frigør scootere i lejeaftalen
						if (ordre.LejeAftale.LejeScooter != null)
						{
							foreach (var scooter in ordre.LejeAftale.LejeScooter)
							{
								scooter.ErTilgængelig = true; // Sæt tilgængelighed til true
								scooter.LejeId = null; // Fjern LejeId fra scooteren
							}
							_context.LejeScootere.UpdateRange(ordre.LejeAftale.LejeScooter);
						}

						ordre.LejeAftale.Status = LejeAftaleStatus.Afsluttet;
						_context.LejeAftaler.Update(ordre.LejeAftale);
					}
				}

				// Hvis status er Annulleret, udfør sletning i den specifikke rækkefølge
				if (nyStatus == OrdreStatus.Annulleret)
                {
                    // 1: Opdater LejeScootere
                    if (ordre.LejeAftale?.LejeScooter != null)
                    {
                        foreach (var scooter in ordre.LejeAftale.LejeScooter)
                        {
                            scooter.ErTilgængelig = true; // Sæt tilgængelighed til true
                            scooter.LejeId = null; // Fjern LejeId
                        }
                        _context.LejeScootere.UpdateRange(ordre.LejeAftale.LejeScooter);
                    }

                    // 2: Slet relaterede data
                    if (ordre.Betalinger != null && ordre.Betalinger.Any())
                        _context.Betalinger.RemoveRange(ordre.Betalinger);

                    if (ordre.OrdreProdukter != null && ordre.OrdreProdukter.Any())
                        _context.OrdreProdukter.RemoveRange(ordre.OrdreProdukter);

                    if (ordre.OrdreYdelse != null && ordre.OrdreYdelse.Any())
                        _context.OrdreYdelser.RemoveRange(ordre.OrdreYdelse);

                    // 3: Fjern LejeId fra ordren
                    ordre.LejeId = null;

                    // 4: Slet lejeaftalen
                    if (ordre.LejeAftale != null)
                    {
                        _context.LejeAftaler.Remove(ordre.LejeAftale);
                    }

                    // 5: Slet selve ordren
                    _context.Ordrer.Remove(ordre);
                }
                else
                {
                    // Hvis status er Betalt, gem ændringer uden at slette ordren
                    _context.Ordrer.Update(ordre);
                }
            }
            else
            {
                // Hvis status ikke er Annulleret eller Betalt, gem kun statusopdateringen
                _context.Ordrer.Update(ordre);
            }

            // Gem ændringer i databasen
            await _context.SaveChangesAsync();
        }
	}
}
