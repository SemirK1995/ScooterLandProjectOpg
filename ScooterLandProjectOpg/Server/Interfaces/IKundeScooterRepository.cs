using ScooterLandProjectOpg.Shared.Models; // Importerer modelklassen KundeScooter, som repræsenterer relationen mellem en kunde og en scooter.

namespace ScooterLandProjectOpg.Server.Interfaces // Angiver, at dette interface er en del af namespace ScooterLandProjectOpg.Server.Interfaces.
{
    // Definerer et repository-interface for KundeScooter, som nedarver fra et generisk IRepository-interface.
    public interface IKundeScooterRepository : IRepository<KundeScooter>
    {
        // Definerer en metode til at hente alle scootere, inklusive deres tilknyttede kundeoplysninger.
        Task<IEnumerable<KundeScooter>> GetScootersWithKundeAsync();

        // Definerer en metode til at hente en specifik scooter med dens tilknyttede kunde baseret på scooterens id.
        Task<KundeScooter> GetScooterWithKundeByIdAsync(int id);

        // Definerer en metode til at tilføje en ny scooter til en kunde i systemet.
        Task<KundeScooter> AddScooterAsync(KundeScooter scooter);

        // Definerer en metode til at hente alle scootere, der er knyttet til en specifik kunde baseret på kundeId.
        Task<List<KundeScooter>> GetScootersByKundeIdAsync(int kundeId);
    }
}