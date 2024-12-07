using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Interfaces
{
	public interface ILejeAftaleRepository : IRepository<LejeAftale>
	{
		Task<IEnumerable<LejeAftale>> GetAllWithKundeAndScootersAsync();
		Task<LejeAftale> GetLejeAftaleWithDetailsAsync(int id);
	}
}
