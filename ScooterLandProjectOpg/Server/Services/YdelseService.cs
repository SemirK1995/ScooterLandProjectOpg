using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;
using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Server.Context;
namespace ScooterLandProjectOpg.Server.Services
{
	public class YdelseService : Repository<Ydelse>, IYdelseRepository
	{
		private readonly ScooterLandContext _context;

		public YdelseService(ScooterLandContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Ydelse>> GetAllWithDetailsAsync()
		{
			// Henter alle Ydelser fra databasen
			return await _context.Ydelser.ToListAsync();
		}

		public async Task<Ydelse> GetWithDetailsByIdAsync(int id)
		{
			// Henter en Ydelse ved hjælp af dens ID
			return await _context.Ydelser.FindAsync(id);
		}

		public async Task<Ydelse> AddAsync(Ydelse entity)
		{
			// Tilføjer en ny Ydelse til databasen
			await _context.Ydelser.AddAsync(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task UpdateAsync(Ydelse entity)
		{
			// Opdaterer en eksisterende Ydelse i databasen
			_context.Ydelser.Update(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			// Henter og sletter en Ydelse baseret på ID
			var ydelse = await _context.Ydelser.FindAsync(id);
			if (ydelse != null)
			{
				_context.Ydelser.Remove(ydelse);
				await _context.SaveChangesAsync();
			}
		}
	}
}
