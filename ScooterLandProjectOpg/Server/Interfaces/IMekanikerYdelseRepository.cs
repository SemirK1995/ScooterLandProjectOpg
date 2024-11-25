using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Interfaces
{
	public interface IMekanikerYdelseRepository : IRepository<MekanikerYdelse>
	{
		Task<IEnumerable<MekanikerYdelse>> GetAllWithDetailsAsync();
		Task<MekanikerYdelse> GetWithDetailsByIdAsync(int id);
	}
}
