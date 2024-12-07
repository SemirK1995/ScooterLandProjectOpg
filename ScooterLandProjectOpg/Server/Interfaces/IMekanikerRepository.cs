using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Interfaces
{
	public interface IMekanikerRepository : IRepository<Mekaniker>
	{
		Task<IEnumerable<Mekaniker>> GetAllAsync(); 
		Task<Mekaniker> GetByIdAsync(int id); 
		Task<Mekaniker> AddAsync(Mekaniker mekaniker); 
		Task UpdateAsync(Mekaniker mekaniker); 
		Task DeleteAsync(int id); 
		Task<IEnumerable<OrdreYdelse>> GetArbejdsopgaverForMekanikerAsync(int mekanikerId); //henter arbejdsopgaver
	}
}
