using ScooterLandProjectOpg.Server.Interfaces; // Importerer interface-definitionen for OrdreYdelse-tjenesten.
using ScooterLandProjectOpg.Shared.Models; // Importerer modelklasser, som OrdreYdelse, der bruges i tjenesten.
using Microsoft.EntityFrameworkCore; // Importerer Entity Framework Core til databaseinteraktion.
using ScooterLandProjectOpg.Server.Context; // Importerer databasekonteksten til at arbejde med ScooterLand-projektets database.

namespace ScooterLandProjectOpg.Server.Services // Definerer namespace for tjenestelogik.
{
    // Implementerer IOrdreYdelseRepository og arver fra Repository<OrdreYdelse>.
    public class OrdreYdelseService : Repository<OrdreYdelse>, IOrdreYdelseRepository 
    {
        // Felt til at holde en reference til databasekonteksten.
        private readonly ScooterLandContext _context; 

        // Constructor, der initialiserer databasekonteksten og sender den til basisklassen.
        public OrdreYdelseService(ScooterLandContext context) : base(context) 
        {
            _context = context; // Initialiserer den lokale databasekontekst med den leverede instans.
        }

        // Metode til at hente alle OrdreYdelser med detaljerede data.
        public async Task<IEnumerable<OrdreYdelse>> GetAllWithDetailsAsync() 
        {
            return await _context.Set<OrdreYdelse>() // Får adgang til "OrdreYdelse"-datasættet i databasen.
                .Include(o => o.Ordre) // Inkluderer relaterede Ordre-oplysninger i forespørgslen.
                .Include(o => o.Ydelse) // Inkluderer relaterede Ydelse-oplysninger i forespørgslen.
                .Include(o => o.Scooter) // Inkluderer relaterede Scooter-oplysninger.
                .ThenInclude(s => s.Kunde) // Indlæser Kunde-oplysninger for de tilknyttede scootere.
                .ToListAsync(); // Konverterer resultatet til en liste og udfører den asynkront.
        }

        // Metode til at hente en specifik OrdreYdelse baseret på dens ID med detaljer.
        public async Task<OrdreYdelse> GetWithDetailsByIdAsync(int id) 
        {
            return await _context.Set<OrdreYdelse>() // Får adgang til "OrdreYdelse"-datasættet i databasen.
                .Include(o => o.Ordre) // Inkluderer relaterede Ordre-oplysninger i forespørgslen.
                .Include(o => o.Ydelse) // Inkluderer relaterede Ydelse-oplysninger i forespørgslen.
                .FirstOrDefaultAsync(o => o.OrdreYdelseId == id); // Finder den første OrdreYdelse, der matcher det angivne ID.
        }
    }
}