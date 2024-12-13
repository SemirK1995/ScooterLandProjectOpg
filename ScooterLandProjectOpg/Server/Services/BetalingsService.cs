using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;
using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Shared.Enum;
using ScooterLandProjectOpg.Shared.DTO;


namespace ScooterLandProjectOpg.Server.Services
{
    public class BetalingsService : Repository<Betaling>, IBetalingRepository
    {
        private readonly ScooterLandContext _context;
        private readonly IOrdreRepository _ordreRepository;

        public BetalingsService(ScooterLandContext context, IOrdreRepository ordreRepository) : base(context)
        {
            _context = context;
            _ordreRepository = ordreRepository;
        }
        public async Task<IEnumerable<Betaling>> GetAllAsync()
        {
            var betalinger = await _context.Set<Betaling>()
         .Include(b => b.Ordre)
         .ThenInclude(o => o.Kunde) // Inkluder Kunde via Ordre
         .Include(b => b.Ordre)
         .ThenInclude(o => o.LejeAftale) // Inkluder LejeAftale, hvis nødvendigt
         .ToListAsync();

            foreach (var betaling in betalinger)
            {
                if (betaling.Ordre != null)
                {
                    // Beregn det opdaterede beløb baseret på totalpris inklusive selvrisiko
                    var totalPris = betaling.Ordre.TotalPris ?? 0;

                    // Hvis der er en lejeaftale, tilføj selvrisiko
                    if (betaling.Ordre.LejeAftale != null)
                    {
                        totalPris += betaling.Ordre.LejeAftale.Selvrisiko;
                    }

                    // Opdater betalingens beløb
                    betaling.Beløb = totalPris;
                }
            }

            return betalinger;
        }

        // Custom implementation for GetById with related orders
        public async Task<Betaling> GetByIdAsync(int id)
        {
            return await _context.Set<Betaling>()
                .Include(b => b.Ordre)
                .FirstOrDefaultAsync(b => b.BetalingsId == id);
        }
        public async Task<IEnumerable<Betaling>> SearchByQueryAsync(string query)
        {
            // Parse søgeforespørgslen som tal, hvis muligt
            bool isNumeric = int.TryParse(query, out int parsedId);

            return await _context.Betalinger
                .Include(b => b.Ordre)
                .ThenInclude(o => o.Kunde)
                .Where(b =>
                    (isNumeric && (b.BetalingsId == parsedId || b.OrdreId == parsedId)) || // Søg på betalings/ordre ID
                    (!isNumeric && b.Ordre.Kunde.Navn.Contains(query))) // Søg på kundens navn
                .ToListAsync();
        }

        public async Task UpdateBetalingsStatusAsync(int betalingsId, BetalingUpdateDto betalingUpdate)
        {
            var betaling = await _context.Betalinger.FindAsync(betalingsId);
            if (betaling == null)
                throw new KeyNotFoundException($"Betaling med ID {betalingsId} blev ikke fundet.");

            // Kun sæt betalingsdato, hvis Betalt er true
            betaling.Betalt = betalingUpdate.Betalt;
            if (betalingUpdate.Betalt)
            {
                betaling.BetalingsDato = DateTime.Now;

                // Hent relateret ordre
                var ordre = await _context.Ordrer
                    .Include(o => o.Betalinger)
                    .FirstOrDefaultAsync(o => o.OrdreId == betaling.OrdreId);

                if (ordre == null)
                    throw new KeyNotFoundException($"Ordre med ID {betaling.OrdreId} blev ikke fundet.");

                // Tjek om alle betalinger for ordren er betalt
                var alleBetalingerBetalt = ordre.Betalinger.All(b => b.Betalt);
                if (alleBetalingerBetalt)
                {
                    // Opdater ordrestatus til "Betalt"
                    await _ordreRepository.UpdateOrdreStatusAsync(ordre.OrdreId, OrdreStatus.Betalt);
                }
            }
            else
            {
                betaling.BetalingsDato = null; // Slet dato, hvis ikke betalt
            }

            _context.Betalinger.Update(betaling);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBetalingsMetodeAsync(int betalingsId, BetalingsMetodeStatus nyMetode)
        {
            var betaling = await _context.Betalinger.FindAsync(betalingsId);
            if (betaling == null)
                throw new KeyNotFoundException($"Betaling med ID {betalingsId} blev ikke fundet.");

            betaling.BetalingsMetode = nyMetode;
            await _context.SaveChangesAsync();
        }
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
		public async Task<int> OpretBetalingerTilEksisterendeOrdrerAsync()
        {
            var ordrerUdenBetaling = await _context.Ordrer
                .Where(o => !_context.Betalinger.Any(b => b.OrdreId == o.OrdreId))
                .ToListAsync();

            foreach (var ordre in ordrerUdenBetaling)
            {
                var betaling = new Betaling
                {
                    OrdreId = ordre.OrdreId,
                    BetalingsDato = DateTime.Now, // Brug nuværende tidspunkt som betalingsdato
                    Beløb = ordre.TotalPris, // Brug ordreens totalpris som beløb
                    BetalingsMetode = null, // Ingen betalingsmetode valgt endnu
                    Betalt = false // Markér som ikke betalt
                };

                _context.Betalinger.Add(betaling);
            }

            await _context.SaveChangesAsync();

            return ordrerUdenBetaling.Count;
        }
        public async Task UpdateAsync(Betaling betaling)
        {
            _context.Betalinger.Update(betaling);
            await _context.SaveChangesAsync();
        }
    }
}
