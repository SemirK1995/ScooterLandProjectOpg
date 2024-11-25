using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ScooterLandProjectOpg.Server.Services
{
	public class KundeScooterService : Repository<KundeScooter>, IKundeScooterRepository
	{
		private readonly ScooterLandContext _context;

		public KundeScooterService(ScooterLandContext context) : base(context)
		{
			_context = context;
		}

		// Retrieve all KundeScooters with their related Kunde
		public async Task<IEnumerable<KundeScooter>> GetScootersWithKundeAsync()
		{
			return await _context.Set<KundeScooter>()
				.Include(ks => ks.Kunde) // Include the related Kunde entity
				.ToListAsync();
		}

		// Retrieve a specific KundeScooter with its related Kunde
		public async Task<KundeScooter> GetScooterWithKundeByIdAsync(int id)
		{
			return await _context.Set<KundeScooter>()
				.Include(ks => ks.Kunde) // Include the related Kunde entity
				.FirstOrDefaultAsync(ks => ks.ScooterId == id);
		}

	}
}
