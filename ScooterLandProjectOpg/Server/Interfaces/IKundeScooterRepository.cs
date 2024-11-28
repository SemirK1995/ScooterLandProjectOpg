using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Interfaces
{
	public interface IKundeScooterRepository : IRepository<KundeScooter>
	{
		Task<IEnumerable<KundeScooter>> GetScootersWithKundeAsync();
		Task<KundeScooter> GetScooterWithKundeByIdAsync(int id);

        Task<KundeScooter> AddScooterAsync(KundeScooter scooter);

		Task<List<KundeScooter>> GetScootersByKundeIdAsync(int kundeId);
    }
}
