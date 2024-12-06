using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Services
{
	public class ProduktService : Repository<Produkt>, IProduktRepository
	{
		private readonly ScooterLandContext _context;

		public ProduktService (ScooterLandContext context) : base (context)
		{ 
			_context = context;
		}
		// Hent alle produkter
		public async Task<IEnumerable<Produkt>> GetAllAsync()
		{
			return await _context.Set<Produkt>().ToListAsync();
		}

		// Hent produkt efter ID
		public async Task<Produkt> GetByIdAsync(int id)
		{
			return await _context.Set<Produkt>().FindAsync(id);
		}

		// Opret et nyt produkt
		public async Task<Produkt> AddAsync(Produkt produkt)
		{
			var createdEntity = await _context.Set<Produkt>().AddAsync(produkt);
			await _context.SaveChangesAsync();
			return createdEntity.Entity;
		}

		// Opdater et eksisterende produkt
		public async Task<Produkt> UpdateAsync(Produkt produkt)
		{
			var existingEntity = await _context.Set<Produkt>().FindAsync(produkt.ProduktId);
			if (existingEntity == null)
			{
				throw new KeyNotFoundException($"Produkt med ID {produkt.ProduktId} blev ikke fundet.");
			}

			_context.Entry(existingEntity).CurrentValues.SetValues(produkt);
			await _context.SaveChangesAsync();
			return existingEntity;
		}

		// Slet et produkt
		public async Task DeleteAsync(int id)
		{
			var entity = await _context.Set<Produkt>().FindAsync(id);
			if (entity == null)
			{
				throw new KeyNotFoundException($"Produkt med ID {id} blev ikke fundet.");
			}

			_context.Set<Produkt>().Remove(entity);
			await _context.SaveChangesAsync();
		}
	}
}
