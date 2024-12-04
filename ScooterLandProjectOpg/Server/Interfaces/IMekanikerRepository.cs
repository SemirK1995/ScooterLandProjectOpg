using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Interfaces
{
	public interface IMekanikerRepository : IRepository<Mekaniker>
	{
		Task<IEnumerable<Mekaniker>> GetAllAsync(); // Hent alle mekanikere
		Task<Mekaniker> GetByIdAsync(int id); // Hent mekaniker ved id
		Task<Mekaniker> AddAsync(Mekaniker mekaniker); // Tilføj ny mekaniker
		Task UpdateAsync(Mekaniker mekaniker); // Opdater eksisterende mekaniker
		Task DeleteAsync(int id); // Slet mekaniker
		Task<IEnumerable<OrdreYdelse>> GetArbejdsopgaverForMekanikerAsync(int mekanikerId); //henter arbejdsopgaver
	}
}
