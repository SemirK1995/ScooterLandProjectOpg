using ScooterLandProjectOpg.Shared.Models; // Importerer modelklassen Kunde, som repræsenterer en kunde i systemet.

namespace ScooterLandProjectOpg.Server.Interfaces // Angiver, at dette interface er en del af namespace ScooterLandProjectOpg.Server.Interfaces.
{
    // Definerer et repository-interface for Kunde, som nedarver fra et generisk IRepository-interface.
    public interface IKundeRepository : IRepository<Kunde>
    {
        // Definerer en metode til at hente alle kunder, inklusiv deres tilknyttede ordrer.
        Task<IEnumerable<Kunde>> GetAllWithOrdersAsync();

        // Definerer en metode til at hente en specifik kunde baseret på id og inkludere deres scootere.
        Task<Kunde> GetKundeWithScootersAsync(int id);

        // Definerer en metode til at søge efter kunder baseret på deres navn.
        Task<IEnumerable<Kunde>> SearchByNameAsync(string name);

        // Definerer en metode til at hente alle ordrer for en specifik kunde baseret på kundeId.
        Task<IEnumerable<Ordre>> GetOrdrerForKundeAsync(int kundeId);

        // Definerer en metode til at hente en kunde med mange detaljer baseret på deres id, fx ordrer og scootere.
        Task<Kunde> GetKundeWithManyDetailsByIdAsync(int kundeId);

        // Definerer en metode til at søge efter kunder baseret på id, navn eller telefonnummer.
        Task<IEnumerable<Kunde>> SearchKunderAsync(string søgeTekst);
    }
}