using ScooterLandProjectOpg.Shared.Models;
namespace ScooterLandProjectOpg.Server.Interfaces
{
	public interface IProduktRepository : IRepository<Produkt>
	{
		Task<IEnumerable<Produkt>> GetAllAsync();
		Task<Produkt> GetByIdAsync(int id);
		Task<Produkt> AddAsync(Produkt produkt);
		Task<Produkt> UpdateAsync(Produkt produkt);
		Task DeleteAsync(int id);
	}
}
