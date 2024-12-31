using ScooterLandProjectOpg.Server.Services; // Importerer services fra serverprojektet, der kan bruges til operationer relateret til leje-scootere.
using ScooterLandProjectOpg.Shared.Models; // Importerer modeller fra det delte bibliotek, specifikt LejeScooter-modellen, der repræsenterer en scooter i en lejeaftale.

namespace ScooterLandProjectOpg.Server.Interfaces // Definerer namespace for serverinterfaces, der bruges til at beskrive repository-operationer for leje-scootere.
{
    // Definerer et interface, der nedarver fra det generiske IRepository-interface og er specifikt for LejeScooter.
    public interface ILejeScooterRepository : IRepository<LejeScooter>
    {
        // Angiver en metode til at hente alle scootere, der er tilgængelige for leje (hvor LejeId = null).
        Task<IEnumerable<LejeScooter>> GetScootersAvailableAsync();

        // Angiver en metode til at opdatere LejeId for en specifik scooter, så den knyttes til en given lejeaftale.
        Task UpdateScooterLejeIdAsync(int scooterId, int lejeId);
    }
}