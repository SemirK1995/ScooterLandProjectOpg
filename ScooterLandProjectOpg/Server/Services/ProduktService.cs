using Microsoft.EntityFrameworkCore; // Importerer Entity Framework Core til databaseinteraktion.
using ScooterLandProjectOpg.Server.Context; // Importerer ScooterLand databasekonteksten.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer interface-definitionerne for produktrelaterede tjenester.
using ScooterLandProjectOpg.Shared.Models; // Importerer modelklassen "Produkt", der bruges til datarepræsentation.

namespace ScooterLandProjectOpg.Server.Services // Definerer namespace for produktrelaterede tjenester.
{
    // Implementerer produktrelateret repository og interface.
    public class ProduktService : Repository<Produkt>, IProduktRepository 
    {
        // Felt til at holde en reference til databasekonteksten.
        private readonly ScooterLandContext _context; 

        // Constructor, der initialiserer databasekonteksten og sender den til basisklassen.
        public ProduktService(ScooterLandContext context) : base(context) 
        { 
            _context = context; // Initialiserer feltet med den leverede databasekontekst.
        }

        // Metode til at hente alle produkter fra databasen.
        public async Task<IEnumerable<Produkt>> GetAllAsync() 
        {
            return await _context.Set<Produkt>().ToListAsync(); // Henter alle produkter som en liste asynkront.
        }

        // Metode til at hente et specifikt produkt baseret på dets ID.
        public async Task<Produkt> GetByIdAsync(int id) 
        {
            return await _context.Set<Produkt>().FindAsync(id); // Finder produktet med det givne ID i databasen.
        }

        // Metode til at oprette et nyt produkt i databasen.
        public async Task<Produkt> AddAsync(Produkt produkt) 
        {
            var createdEntity = await _context.Set<Produkt>().AddAsync(produkt); // Tilføjer det nye produkt til konteksten.
            await _context.SaveChangesAsync(); // Gemmer ændringerne i databasen.
            return createdEntity.Entity; // Returnerer det oprettede produkt.
        }

        // Metode til at opdatere et eksisterende produkt i databasen.
        public async Task<Produkt> UpdateAsync(Produkt produkt) 
        {
            var existingEntity = await _context.Set<Produkt>().FindAsync(produkt.ProduktId); // Finder det eksisterende produkt baseret på ID.
            if (existingEntity == null) // Hvis produktet ikke findes, kastes en undtagelse.
            {
                throw new KeyNotFoundException($"Produkt med ID {produkt.ProduktId} blev ikke fundet.");
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(produkt); // Opdaterer de eksisterende værdier med de nye værdier.
            await _context.SaveChangesAsync(); // Gemmer ændringerne i databasen.
            return existingEntity; // Returnerer det opdaterede produkt.
        }

        // Metode til at slette et produkt fra databasen.
        public async Task DeleteAsync(int id) 
        {
            var entity = await _context.Set<Produkt>().FindAsync(id); // Finder det produkt, der skal slettes, baseret på ID.
            if (entity == null) // Hvis produktet ikke findes, kastes en undtagelse.
            {
                throw new KeyNotFoundException($"Produkt med ID {id} blev ikke fundet.");
            }

            _context.Set<Produkt>().Remove(entity); // Fjerner produktet fra konteksten.
            await _context.SaveChangesAsync(); // Gemmer ændringerne i databasen.
        }
    }
}