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

		//Hent alle OrdreYdelser med en ordre og detaljer omkring ydelsen.
		public async Task<IEnumerable<OrdreYdelse>> GetAllWithDetailsAsync()
		{
			return await _context.Set<OrdreYdelse>()
				.Include(o => o.Ordre)  // Include the associated Ordre
				.Include(o => o.Ydelse) // Include the associated Ydelse
				.Include(o =>o.Scooter)
				.ThenInclude(s => s.Kunde)
				.ToListAsync();
		}

        //Hent en OrdreYdelse med en ordre og detaljer omkring ydelsen.
        public async Task<OrdreYdelse> GetWithDetailsByIdAsync(int id)
		{
			return await _context.Set<OrdreYdelse>()
				.Include(o => o.Ordre)  // Include the associated Ordre
				.Include(o => o.Ydelse) // Include the associated Ydelse
				.FirstOrDefaultAsync(o => o.OrdreYdelseId == id);
		}
	}
}
