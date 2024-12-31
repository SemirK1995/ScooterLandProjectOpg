using Microsoft.EntityFrameworkCore; // Importerer Entity Framework Core til databaseinteraktion.
using ScooterLandProjectOpg.Server.Context; // Importerer den specifikke databasekontekst for ScooterLand.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer interface-definitionen for repository-klassen.

namespace ScooterLandProjectOpg.Server.Services // Definerer namespace for tjenester relateret til databasen.
{
    // Generisk repository-klasse, der implementerer IRepository for type T.
    public class Repository<T> : IRepository<T> where T : class 
    {
        private readonly ScooterLandContext _context; // Felt til at holde en reference til databasekonteksten.
        private readonly DbSet<T> _dbSet; // Felt til at holde en reference til den specifikke DbSet for T.

        // Constructor, der initialiserer databasekonteksten.
        public Repository(ScooterLandContext context) 
        {
            _context = context; // Initialiserer _context med den leverede databasekontekst.
            _dbSet = context.Set<T>(); // Initialiserer _dbSet med DbSet for T fra konteksten.
        }

        // Metode til at hente alle poster af type T.
        public async Task<IEnumerable<T>> GetAllAsync() 
        {
            return await _dbSet.ToListAsync(); // Henter alle poster fra databasen og returnerer dem som en liste.
        }

        // Metode til at hente en enkelt post baseret på dens ID.
        public async Task<T> GetByIdAsync(int id) 
        {
            return await _dbSet.FindAsync(id); // Finder posten med det givne ID.
        }

        // Metode til at tilføje en ny post til databasen.
        public async Task<T> AddAsync(T entity) 
        {
            await _dbSet.AddAsync(entity); // Tilføjer posten til DbSet.
            await _context.SaveChangesAsync(); // Gemmer ændringerne i databasen.
            return entity; // Returnerer den tilføjede post.
        }

        // Metode til at opdatere en eksisterende post.
        public async Task UpdateAsync(T entity) 
        {
            _dbSet.Update(entity); // Marker posten som opdateret i DbSet.
            await _context.SaveChangesAsync(); // Gemmer ændringerne i databasen.
        }

        // Metode til at slette en post baseret på dens ID.
        public async Task DeleteAsync(int id) 
        {
            var entity = await _dbSet.FindAsync(id); // Finder posten med det givne ID.
            if (entity != null) // Tjekker, om posten blev fundet.
            {
                _dbSet.Remove(entity); // Fjerner posten fra DbSet.
                await _context.SaveChangesAsync(); // Gemmer ændringerne i databasen.
            }
        }
    }
}