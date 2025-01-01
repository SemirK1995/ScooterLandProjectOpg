using ScooterLandProjectOpg.Server.Context; // Importerer projektets databasekontekst for at muliggøre databaseoperationer.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer interface-kontrakter for repositories.
using ScooterLandProjectOpg.Shared.Models; // Importerer delte modeller, som repræsenterer databasen og dataobjekter.
using Microsoft.EntityFrameworkCore; // Importerer Entity Framework Core til databaseoperationer.
using ScooterLandProjectOpg.Shared.Enum; // Importerer enums til at repræsentere faste værdier som BetalingsMetodeStatus og OrdreStatus.
using ScooterLandProjectOpg.Shared.DTO; // Importerer DTO'er til at overføre data mellem applikationslag.

namespace ScooterLandProjectOpg.Server.Services // Definerer et navneområde for denne service, som hører til Server-delen af applikationen.
{
    // BetalingsService implementerer betalingslogik ved at arve fra Repository og implementere IBetalingRepository.
    public class BetalingsService : Repository<Betaling>, IBetalingRepository

    {
        // Lokalt felt til databasekonteksten, der giver adgang til betalingsdata.
        private readonly ScooterLandContext _context;

        // Lokalt felt til ordre-repositoriet, som bruges til at opdatere ordrestatuser.
        private readonly IOrdreRepository _ordreRepository;

        public BetalingsService(ScooterLandContext context, IOrdreRepository ordreRepository) : base(context)
        {
            // Initialiserer databasekonteksten.
            _context = context;

            // Initialiserer ordre-repositoriet.
            _ordreRepository = ordreRepository;
        }

        // Henter alle betalinger med tilknyttede ordre-, kunde- og lejeaftaledata og opdaterer beløbet baseret på totalpris.
        public async Task<IEnumerable<Betaling>> GetAllAsync()
        {
            // Henter alle betalinger fra databasen.
            var betalinger = await _context.Set<Betaling>()

                // Inkluderer relaterede ordrer.
                .Include(b => b.Ordre)

                // Inkluderer kundeoplysninger via ordren.
                .ThenInclude(o => o.Kunde) 


                // Inkluderer relaterede lejeaftaler, hvis nødvendigt.
                .Include(b => b.Ordre)
                .ThenInclude(o => o.LejeAftale)

                // Konverterer resultatet til en liste.
                .ToListAsync(); 

            foreach (var betaling in betalinger)
            {
                if (betaling.Ordre != null)
                {
                    // Beregner totalprisen for ordren eller sætter den til 0, hvis den er null.
                    var totalPris = betaling.Ordre.TotalPris ?? 0;

                    if (betaling.Ordre.LejeAftale != null)
                    {
                        // Lægger selvrisikoen fra lejeaftalen til totalprisen.
                        totalPris += betaling.Ordre.LejeAftale.Selvrisiko;
                    }

                    // Opdaterer betalingens beløb med den beregnede totalpris.
                    betaling.Beløb = totalPris;
                }
            }

            // Returnerer listen af betalinger med opdaterede beløb.
            return betalinger;
        }

        // Henter en betaling med tilknyttede ordredata baseret på betalings-ID.
        public async Task<Betaling> GetByIdAsync(int id)
        {
            // Henter betaling fra databasen.
            return await _context.Set<Betaling>()

                // Inkluderer den tilknyttede ordre.
                .Include(b => b.Ordre)

                // Finder betalingen baseret på betalings-ID.
                .FirstOrDefaultAsync(b => b.BetalingsId == id);
        }

        // Søger efter betalinger baseret på ID eller kundens navn og returnerer en liste over resultater.
        public async Task<IEnumerable<Betaling>> SearchByQueryAsync(string query)
        {
            // Tjekker, om søgeforespørgslen er numerisk, og forsøger at parse den som et heltal.
            bool isNumeric = int.TryParse(query, out int parsedId);

            // Inkluderer relaterede ordrer og kunder.
            return await _context.Betalinger
                .Include(b => b.Ordre)
                .ThenInclude(o => o.Kunde)

                // Filtrerer betalinger baseret på betalings-ID eller ordre-ID, hvis forespørgslen er numerisk.
                .Where(b =>
                    (isNumeric && (b.BetalingsId == parsedId || b.OrdreId == parsedId)) ||

                    // Filtrerer betalinger baseret på kundens navn, hvis forespørgslen er tekst.
                    (!isNumeric && b.Ordre.Kunde.Navn.Contains(query)))

                // Returnerer resultatet som en liste.
                .ToListAsync();
        }

        // Opdaterer betalingsstatus og betalingsdato for en specifik betaling baseret på betalings-ID.
        public async Task UpdateBetalingsStatusAsync(int betalingsId, BetalingUpdateDto betalingUpdate)
        {
            // Finder betalingen baseret på betalings-ID.
            var betaling = await _context.Betalinger.FindAsync(betalingsId);

            // Kaster en fejl, hvis betalingen ikke findes.
            if (betaling == null)
                throw new KeyNotFoundException($"Betaling med ID {betalingsId} blev ikke fundet.");

            // Opdaterer betalingsstatus.
            betaling.Betalt = betalingUpdate.Betalt;

            if (betalingUpdate.Betalt)
            {
                // Sætter betalingsdatoen til det nuværende tidspunkt.
                betaling.BetalingsDato = DateTime.Now;

                // Henter relateret ordre og dens betalinger.
                var ordre = await _context.Ordrer
                    .Include(o => o.Betalinger)
                    .FirstOrDefaultAsync(o => o.OrdreId == betaling.OrdreId);

                // Kaster en fejl, hvis ordren ikke findes.
                if (ordre == null)
                    throw new KeyNotFoundException($"Ordre med ID {betaling.OrdreId} blev ikke fundet.");

                // Tjekker, om alle betalinger for ordren er betalt.
                var alleBetalingerBetalt = ordre.Betalinger.All(b => b.Betalt);

                if (alleBetalingerBetalt)
                {
                    // Opdaterer ordrestatus til "Betalt", hvis alle betalinger er fuldført.
                    await _ordreRepository.UpdateOrdreStatusAsync(ordre.OrdreId, OrdreStatus.Betalt);
                }
            }
            else
            {
                // Nulstiller betalingsdatoen, hvis status ikke er betalt.
                betaling.BetalingsDato = null;
            }

            // Opdaterer betalingen i databasen.
            _context.Betalinger.Update(betaling);

            // Gemmer ændringerne.
            await _context.SaveChangesAsync();
        }

        // Opdaterer betalingsmetoden for en specifik betaling baseret på betalings-ID.
        public async Task UpdateBetalingsMetodeAsync(int betalingsId, BetalingsMetodeStatus nyMetode)
        {
            // Finder betalingen baseret på betalings-ID.
            var betaling = await _context.Betalinger.FindAsync(betalingsId);

            // Kaster en fejl, hvis betalingen ikke findes.
            if (betaling == null)
                throw new KeyNotFoundException($"Betaling med ID {betalingsId} blev ikke fundet.");

            // Opdaterer betalingsmetoden.
            betaling.BetalingsMetode = nyMetode;

            // Gemmer ændringerne.
            await _context.SaveChangesAsync();
        }

        // Henter detaljeret fakturadata for en specifik betaling, inklusive relaterede ordrer, lejeaftaler, ydelser og produkter.
        public async Task<Betaling> GetFakturaDetaljerAsync(int betalingsId)
        {
            return await _context.Betalinger
                .Include(b => b.Ordre)
                    .ThenInclude(o => o.LejeAftale)
                        .ThenInclude(la => la.LejeScooter)
                .Include(b => b.Ordre)
                    .ThenInclude(o => o.OrdreYdelse)
                        .ThenInclude(oy => oy.Scooter)
                .Include(b => b.Ordre)
                    .ThenInclude(o => o.OrdreYdelse)
                        .ThenInclude(oy => oy.Ydelse)
                .Include(b => b.Ordre)
                    .ThenInclude(o => o.OrdreYdelse)
                        .ThenInclude(oy => oy.Mekaniker) // Tilføj denne linje for mekanikeroplysninger
                .Include(b => b.Ordre)
                    .ThenInclude(o => o.Kunde)
                .Include(b => b.Ordre) // Tilføj produkterne
                    .ThenInclude(o => o.OrdreProdukter)
                        .ThenInclude(op => op.Produkt)
                .FirstOrDefaultAsync(b => b.BetalingsId == betalingsId);
        }

        // Opretter betalinger for ordrer uden eksisterende betalinger og returnerer antallet af oprettede betalinger.
        public async Task<int> OpretBetalingerTilEksisterendeOrdrerAsync()
        {
            // Finder alle ordrer, der ikke har en tilknyttet betaling.
            var ordrerUdenBetaling = await _context.Ordrer
                .Where(o => !_context.Betalinger.Any(b => b.OrdreId == o.OrdreId))
                .ToListAsync();

            foreach (var ordre in ordrerUdenBetaling)
            {
                var betaling = new Betaling
                {
                    // Sætter nuværende tidspunkt som betalingsdato.
                    OrdreId = ordre.OrdreId,
                    BetalingsDato = DateTime.Now,

                    // Bruger ordreens totalpris som beløb.
                    Beløb = ordre.TotalPris,

                    // Ingen betalingsmetode valgt endnu.
                    BetalingsMetode = null,

                    // Marker betalingen som ikke betalt.
                    Betalt = false
                };

                // Tilføjer betalingen til databasen.
                _context.Betalinger.Add(betaling);
            }

            // Gemmer ændringerne.
            await _context.SaveChangesAsync();

            // Returnerer antallet af oprettede betalinger.
            return ordrerUdenBetaling.Count;
        }

        // Opdaterer oplysninger for en eksisterende betaling i databasen og gemmer ændringerne.
        public async Task UpdateAsync(Betaling betaling)
        {
            // Opdaterer betalingen i databasen.
            _context.Betalinger.Update(betaling);

            // Gemmer ændringerne.
            await _context.SaveChangesAsync();
        }
    }
}