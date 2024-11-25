using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ScooterLandProjectOpg.Server.Services
{
	public class MekanikerService : Repository<Mekaniker>, IMekanikerRepository
	{
		private readonly ScooterLandContext _context;

		public MekanikerService(ScooterLandContext context) : base(context) 
		{
			_context = context;
		}

		// Hent alle mekanikere
		public async Task<IEnumerable<Mekaniker>> GetAllAsync()
		{
			return await _context.Mekanikere.ToListAsync();
		}

		// Hent mekaniker ved id
		public async Task<Mekaniker> GetByIdAsync(int id)
		{
			return await _context.Mekanikere
				.FirstOrDefaultAsync(m => m.MekanikerId == id);
		}

		// Hent alle mekanikere med deres ydelser
		public async Task<IEnumerable<Mekaniker>> GetAllWithYdelserAsync()
		{
			return await _context.Mekanikere
				.Include(m => m.MekanikerYdelse) // Inkluder relaterede ydelser
				.ToListAsync();
		}

		// Tilføj ny mekaniker
		public async Task<Mekaniker> AddAsync(Mekaniker mekaniker)
		{
			_context.Mekanikere.Add(mekaniker);
			await _context.SaveChangesAsync();
			return mekaniker; // Returnér den tilføjede mekaniker
		}

		// Opdater eksisterende mekaniker
		public async Task UpdateAsync(Mekaniker mekaniker)
		{
			_context.Mekanikere.Update(mekaniker);
			await _context.SaveChangesAsync();
		}

		// Slet mekaniker
		public async Task DeleteAsync(int id)
		{
			var mekaniker = await _context.Mekanikere.FindAsync(id);
			if (mekaniker != null)
			{
				_context.Mekanikere.Remove(mekaniker);
				await _context.SaveChangesAsync();
			}
		}
	}
}

