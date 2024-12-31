using ScooterLandProjectOpg.Server.Interfaces; // Importerer interface-definitioner for Ydelse-relaterede operationer.
using ScooterLandProjectOpg.Shared.Models; // Importerer modeldefinitionen for Ydelse.
using Microsoft.EntityFrameworkCore; // Importerer Entity Framework Core til databaseinteraktion.
using ScooterLandProjectOpg.Server.Context; // Importerer databasekonteksten for ScooterLand.

namespace ScooterLandProjectOpg.Server.Services // Definerer namespace for Ydelse-tjenester.
{
    // Arver generisk Repository og implementerer IYdelseRepository.
    public class YdelseService : Repository<Ydelse>, IYdelseRepository 
    {
        // Felt til at holde en reference til databasekonteksten.
        private readonly ScooterLandContext _context; 

        // Constructor, der initialiserer databasekonteksten.
        public YdelseService(ScooterLandContext context) : base(context) 
        {
            _context = context; // Initialiserer _context med den leverede databasekontekst.
        }

        // Metode til at hente alle Ydelser fra databasen.
        public async Task<IEnumerable<Ydelse>> GetAllWithDetailsAsync() 
        {
            return await _context.Ydelser.ToListAsync(); // Henter alle poster i Ydelser-tabellen og returnerer som en liste.
        }

        // Metode til at hente en Ydelse ved hjælp af dens ID.
        public async Task<Ydelse> GetWithDetailsByIdAsync(int id) 
        {
            return await _context.Ydelser.FindAsync(id); // Finder og returnerer Ydelsen med det angivne ID.
        }

        // Metode til at tilføje en ny Ydelse til databasen.
        public async Task<Ydelse> AddAsync(Ydelse entity) 
        {
            await _context.Ydelser.AddAsync(entity); // Tilføjer Ydelsen til DbSet.
            await _context.SaveChangesAsync(); // Gemmer ændringer i databasen.
            return entity; // Returnerer den tilføjede Ydelse.
        }

        // Metode til at opdatere en eksisterende Ydelse i databasen.
        public async Task UpdateAsync(Ydelse entity) 
        {
            _context.Ydelser.Update(entity); // Marker Ydelsen som opdateret i DbSet.
            await _context.SaveChangesAsync(); // Gemmer ændringer i databasen.
        }

        // Metode til at slette en Ydelse baseret på dens ID.
        public async Task DeleteAsync(int id) 
        {
            var ydelse = await _context.Ydelser.FindAsync(id); // Finder Ydelsen med det angivne ID.
            if (ydelse != null) // Tjekker, om Ydelsen blev fundet.
            {
                _context.Ydelser.Remove(ydelse); // Fjerner Ydelsen fra DbSet.
                await _context.SaveChangesAsync(); // Gemmer ændringer i databasen.
            }
        }
    }
}
