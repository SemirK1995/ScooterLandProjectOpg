using ScooterLandProjectOpg.Server.Context; // Importerer databasekonteksten til interaktion med databasen.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer interface-definitioner for mekanikere.
using ScooterLandProjectOpg.Shared.Models; // Importerer modellen for Mekaniker.
using Microsoft.EntityFrameworkCore; // Importerer Entity Framework Core for databasenavigation.
using ScooterLandProjectOpg.Shared.Enum; // Importerer enum-typer, f.eks. OrdreStatus.

namespace ScooterLandProjectOpg.Server.Services // Definerer navnerummet for tjenestelogikken.
{
    // MekanikerService implementerer IMekanikerRepository og arver fra Repository<Mekaniker>.
    public class MekanikerService : Repository<Mekaniker>, IMekanikerRepository 
    {
        // Felt til at holde den aktuelle databasekontekst.
        private readonly ScooterLandContext _context; 

        // Constructor, der initialiserer databasekonteksten og sender den til basisklassen.
        public MekanikerService(ScooterLandContext context) : base(context) 
        {
            _context = context; // Initialiserer den lokale databasekontekst.
        }

        // Metode til at hente alle mekanikere fra databasen.
        public async Task<IEnumerable<Mekaniker>> GetAllAsync()
        {
            return await _context.Mekanikere.ToListAsync(); // Returnerer alle mekanikere som en liste.
        }

        // Metode til at hente en specifik mekaniker baseret på dens ID.
        public async Task<Mekaniker> GetByIdAsync(int id)
        {
            return await _context.Mekanikere
                .FirstOrDefaultAsync(m => m.MekanikerId == id); // Finder første mekaniker med det matchende ID.
        }

        // Metode til at tilføje en ny mekaniker til databasen.
        public async Task<Mekaniker> AddAsync(Mekaniker mekaniker)
        {
            _context.Mekanikere.Add(mekaniker); // Tilføjer mekanikeren til konteksten.
            await _context.SaveChangesAsync(); // Gemmer ændringerne i databasen.
            return mekaniker; // Returnerer den nyligt tilføjede mekaniker.
        }

        // Metode til at opdatere en eksisterende mekanikers oplysninger.
        public async Task UpdateAsync(Mekaniker mekaniker)
        {
            _context.Mekanikere.Update(mekaniker); // Marker mekanikeren som opdateret.
            await _context.SaveChangesAsync(); // Gemmer ændringerne i databasen.
        }

        // Metode til at slette en mekaniker baseret på ID.
        public async Task DeleteAsync(int id)
        {
            var mekaniker = await _context.Mekanikere.FindAsync(id); // Finder mekanikeren i databasen baseret på ID.
            if (mekaniker != null) // Hvis mekanikeren findes:
            {
                _context.Mekanikere.Remove(mekaniker); // Fjern mekanikeren fra databasen.
                await _context.SaveChangesAsync(); // Gemmer ændringerne i databasen.
            }
        }

        // Metode til at hente aktive arbejdsopgaver for en mekaniker baseret på ID.
        public async Task<IEnumerable<OrdreYdelse>> GetAktiveArbejdsopgaverForMekanikerAsync(int mekanikerId)
        {
            return await _context.OrdreYdelser
                .Include(oy => oy.Ydelse) // Inkluder relaterede oplysninger om ydelsen.
                .Include(oy => oy.Scooter) // Inkluder oplysninger om den relaterede scooter.
                .Include(oy => oy.Ordre) // Inkluder relaterede ordreoplysninger for filtrering.
                .Where(oy => oy.MekanikerId == mekanikerId && // Filtrer efter mekanikerens ID.
                             oy.Ordre.Status != OrdreStatus.Betalt && // Ekskluder opgaver fra betalte ordrer.
                             oy.Ordre.Status != OrdreStatus.Annulleret) // Ekskluder opgaver fra annullerede ordrer.
                .ToListAsync(); // Returnerer resultatet som en liste asynkront.
        }
    }
}