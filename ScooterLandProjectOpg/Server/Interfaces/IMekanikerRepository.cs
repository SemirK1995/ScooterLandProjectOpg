using ScooterLandProjectOpg.Shared.Models; // Importerer modellen Mekaniker og relaterede data, der skal bruges i repository-operationer.

namespace ScooterLandProjectOpg.Server.Interfaces // Definerer namespace for interfaces, der bruges i serverdelen af applikationen.
{
    // Definerer et interface for repository-operationer, der er specifikke for Mekaniker-modellen og nedarver fra det generiske IRepository-interface.
    public interface IMekanikerRepository : IRepository<Mekaniker>
    {
        // Definerer en metode til at hente alle mekanikere fra databasen som en asynkron opgave.
        Task<IEnumerable<Mekaniker>> GetAllAsync();

        // Definerer en metode til at hente en specifik mekaniker ved hjælp af deres ID som en asynkron opgave.
        Task<Mekaniker> GetByIdAsync(int id);

        // Definerer en metode til at tilføje en ny mekaniker til databasen som en asynkron opgave.
        Task<Mekaniker> AddAsync(Mekaniker mekaniker);

        // Definerer en metode til at opdatere eksisterende mekanikerdata som en asynkron opgave.
        Task UpdateAsync(Mekaniker mekaniker);

        // Definerer en metode til at slette en mekaniker fra databasen ved hjælp af deres ID som en asynkron opgave.
        Task DeleteAsync(int id);

        // Definerer en metode til at hente alle aktive arbejdsopgaver for en specifik mekaniker baseret på deres ID.
        Task<IEnumerable<OrdreYdelse>> GetAktiveArbejdsopgaverForMekanikerAsync(int mekanikerId);
    }
}