using ScooterLandProjectOpg.Shared.DTO; // Importerer Data Transfer Objects (DTO'er) for at kunne overføre data mellem lag i applikationen.
using ScooterLandProjectOpg.Shared.Enum; // Importerer enum-typer, som bruges til faste værdier, fx betalingsmetoder.
using ScooterLandProjectOpg.Shared.Models; // Importerer modelklasser, som repræsenterer entiteterne i systemet, fx Betaling.

namespace ScooterLandProjectOpg.Server.Interfaces // Angiver, at dette interface er en del af namespace ScooterLandProjectOpg.Server.Interfaces.
{
    // Definerer et repository-interface for Betaling, som nedarver fra et generisk IRepository-interface.
    public interface IBetalingRepository : IRepository<Betaling>
    {
        // Definerer en metode til at søge betalinger baseret på en tekstforespørgsel (query), fx kunde- eller ordrenavn.
        Task<IEnumerable<Betaling>> SearchByQueryAsync(string query);

        // Definerer en metode til at opdatere betalingsstatus for en betaling, hvor betalingsId angiver den specifikke betaling, og BetalingUpdateDto indeholder de nye værdier.
        Task UpdateBetalingsStatusAsync(int betalingsId, BetalingUpdateDto betalingUpdate);

        // Definerer en metode til at opdatere betalingsmetoden for en betaling, fx ændring fra "Kontant" til "MobilePay".
        Task UpdateBetalingsMetodeAsync(int betalingsId, BetalingsMetodeStatus nyMetode);

        // Definerer en metode til at hente detaljer om en betaling, som fx kundeoplysninger og ordreoplysninger, der skal bruges til en faktura.
        Task<Betaling> GetFakturaDetaljerAsync(int betalingsId);

        // Definerer en metode, der opretter betalinger for eksisterende ordrer, som fx mangler tilknyttede betalinger.
        Task<int> OpretBetalingerTilEksisterendeOrdrerAsync();
    }
}