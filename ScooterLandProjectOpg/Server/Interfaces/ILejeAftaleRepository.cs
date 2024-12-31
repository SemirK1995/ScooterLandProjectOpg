using ScooterLandProjectOpg.Shared.Models; // Importerer modellen LejeAftale, som repræsenterer en lejeaftale i systemet.

namespace ScooterLandProjectOpg.Server.Interfaces // Definerer namespace for serverinterfaces, der bruges i projektet.
{
    // Angiver, at dette interface er et specifikt repository for LejeAftale og nedarver fra det generiske IRepository-interface.
    public interface ILejeAftaleRepository : IRepository<LejeAftale>
    {
        // Definerer en metode til at hente alle lejeaftaler med detaljerede oplysninger om kunder og scootere.
        Task<IEnumerable<LejeAftale>> GetAllWithKundeAndScootersAsync();

        // Definerer en metode til at hente en specifik lejeaftale med detaljerede oplysninger baseret på lejeaftalens id.
        Task<LejeAftale> GetLejeAftaleWithDetailsAsync(int id);

        // Definerer en metode til at søge efter lejeaftaler baseret på en forespørgsel (query).
        Task<IEnumerable<LejeAftale>> SearchLejeAftalerAsync(string query);

        // Definerer en metode til at opdatere selvrisikoen for en specifik lejeaftale og returnere den relaterede ordre.
        Task<Ordre> UpdateSelvrisikoAsync(int lejeId, double selvrisiko);

        // Definerer en metode til at opdatere kilometerantallet for en lejeaftale og returnere den opdaterede lejeaftale.
        Task<LejeAftale?> UpdateKortKilometerAsync(int lejeId, int? kortKilometer);
    }
}