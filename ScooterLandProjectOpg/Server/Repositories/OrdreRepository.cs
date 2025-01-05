using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Server.Repositories.Interfaces;
using ScooterLandProjectOpg.Server.Services;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Repositories
{
    public class OrdreRepository : Repository<Ordre>, IOrdreRepository
    {
        private readonly ScooterLandContext _context;

        // Constructor, der initialiserer databasekonteksten og sender den til basisklassen.
        public OrdreRepository(ScooterLandContext context) : base(context)
        {
            _context = context; // Initialiserer lokal databasekontekst.
        }


        public async Task<IEnumerable<Ordre>> GetAllOrders()
        {
            return await _context.Set<Ordre>()
                 .Include(o => o.Kunde) // Inkluderer oplysninger om kunden, der er knyttet til hver ordre.
                 .Include(o => o.Betalinger) // Inkluderer betalingsoplysninger, der er knyttet til hver ordre.
                 .Include(o => o.OrdreYdelse) // Inkluderer ordreydelser, der er knyttet til hver ordre.
                 .ToListAsync(); // Returnerer resultaterne som en liste af ordrer.
        }

        public Task<Ordre> GetOrderById(int id)
        {
            return GetByIdAsync(id);
        }
    }
}
