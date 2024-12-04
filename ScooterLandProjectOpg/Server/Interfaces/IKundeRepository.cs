using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Interfaces
{
	public interface IKundeRepository : IRepository<Kunde>
	{
		// Specifikke metoder til at hente Kunde med relaterede data
		Task<IEnumerable<Kunde>> GetAllWithOrdersAsync();
		Task<Kunde> GetKundeWithScootersAsync(int id);
		Task<IEnumerable<Kunde>> SearchByNameAsync(string name);

		//En specifik metode til at hente kunder med ordre
        Task<IEnumerable<Ordre>> GetOrdrerForKundeAsync(int kundeId);

        Task<Kunde> GetKundeWithManyDetailsByIdAsync(int kundeId);
		Task<IEnumerable<Kunde>> SearchKunderAsync(string søgeTekst);

	}
}
