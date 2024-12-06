using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ScooterLandProjectOpg.Server.Services
{
	public class KundeScooterService : Repository<KundeScooter>, IKundeScooterRepository
	{
		private readonly ScooterLandContext _context;

		public KundeScooterService(ScooterLandContext context) : base(context)
		{
			_context = context;
		}

		// Retrieve all KundeScooters with their related Kunde
		public async Task<IEnumerable<KundeScooter>> GetScootersWithKundeAsync()
		{
			return await _context.Set<KundeScooter>()
				.Include(ks => ks.Kunde) // Include the related Kunde entity
				.ToListAsync();
		}

		
        // Denne metode er designet til at hente en specifik scooter baseret på dens ScooterId. Den inkluderer også kundeoplysningerne via
        public async Task<KundeScooter> AddScooterAsync(KundeScooter scooter)
        {
            _context.KunderScootere.Add(scooter); 
            await _context.SaveChangesAsync(); 
            return scooter; 
        }
        // Denne metode henter alle scootere relateret til en bestemt kunde baseret på KundeId
        public async Task<List<KundeScooter>> GetScootersByKundeIdAsync(int kundeId)
        {
            return await _context.KunderScootere
                .Where(ks => ks.KundeId == kundeId)
                .ToListAsync();
        }
        public async Task<KundeScooter> GetScooterWithKundeByIdAsync(int id)
        {
            // Implementering af den manglende metode
            return await _context.KunderScootere
                .Include(ks => ks.Kunde) // Hent relateret Kunde-data
                .FirstOrDefaultAsync(ks => ks.ScooterId == id); // Find scooter med det givne id
        }





    }
}
