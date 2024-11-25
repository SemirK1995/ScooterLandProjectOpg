using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;
using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Server.Context;
namespace ScooterLandProjectOpg.Server.Services
{
	public class LejeScooterService : Repository<LejeScooter>, ILejeScooterRepository
	{
		private readonly ScooterLandContext _context;

		public LejeScooterService(ScooterLandContext context) : base(context)
		{
			_context = context;
		}
		// Retrieve all LejeScooter entities with related LejeAftale data
		public async Task<IEnumerable<LejeScooter>> GetAllWithLejeAftaleAsync()
		{
			return await _context.Set<LejeScooter>()
				.Include(ls => ls.LejeAftale) // Include related LejeAftale
				.ToListAsync();
		}

		// Retrieve a specific LejeScooter with related LejeAftale details
		public async Task<LejeScooter> GetLejeScooterWithDetailsAsync(int id)
		{
			return await _context.Set<LejeScooter>()
				.Include(ls => ls.LejeAftale) // Include related LejeAftale
				.FirstOrDefaultAsync(ls => ls.LejeScooterId == id);
		}
	}
}
