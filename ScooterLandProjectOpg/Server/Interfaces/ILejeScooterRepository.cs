using ScooterLandProjectOpg.Server.Services;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Interfaces
{
    public interface ILejeScooterRepository : IRepository<LejeScooter>
    {
        // Metode til at finde alle scootere med LejeId = null
        Task<IEnumerable<LejeScooter>> GetScootersAvailableAsync();

        Task UpdateScooterLejeIdAsync(int scooterId, int lejeId);
    }
}
