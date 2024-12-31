using ScooterLandProjectOpg.Shared.Models; // Importerer modellen Produkt, som bruges i repository-operationerne.

namespace ScooterLandProjectOpg.Server.Interfaces // Definerer namespace for interfaces, der bruges i serverdelen af applikationen.
{
    // Definerer et interface for repository-operationer, der er specifikke for Produkt-modellen, og nedarver fra det generiske IRepository-interface.
    public interface IProduktRepository : IRepository<Produkt>
    {
        // Definerer en asynkron metode, der henter alle produkter fra databasen.
        Task<IEnumerable<Produkt>> GetAllAsync();

        // Definerer en asynkron metode, der henter et specifikt produkt fra databasen ved hjælp af dets ID.
        Task<Produkt> GetByIdAsync(int id);

        // Definerer en asynkron metode, der tilføjer et nyt produkt til databasen og returnerer det oprettede produkt.
        Task<Produkt> AddAsync(Produkt produkt);

        // Definerer en asynkron metode, der opdaterer et eksisterende produkt i databasen og returnerer det opdaterede produkt.
        Task<Produkt> UpdateAsync(Produkt produkt);

        // Definerer en asynkron metode, der sletter et produkt fra databasen ved hjælp af dets ID.
        Task DeleteAsync(int id);
    }
}