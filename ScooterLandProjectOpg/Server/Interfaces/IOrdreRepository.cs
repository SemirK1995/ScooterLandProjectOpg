using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Interfaces
{
	public interface IOrdreRepository : IRepository<Ordre>
	{
		Task<IEnumerable<Ordre>> GetAllWithDetailsAsync();
		Task<Ordre> GetWithDetailsByIdAsync(int id);
	}
}
