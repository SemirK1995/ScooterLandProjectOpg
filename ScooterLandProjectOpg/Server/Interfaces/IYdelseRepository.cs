using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Interfaces
{
	public interface IYdelseRepository : IRepository<Ydelse>
	{
		Task<IEnumerable<Ydelse>> GetAllWithDetailsAsync();
		Task<Ydelse> GetWithDetailsByIdAsync(int id);
	}
}
