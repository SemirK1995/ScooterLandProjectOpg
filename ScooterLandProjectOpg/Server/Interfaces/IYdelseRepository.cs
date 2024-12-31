using ScooterLandProjectOpg.Shared.Models; // Importerer modellen `Ydelse` fra det delte projekt for at kunne bruge den i repository-interfacet.

namespace ScooterLandProjectOpg.Server.Interfaces // Definerer navneområdet, hvor interfacet `IYdelseRepository` bor.
{
    // Definerer et interface for repository-mønsteret, som nedarver CRUD-funktionalitet fra det generiske `IRepository<Ydelse>`.
    public interface IYdelseRepository : IRepository<Ydelse> 
    {
        // En metode, der returnerer en liste af `Ydelse`-objekter med tilknyttede detaljer. Metoden er asynkron og udføres som en `Task`.
        Task<IEnumerable<Ydelse>> GetAllWithDetailsAsync();

        // En metode, der henter en specifik `Ydelse` baseret på dens ID og inkluderer eventuelle relaterede data. Denne metode er asynkron og udføres som en `Task`.
        Task<Ydelse> GetWithDetailsByIdAsync(int id); 
    }
}