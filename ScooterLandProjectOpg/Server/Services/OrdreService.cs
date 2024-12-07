using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;
using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Shared.Enum;
namespace ScooterLandProjectOpg.Server.Services
{
	public class OrdreService : Repository<Ordre>, IOrdreRepository
	{
		private readonly ScooterLandContext _context;

		public OrdreService(ScooterLandContext context) : base(context)
		{
			_context = context;
		}

		//Henter alle ordre med en kunde, betalinger og ordreydelse detaljer.
		public async Task<IEnumerable<Ordre>> GetAllWithDetailsAsync()
		{
			return await _context.Set<Ordre>()
				.Include(o => o.Kunde)         // Indkluderer Kunde details
				.Include(o => o.Betalinger)   // Inkluderer Betalinger
				.Include(o => o.OrdreYdelse)  // Inlluderer OrdreYdelse
				.ToListAsync();
		}

		//Henter en specifik ordre ud fra id og dens kunde, betalinger og ordreydelse detaljer.
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

        //Opdaterer ordrer status
        public async Task UpdateOrdreStatusAsync(int ordreId, OrdreStatus nyStatus)
        {
            var ordre = await _context.Ordrer.FindAsync(ordreId);
            if (ordre == null)
            {
                throw new KeyNotFoundException($"Ordre med ID {ordreId} blev ikke fundet.");
            }

            ordre.Status = nyStatus;
            await _context.SaveChangesAsync();
        }

        //Tilføj Selvrisiko til en ordrer
        public async Task TilføjSelvrisikoAsync(int ordreId)
        {
            var ordre = await _context.Ordrer.FindAsync(ordreId);
            if (ordre == null)
            {
                throw new KeyNotFoundException($"Ordre med ID {ordreId} blev ikke fundet.");
            }

            ordre.TotalPris += 1000; // Tilføj 1000 kr. for selvrisiko
            await _context.SaveChangesAsync();
        }
		public async Task FjernSelvrisikoAsync(int ordreId)
		{
			var ordre = await _context.Ordrer.FindAsync(ordreId);
			if (ordre == null)
			{
				throw new KeyNotFoundException($"Ordre med ID {ordreId} blev ikke fundet.");
			}

			// Træk selvrisiko fra totalpris, hvis den er tilføjet
			if (ordre.TotalPris != null && ordre.TotalPris >= 1000)
			{
				ordre.TotalPris -= 1000;
			}

			await _context.SaveChangesAsync();
		}
	}
}
