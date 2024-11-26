using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;
using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Server.Context;
namespace ScooterLandProjectOpg.Server.Services
{
	public class OrdreService : Repository<Ordre>, IOrdreRepository
	{
		private readonly ScooterLandContext _context;

		public OrdreService(ScooterLandContext context) : base(context)
		{
			_context = context;
		}

		// Retrieve all orders with related Kunde, Betalinger, and OrdreYdelse details
		public async Task<IEnumerable<Ordre>> GetAllWithDetailsAsync()
		{
			return await _context.Set<Ordre>()
				.Include(o => o.Kunde)         // Include Kunde details
				.Include(o => o.Betalinger)   // Include associated Betalinger
				.Include(o => o.OrdreYdelse)  // Include associated OrdreYdelse
				.ToListAsync();
		}

		// Retrieve a specific order by ID with related Kunde, Betalinger, and OrdreYdelse details
		public async Task<Ordre> GetWithDetailsByIdAsync(int id)
		{
			return await _context.Ordrer
				.Include(o => o.Kunde)           // Kundeoplysninger
				.Include(o => o.Betalinger)      // Betalinger
				.Include(o => o.OrdreYdelse)
					.ThenInclude(oy => oy.Ydelse) // Tilføj Ydelse-detaljer
				.FirstOrDefaultAsync(o => o.OrdreId == id);
		}

		public async Task<Ordre> AddAsync(Ordre ordre)
		{
			_context.Ordrer.Add(ordre);
			await _context.SaveChangesAsync();
			return ordre;
		}

	}
}
