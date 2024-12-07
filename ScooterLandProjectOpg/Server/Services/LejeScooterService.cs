using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Services
{
    public class LejeScooterService : Repository<LejeScooter>, ILejeScooterRepository
    {
        private readonly ScooterLandContext _context;

        public LejeScooterService(ScooterLandContext context) : base(context)
        {
            _context = context;
        }
        // Metode til at finde ledige scootere (LejeId = null)
        public async Task<IEnumerable<LejeScooter>> GetScootersAvailableAsync()
        {
            return await _context.LejeScootere
                .Where(scooter => scooter.LejeId == null) // Find scootere uden LejeId
                .ToListAsync();
        }

        public async Task UpdateScooterLejeIdAsync(int scooterId, int lejeId)
        {
            var scooter = await _context.LejeScootere.FindAsync(scooterId);
            if (scooter == null)
            {
                throw new ArgumentException($"Scooter with ID {scooterId} does not exist.");
            }

            // Opdater LejeId og tilgængelighed
            scooter.LejeId = lejeId;
            scooter.ErTilgængelig = false;

            _context.LejeScootere.Update(scooter);
            await _context.SaveChangesAsync();
        }
    }
}
