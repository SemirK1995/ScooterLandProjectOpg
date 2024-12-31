namespace ScooterLandProjectOpg.Server.Interfaces // Definerer namespace for interfaces, der bruges til serveroperationer i ScooterLand-projektet.
{
    // Et generisk interface, der repræsenterer CRUD-operationer for en hvilken som helst type T, hvor T skal være en reference-type (class).
    public interface IRepository<T> where T : class
	{
        // Alle andre repositories arver fra denne.
        // Dette interface fungerer som en basis for alle andre repositories.

        // Definerer en asynkron metode, der henter alle poster af typen T fra databasen og returnerer en samling.
        Task<IEnumerable<T>> GetAllAsync();
        
        // Definerer en asynkron metode, der henter en specifik post af typen T fra databasen baseret på dens ID.
        Task<T> GetByIdAsync(int id);
        
        // Definerer en asynkron metode, der tilføjer en ny post af typen T til databasen og returnerer den tilføjede post.
        Task<T> AddAsync(T entity);
        
        // Definerer en asynkron metode, der opdaterer en eksisterende post af typen T i databasen.
        Task UpdateAsync(T entity);
        
        // Definerer en asynkron metode, der sletter en specifik post af typen T fra databasen baseret på dens ID.
        Task DeleteAsync(int id);
	}
}