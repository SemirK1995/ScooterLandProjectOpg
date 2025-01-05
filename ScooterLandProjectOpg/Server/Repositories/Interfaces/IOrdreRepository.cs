using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Repositories.Interfaces
{
    public interface IOrdreRepository
    {
        public Task<IEnumerable<Ordre>> GetAllOrders();
        public Task<Ordre> GetOrderById(int id);
    }
}
