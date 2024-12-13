using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ScooterLandProjectOpg.Server.Services
{
	public class LejeAftaleService : Repository<LejeAftale>, ILejeAftaleRepository
	{
		private readonly ScooterLandContext _context;

		public LejeAftaleService(ScooterLandContext context) : base(context)
		{
			_context = context;
		}

		//Henter alt omkring lejeaftaler med kunder og deres scooter som de har lejet.
		public async Task<IEnumerable<LejeAftale>> GetAllWithKundeAndScootersAsync()
		{
			return await _context.Set<LejeAftale>()
				.Include(la => la.Kunde)
				.Include(la => la.LejeScooter)
				.ToListAsync();
		}

		//Henter alt omkring en lejeaftale med en kunde og hans scooter som han har lejet.
		public async Task<LejeAftale> GetLejeAftaleWithDetailsAsync(int id)
		{
			return await _context.Set<LejeAftale>()
				.Include(la => la.Kunde)
				.Include(la => la.LejeScooter)
				.FirstOrDefaultAsync(la => la.LejeId == id);
		}
		public async Task<IEnumerable<LejeAftale>> SearchLejeAftalerAsync(string query)
		{
			return await _context.Set<LejeAftale>()
				.Include(la => la.Kunde)
				.Where(la => la.Kunde.Navn.Contains(query) ||
							 la.KundeId.ToString().Contains(query) ||
							 la.LejeId.ToString().Contains(query))
				.ToListAsync();
		}
		
		public async Task<Ordre> UpdateSelvrisikoAsync(int lejeId, double selvrisiko)
		{
			var lejeAftale = await _context.Set<LejeAftale>()
				.Include(la => la.Ordre)
				.FirstOrDefaultAsync(la => la.LejeId == lejeId);

			if (lejeAftale == null)
				throw new KeyNotFoundException($"Lejeaftale med ID {lejeId} blev ikke fundet.");

			lejeAftale.Selvrisiko = selvrisiko;

			// Gem ændringer
			_context.Update(lejeAftale);
			await _context.SaveChangesAsync();

			// Returnér opdateret ordre
			return await _context.Set<Ordre>()
				.Include(o => o.LejeAftale)
				.Include(o => o.OrdreProdukter)
				.FirstOrDefaultAsync(o => o.OrdreId == lejeAftale.Ordre.OrdreId);
		}

		public async Task<LejeAftale?> UpdateKortKilometerAsync(int lejeId, int? kortKilometer)
		{
			// Hent LejeAftale med relaterede data, hvis nødvendigt
			var lejeAftale = await _context.LejeAftaler
				.Include(la => la.Ordre) // Indlæs den relaterede ordre
				.FirstOrDefaultAsync(la => la.LejeId == lejeId);

			if (lejeAftale == null)
			{
				throw new KeyNotFoundException($"Lejeaftale med ID {lejeId} blev ikke fundet.");
			}

			// Opdater KortKilometer
			lejeAftale.KortKilometer = kortKilometer;

			// Hvis der er en relateret ordre, opdater dens TotalPris
			if (lejeAftale.Ordre != null)
			{
				lejeAftale.Ordre.TotalPris = lejeAftale.TotalPris; // Genberegn totalprisen
			}

			// Gem ændringer
			_context.LejeAftaler.Update(lejeAftale);
			await _context.SaveChangesAsync();
			return lejeAftale;
		}
	}
}

