using ScooterLandProjectOpg.Server.Context; // Importerer konteksten til at arbejde med databasen.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer interface-definitioner for repository-metoder.
using ScooterLandProjectOpg.Shared.Models; // Importerer datamodeller for KundeScooter.
using Microsoft.EntityFrameworkCore; // Importerer EF Core til databaseinteraktion.

namespace ScooterLandProjectOpg.Server.Services // Definerer navnerummet for tjenesten.
{
    // Arver fra Repository<KundeScooter> og implementerer IKundeScooterRepository.
    public class KundeScooterService : Repository<KundeScooter>, IKundeScooterRepository 
    {
        // Felt til databasekonteksten.
        private readonly ScooterLandContext _context; 

        // Constructor der initialiserer konteksten og sender den videre til basisklassen.
        public KundeScooterService(ScooterLandContext context) : base(context) 
        {
            _context = context; // Initialiserer den private kontekstvariabel.
        }

        // Henter alle KundeScooter-objekter med relateret Kunde-data.
        public async Task<IEnumerable<KundeScooter>> GetScootersWithKundeAsync()
        {
            return await _context.Set<KundeScooter>() // Henter KundeScooter fra databasen.
                .Include(ks => ks.Kunde) // Inkluderer relateret Kunde-data.
                .ToListAsync(); // Konverterer resultatet til en liste.
        }

        // Tilføjer en ny KundeScooter til databasen og returnerer det oprettede objekt.
        public async Task<KundeScooter> AddScooterAsync(KundeScooter scooter)
        {
            _context.KunderScootere.Add(scooter); // Tilføjer scooteren til konteksten.
            await _context.SaveChangesAsync(); // Gemmer ændringerne i databasen.
            return scooter; // Returnerer det oprettede objekt.
        }

        // Henter alle scootere relateret til en bestemt kunde baseret på KundeId.
        public async Task<List<KundeScooter>> GetScootersByKundeIdAsync(int kundeId)
        {
            return await _context.KunderScootere // Henter KundeScooter fra databasen.
                .Where(ks => ks.KundeId == kundeId) // Filtrerer på KundeId.
                .ToListAsync(); // Konverterer resultatet til en liste.
        }

        // Henter en specifik scooter og dens relaterede Kunde baseret på ScooterId.
        public async Task<KundeScooter> GetScooterWithKundeByIdAsync(int id)
        {
            return await _context.KunderScootere // Henter KundeScooter fra databasen.
                .Include(ks => ks.Kunde) // Inkluderer relateret Kunde-data.
                .FirstOrDefaultAsync(ks => ks.ScooterId == id); // Finder den første scooter med det givne id.
        }
    }
}