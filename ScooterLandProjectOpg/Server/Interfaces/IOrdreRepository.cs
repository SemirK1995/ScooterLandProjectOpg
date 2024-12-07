using ScooterLandProjectOpg.Shared.Enum;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Interfaces
{
	public interface IOrdreRepository : IRepository<Ordre>
	{
		Task<IEnumerable<Ordre>> GetAllWithDetailsAsync();
		Task<Ordre> GetWithDetailsByIdAsync(int id);
        Task UpdateOrdreStatusAsync(int ordreId, OrdreStatus nyStatus);

		//To metoder der fjerne og tilføjer selvrisiko
        Task TilføjSelvrisikoAsync(int ordreId);
		Task FjernSelvrisikoAsync(int ordreId); 
	}
}
