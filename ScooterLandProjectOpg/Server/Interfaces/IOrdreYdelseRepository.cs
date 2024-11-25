using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Interfaces
{
	public interface IOrdreYdelseRepository : IRepository<OrdreYdelse>
	{
		Task<IEnumerable<OrdreYdelse>> GetAllWithDetailsAsync();
		Task<OrdreYdelse> GetWithDetailsByIdAsync(int id);
	}
}
