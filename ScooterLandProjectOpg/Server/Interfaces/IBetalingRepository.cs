using ScooterLandProjectOpg.Shared.Enum;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Interfaces
{
	public interface IBetalingRepository : IRepository<Betaling>
	{
        Task<IEnumerable<Betaling>> SearchByQueryAsync(string query);
        Task UpdateBetalingsStatusAsync(int betalingsId, bool betaltStatus);
        Task UpdateBetalingsMetodeAsync(int betalingsId, BetalingsMetodeStatus nyMetode);
        Task<Betaling> GetFakturaDetaljerAsync(int betalingsId);

        Task<int> OpretBetalingerTilEksisterendeOrdrerAsync();

    }
}
