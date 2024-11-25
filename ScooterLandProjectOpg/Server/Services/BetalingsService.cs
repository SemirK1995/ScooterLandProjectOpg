using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;
using Microsoft.EntityFrameworkCore;


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
	}
}
