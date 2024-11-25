using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Interfaces
{
	public interface ILejeScooterRepository : IRepository<LejeScooter>
	{
		Task<IEnumerable<LejeScooter>> GetAllWithLejeAftaleAsync();
		Task<LejeScooter> GetLejeScooterWithDetailsAsync(int id);
	}
}
