using ScooterLandProjectOpg.Shared.DTO;
using ScooterLandProjectOpg.Shared.Enum;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Interfaces
{
	public interface IBetalingRepository : IRepository<Betaling>
	{
        Task<IEnumerable<Betaling>> SearchByQueryAsync(string query);

        //Metode der kan opdatere betalingsstatus: Om den er betalt: "Ja"/"Nej"
        Task UpdateBetalingsStatusAsync(int betalingsId, BetalingUpdateDto betalingUpdate);

        //Metode der opdater om en kunde har betalt: F.eks. Kontant eller Mobilepay
        Task UpdateBetalingsMetodeAsync(int betalingsId, BetalingsMetodeStatus nyMetode);

        Task<Betaling> GetFakturaDetaljerAsync(int betalingsId);

        Task<int> OpretBetalingerTilEksisterendeOrdrerAsync();
    }
}
