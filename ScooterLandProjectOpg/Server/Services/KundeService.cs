using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Services
{
	public class KundeService : Repository<Kunde>, IKundeRepository
	{
		private readonly ScooterLandContext _context;

		public KundeService(ScooterLandContext context) : base(context)
		{
			_context = context;
		}

		//Henter alle kunder med en ordre.
		public async Task<IEnumerable<Kunde>> GetAllWithOrdersAsync()
		{
			return await _context.Kunder
				.Include(k => k.Ordre) 
				.ToListAsync();
		}

		//Henter en specifikt kunde med de scootere han har.
		public async Task<Kunde> GetKundeWithScootersAsync(int id)
		{
			return await _context.Kunder
				.Include(k => k.KundeScooter)
				.FirstOrDefaultAsync(k => k.KundeId == id);
		}
		
		// Søg efter kunde via navn.
		public async Task<IEnumerable<Kunde>> SearchByNameAsync(string name)
		{
			return await _context.Kunder
				.Where(k => EF.Functions.Like(k.Navn, $"%{name}%")) 
				.ToListAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var kunde = await _context.Kunder.FindAsync(id);
			if (kunde == null)
			{
				throw new Exception($"Kunde med ID {id} blev ikke fundet.");
			}

			_context.Kunder.Remove(kunde); // Cascade delete håndterer relaterede data
			await _context.SaveChangesAsync();
		}
		public new async Task UpdateAsync(Kunde entity)
		{
			await base.UpdateAsync(entity); // Brug af base klassen' UpdateAsync
		}

		public new async Task<Kunde> AddAsync(Kunde entity)
		{
			return await base.AddAsync(entity); // Brug af base klassen' AddAsync
		}

        public async Task<IEnumerable<Ordre>> GetOrdrerForKundeAsync(int kundeId)
        {
            return await _context.Ordrer
                .Where(o => o.KundeId == kundeId)
                 .Include(o => o.Kunde)
                .Include(o => o.OrdreYdelse)
                    .ThenInclude(oy => oy.Ydelse)
                .ToListAsync();
        }
		
		public async Task<Kunde> GetKundeWithManyDetailsByIdAsync(int kundeId)
		{
			return await _context.Kunder
				.Include(k => k.Ordre)
					.ThenInclude(o => o.OrdreYdelse)
						.ThenInclude(oy => oy.Ydelse)
				.Include(k => k.Ordre)
					.ThenInclude(o => o.OrdreProdukter)
						.ThenInclude(op => op.Produkt)
				.Include(k => k.Ordre)
					.ThenInclude(o => o.LejeAftale) // Relater til lejeaftale
						.ThenInclude(la => la.LejeScooter) // Og tilhørende scootere
				.Include(k => k.KundeScooter) // Hvis nødvendigt
				.FirstOrDefaultAsync(k => k.KundeId == kundeId);
		}

		public async Task<IEnumerable<Kunde>> SearchKunderAsync(string søgeTekst)
		{
			if (string.IsNullOrWhiteSpace(søgeTekst))
				return new List<Kunde>();

			var søgning = søgeTekst.Trim().ToLower();

			// Check om søgeteksten er numerisk
			bool isNumeric = int.TryParse(søgning, out int numericValue);

			// Filtrér efter ID, telefonnummer eller navn
			var kunder = await _context.Kunder
				.Where(k =>
					(isNumeric && (k.KundeId == numericValue ||
								   (k.Telefonnummer.HasValue && k.Telefonnummer.Value == numericValue))) ||
					(!isNumeric && k.Navn != null && k.Navn.ToLower().Contains(søgning)))
				.ToListAsync();

			return kunder;
		}
	}
}
