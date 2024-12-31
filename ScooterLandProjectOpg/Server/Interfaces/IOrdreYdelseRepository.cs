using ScooterLandProjectOpg.Shared.Models; // Importerer modellen OrdreYdelse, som bruges i repository-operationerne.

namespace ScooterLandProjectOpg.Server.Interfaces // Definerer namespace for interfaces, der bruges i serverdelen af applikationen.
{
    // Definerer et interface for repository-operationer, der er specifikke for OrdreYdelse-modellen, og nedarver fra det generiske IRepository-interface.
    public interface IOrdreYdelseRepository : IRepository<OrdreYdelse>
    {
        // Definerer en asynkron metode, der henter alle ordreydelser fra databasen, inklusive relaterede detaljer som tilknyttede ydelser, scootere og mekanikere.
        Task<IEnumerable<OrdreYdelse>> GetAllWithDetailsAsync();

        // Definerer en asynkron metode, der henter en specifik ordreydelse fra databasen ved hjælp af dens ID og inkluderer relaterede detaljer som ydelser, scootere og mekanikere.
        Task<OrdreYdelse> GetWithDetailsByIdAsync(int id);
    }
}