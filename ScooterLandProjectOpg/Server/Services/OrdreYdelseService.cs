using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;
using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Server.Context;
namespace ScooterLandProjectOpg.Server.Services
{
	public class OrdreYdelseService : Repository<OrdreYdelse>, IOrdreYdelseRepository
	{
		private readonly ScooterLandContext _context;

		public OrdreYdelseService(ScooterLandContext context) : base(context)
		{
			_context = context;
		}

		// Retrieve all OrdreYdelse with related Ordre and Ydelse details
		public async Task<IEnumerable<OrdreYdelse>> GetAllWithDetailsAsync()
		{
			return await _context.Set<OrdreYdelse>()
				.Include(o => o.Ordre)  // Include the associated Ordre
				.Include(o => o.Ydelse) // Include the associated Ydelse
				.ToListAsync();
		}

		// Retrieve a specific OrdreYdelse by ID with related Ordre and Ydelse details
		public async Task<OrdreYdelse> GetWithDetailsByIdAsync(int id)
		{
			return await _context.Set<OrdreYdelse>()
				.Include(o => o.Ordre)  // Include the associated Ordre
				.Include(o => o.Ydelse) // Include the associated Ydelse
				.FirstOrDefaultAsync(o => o.OrdreYdelseId == id);
		}
	}
}
