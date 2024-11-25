using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;
using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Server.Context;

namespace ScooterLandProjectOpg.Server.Services
{
	public class MekanikerYdelseService : Repository<MekanikerYdelse>, IMekanikerYdelseRepository
	{
		private readonly ScooterLandContext _context;

		public MekanikerYdelseService(ScooterLandContext context) : base(context)
		{
			_context = context;
		}

		// Retrieve all MekanikerYdelse entities with related Mekaniker and Ydelse details
		public async Task<IEnumerable<MekanikerYdelse>> GetAllWithDetailsAsync()
		{
			return await _context.Set<MekanikerYdelse>()
				.Include(my => my.Mekaniker) // Include related Mekaniker
				.Include(my => my.Ydelse)   // Include related Ydelse
				.ToListAsync();
		}

		// Retrieve a specific MekanikerYdelse by ID with related Mekaniker and Ydelse details
		public async Task<MekanikerYdelse> GetWithDetailsByIdAsync(int id)
		{
			return await _context.Set<MekanikerYdelse>()
				.Include(my => my.Mekaniker) // Include related Mekaniker
				.Include(my => my.Ydelse)   // Include related Ydelse
				.FirstOrDefaultAsync(my => my.MekanikerYdelseId == id);
		}
	}
}
