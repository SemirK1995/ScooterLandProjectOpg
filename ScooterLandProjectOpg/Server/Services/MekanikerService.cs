using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;
using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Shared.Enum;

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
		//Hent arbejdsopgaver for en mekaniker
		//public async Task<IEnumerable<OrdreYdelse>> GetArbejdsopgaverForMekanikerAsync(int mekanikerId)
		//{
		//	return await _context.OrdreYdelser
		//		.Include(oy => oy.Ydelse) // Inkluderer oplysninger om ydelsen
		//		.Include(oy => oy.Scooter) // Inkluderer oplysninger om kundens scooter
		//		.Where(oy => oy.MekanikerId == mekanikerId)
		//		.ToListAsync();
		//}
		// Hent kun aktive arbejdsopgaver for en mekaniker
		public async Task<IEnumerable<OrdreYdelse>> GetAktiveArbejdsopgaverForMekanikerAsync(int mekanikerId)
		{
			return await _context.OrdreYdelser
				.Include(oy => oy.Ydelse) // Inkluder oplysninger om ydelsen
				.Include(oy => oy.Scooter) // Inkluder oplysninger om scooteren
				.Include(oy => oy.Ordre) // Inkluder relateret ordre for at filtrere status
				.Where(oy => oy.MekanikerId == mekanikerId &&
							 oy.Ordre.Status != OrdreStatus.Afsluttet &&
							 oy.Ordre.Status != OrdreStatus.Betalt &&
							 oy.Ordre.Status != OrdreStatus.Annulleret)
				.ToListAsync();
		}

	}
}

