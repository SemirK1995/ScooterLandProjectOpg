using ScooterLandProjectOpg.Shared.Enum; // Importerer enum-typen OrdreStatus, der bruges til at repræsentere forskellige statusser for en ordre.
using ScooterLandProjectOpg.Shared.Models; // Importerer modellen Ordre, som bruges i repository-operationerne.

namespace ScooterLandProjectOpg.Server.Interfaces // Definerer namespace for interfaces, der bruges i serverdelen af applikationen.
{
    // Definerer et interface for repository-operationer, der er specifikke for Ordre-modellen, og nedarver fra det generiske IRepository-interface.
    public interface IOrdreRepository : IRepository<Ordre>
    {
        // Definerer en asynkron metode, der henter alle ordrer fra databasen inklusive relaterede detaljer som kundeoplysninger, ydelser og produkter.
        Task<IEnumerable<Ordre>> GetAllWithDetailsAsync();

        // Definerer en asynkron metode, der henter en specifik ordre fra databasen ved hjælp af dens ID og inkluderer relaterede detaljer som kunde, ydelser og produkter.
        Task<Ordre> GetWithDetailsByIdAsync(int id);

        // Definerer en asynkron metode, der opdaterer status for en ordre baseret på dens ID og sætter den til en ny status, der gives som parameter.
        Task UpdateOrdreStatusAsync(int ordreId, OrdreStatus nyStatus);
    }
}