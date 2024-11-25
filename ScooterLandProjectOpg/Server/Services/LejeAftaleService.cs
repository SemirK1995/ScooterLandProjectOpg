using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ScooterLandProjectOpg.Server.Services
{
	public class LejeAftaleService : Repository<LejeAftale>, ILejeAftaleRepository
	{
		private readonly ScooterLandContext _context;

		public LejeAftaleService(ScooterLandContext context) : base(context)
		{
			_context = context;
		}

		// Retrieve all LejeAftale entities with related Kunde and LejeScooter data
		public async Task<IEnumerable<LejeAftale>> GetAllWithKundeAndScootersAsync()
		{
			return await _context.Set<LejeAftale>()
				.Include(la => la.Kunde) // Include related Kunde
				.Include(la => la.LejeScooter) // Include related LejeScooter list
				.ToListAsync();
		}

		// Retrieve a specific LejeAftale with related Kunde and LejeScooter details
		public async Task<LejeAftale> GetLejeAftaleWithDetailsAsync(int id)
		{
			return await _context.Set<LejeAftale>()
				.Include(la => la.Kunde) // Include related Kunde
				.Include(la => la.LejeScooter) // Include related LejeScooter list
				.FirstOrDefaultAsync(la => la.LejeId == id);
		}
	}
}

