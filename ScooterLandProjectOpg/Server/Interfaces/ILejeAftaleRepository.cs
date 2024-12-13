using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Interfaces
{
	public interface ILejeAftaleRepository : IRepository<LejeAftale>
	{
		Task<IEnumerable<LejeAftale>> GetAllWithKundeAndScootersAsync();
		Task<LejeAftale> GetLejeAftaleWithDetailsAsync(int id);
        Task<IEnumerable<LejeAftale>> SearchLejeAftalerAsync(string query);
        Task <Ordre> UpdateSelvrisikoAsync(int lejeId, double selvrisiko);
        Task<LejeAftale?> UpdateKortKilometerAsync(int lejeId, int? kortKilometer);
    }
}
