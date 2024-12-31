using Microsoft.EntityFrameworkCore; // Importerer Entity Framework Core til databaseinteraktion.
using ScooterLandProjectOpg.Server.Context; // Importerer konteksten til at arbejde med databasen.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer interface-definitioner for repository-metoder.
using ScooterLandProjectOpg.Shared.Models; // Importerer datamodeller for Kunde.

namespace ScooterLandProjectOpg.Server.Services // Definerer navnerummet for tjenesten.
{
    // Arver fra Repository<Kunde> og implementerer IKundeRepository.
    public class KundeService : Repository<Kunde>, IKundeRepository 
    {
        // Felt til databasekonteksten.
        private readonly ScooterLandContext _context; 

        // Constructor der initialiserer konteksten og sender den videre til basisklassen.
        public KundeService(ScooterLandContext context) : base(context) 
        {
            _context = context; // Initialiserer den private kontekstvariabel.
        }

        // Henter alle kunder med tilknyttede ordrer.
        public async Task<IEnumerable<Kunde>> GetAllWithOrdersAsync()
        {
            return await _context.Kunder // Henter kunder fra databasen.
                .Include(k => k.Ordre) // Inkluderer relaterede ordrer.
                .ToListAsync(); // Konverterer resultatet til en liste.
        }

        // Henter en specifik kunde med tilknyttede scootere.
        public async Task<Kunde> GetKundeWithScootersAsync(int id)
        {
            return await _context.Kunder // Henter kunder fra databasen.
                .Include(k => k.KundeScooter) // Inkluderer relaterede KundeScooter.
                .FirstOrDefaultAsync(k => k.KundeId == id); // Finder den første kunde med det givne id.
        }

        // Søger efter kunder baseret på navn.
        public async Task<IEnumerable<Kunde>> SearchByNameAsync(string name)
        {
            return await _context.Kunder // Henter kunder fra databasen.
                .Where(k => EF.Functions.Like(k.Navn, $"%{name}%")) // Filtrerer på navnet ved hjælp af EF's Like-funktion.
                .ToListAsync(); // Konverterer resultatet til en liste.
        }

        // Sletter en kunde baseret på id.
        public async Task DeleteAsync(int id)
        {
            var kunde = await _context.Kunder.FindAsync(id); // Finder kunden baseret på id.
            if (kunde == null) // Hvis kunden ikke findes, kastes en fejl.
            {
                throw new Exception($"Kunde med ID {id} blev ikke fundet.");
            }

            _context.Kunder.Remove(kunde); // Fjerner kunden fra databasen.
            await _context.SaveChangesAsync(); // Gemmer ændringerne.
        }

        // Opdaterer en kunde.
        public new async Task UpdateAsync(Kunde entity)
        {
            await base.UpdateAsync(entity); // Kalder basisklassens UpdateAsync-metode.
        }

        // Tilføjer en ny kunde.
        public new async Task<Kunde> AddAsync(Kunde entity)
        {
            return await base.AddAsync(entity); // Kalder basisklassens AddAsync-metode.
        }

        // Henter ordrer for en specifik kunde baseret på KundeId.
        public async Task<IEnumerable<Ordre>> GetOrdrerForKundeAsync(int kundeId)
        {
            return await _context.Ordrer // Henter ordrer fra databasen.
                .Where(o => o.KundeId == kundeId) // Filtrerer på KundeId.
                .Include(o => o.Kunde) // Inkluderer relateret Kunde-data.
                .Include(o => o.OrdreYdelse) // Inkluderer OrdreYdelse.
                    .ThenInclude(oy => oy.Ydelse) // Inkluderer Ydelse via OrdreYdelse.
                .ToListAsync(); // Konverterer resultatet til en liste.
        }

        // Henter en kunde med mange detaljer baseret på KundeId.
        public async Task<Kunde> GetKundeWithManyDetailsByIdAsync(int kundeId)
        {
            return await _context.Kunder // Henter kunder fra databasen.
                .Include(k => k.Ordre) // Inkluderer ordrer.
                    .ThenInclude(o => o.OrdreYdelse) // Inkluderer OrdreYdelse.
                        .ThenInclude(oy => oy.Ydelse) // Inkluderer Ydelse via OrdreYdelse.
                .Include(k => k.Ordre) // Inkluderer ordrer.
                    .ThenInclude(o => o.OrdreProdukter) // Inkluderer OrdreProdukter.
                        .ThenInclude(op => op.Produkt) // Inkluderer Produkter via OrdreProdukter.
                .Include(k => k.Ordre) // Inkluderer ordrer.
                    .ThenInclude(o => o.LejeAftale) // Inkluderer LejeAftale.
                        .ThenInclude(la => la.LejeScooter) // Inkluderer scootere via LejeAftale.
                .Include(k => k.KundeScooter) // Inkluderer KundeScooter.
                .FirstOrDefaultAsync(k => k.KundeId == kundeId); // Finder den første kunde med det givne id.
        }

        // Søger efter kunder baseret på tekst, der kan være id, telefonnummer eller navn.
        public async Task<IEnumerable<Kunde>> SearchKunderAsync(string søgeTekst)
        {
            if (string.IsNullOrWhiteSpace(søgeTekst)) // Tjekker, om søgeteksten er tom eller kun består af mellemrum.
                return new List<Kunde>(); // Returnerer en tom liste, hvis søgeteksten er tom.

            var søgning = søgeTekst.Trim().ToLower(); // Trimmer og konverterer søgeteksten til små bogstaver.

            bool isNumeric = int.TryParse(søgning, out int numericValue); // Tjekker, om søgeteksten er numerisk.

            // Filtrerer kunder baseret på id, telefonnummer eller navn.
            var kunder = await _context.Kunder
                .Where(k =>
                    (isNumeric && (k.KundeId == numericValue ||
                                   (k.Telefonnummer.HasValue && k.Telefonnummer.Value == numericValue))) ||
                    (!isNumeric && k.Navn != null && k.Navn.ToLower().Contains(søgning)))
                .ToListAsync();

            return kunder; // Returnerer den filtrerede liste af kunder.
        }
    }
}