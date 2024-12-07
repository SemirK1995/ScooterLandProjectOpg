namespace ScooterLandProjectOpg.Server.Interfaces
{
	public interface IRepository<T> where T : class
	{
		//Alle andre repositories arver fra denne:

		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		Task<T> AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(int id);
	}
}
