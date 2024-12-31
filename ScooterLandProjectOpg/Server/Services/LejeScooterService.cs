using Microsoft.EntityFrameworkCore; // Importerer Entity Framework Core til databaseinteraktion.
using ScooterLandProjectOpg.Server.Context; // Importerer den specifikke databasekontekst for ScooterLand.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer interface-definitioner for repository-metoder.
using ScooterLandProjectOpg.Shared.Models; // Importerer datamodellen for LejeScooter.

namespace ScooterLandProjectOpg.Server.Services // Definerer navnerummet for tjenesten.
{
    // Arver fra Repository<LejeScooter> og implementerer ILejeScooterRepository.
    public class LejeScooterService : Repository<LejeScooter>, ILejeScooterRepository 
    {
        // Felt til at holde den aktuelle databasekontekst.
        private readonly ScooterLandContext _context; 

        // Constructor, der initialiserer konteksten og sender den til basisklassen.
        public LejeScooterService(ScooterLandContext context) : base(context) 
        {
            _context = context; // Initialiserer den private kontekstvariabel.
        }

        // Metode til at hente en liste over scootere, der er tilgængelige til leje.
        public async Task<IEnumerable<LejeScooter>> GetScootersAvailableAsync()
        {
            return await _context.LejeScootere // Får adgang til LejeScootere-tabellen i databasen.
                .Where(scooter => scooter.LejeId == null) // Filtrerer scootere, hvor LejeId er null (dvs. de er ikke lejet ud).
                .ToListAsync(); // Konverterer resultatet til en liste og udfører forespørgslen asynkront.
        }

        // Metode til at opdatere LejeId og tilgængelighed for en specifik scooter.
        public async Task UpdateScooterLejeIdAsync(int scooterId, int lejeId)
        {
            var scooter = await _context.LejeScootere.FindAsync(scooterId); // Finder scooteren i databasen baseret på ID.
            if (scooter == null) // Hvis scooteren ikke findes, kastes en fejl.
            {
                throw new ArgumentException($"Scooter with ID {scooterId} does not exist."); // Fejlbesked for ugyldigt ID.
            }

            scooter.LejeId = lejeId; // Opdaterer LejeId med den nye lejeaftale-ID.
            scooter.ErTilgængelig = false; // Marker scooteren som ikke tilgængelig, da den er blevet lejet ud.

            _context.LejeScootere.Update(scooter); // Opdaterer scooteren i databasen.
            await _context.SaveChangesAsync(); // Gemmer ændringerne asynkront.
        }
    }
}