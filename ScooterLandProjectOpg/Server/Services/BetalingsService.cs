using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;
using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Shared.Enum;


namespace ScooterLandProjectOpg.Server.Services
{
	public class BetalingsService : Repository<Betaling>, IBetalingRepository
	{
		private readonly ScooterLandContext _context;

		public BetalingsService(ScooterLandContext context) : base(context)
		{
			_context = context;
		}
		// Override to include related orders
		public async Task<IEnumerable<Betaling>> GetAllAsync()
		{
			return await _context.Set<Betaling>()
				.Include(b => b.Ordre)
				.ToListAsync();
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

        public async Task UpdateBetalingsStatusAsync(int betalingsId, bool betaltStatus)
        {
            var betaling = await _context.Betalinger.FindAsync(betalingsId);
            if (betaling == null)
                throw new KeyNotFoundException($"Betaling med ID {betalingsId} blev ikke fundet.");

            betaling.Betalt = betaltStatus;
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
        //public async Task<Betaling> GetFakturaDetaljerAsync(int betalingsId)
        //{
        //    return await _context.Betalinger
        //        .Include(b => b.Ordre)
        //            .ThenInclude(o => o.LejeAftale)
        //            .ThenInclude(la => la.LejeScooter)
        //        .Include(b => b.Ordre)
        //            .ThenInclude(o => o.OrdreYdelse)
        //            .ThenInclude(oy => oy.Scooter)
        //        .Include(b => b.Ordre)
        //            .ThenInclude(o => o.OrdreYdelse)
        //            .ThenInclude(oy => oy.Ydelse)
        //        .Include(b => b.Ordre)
        //            .ThenInclude(o => o.Kunde)
        //        .FirstOrDefaultAsync(b => b.BetalingsId == betalingsId);
        //}
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
