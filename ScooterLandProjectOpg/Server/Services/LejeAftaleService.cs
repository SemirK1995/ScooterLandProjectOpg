using ScooterLandProjectOpg.Server.Context; // Importerer databasekonteksten for applikationen.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer interface-definitioner for repository-metoder.
using ScooterLandProjectOpg.Shared.Models; // Importerer datamodeller som LejeAftale og Ordre.
using Microsoft.EntityFrameworkCore; // Importerer Entity Framework Core til databaseinteraktion.

namespace ScooterLandProjectOpg.Server.Services // Definerer navnerummet for tjenesten.
{
    // Arver fra Repository<LejeAftale> og implementerer ILejeAftaleRepository.
    public class LejeAftaleService : Repository<LejeAftale>, ILejeAftaleRepository 
    {
        // Felt til databasekonteksten.
        private readonly ScooterLandContext _context; 

        // Constructor der initialiserer konteksten og sender den videre til basisklassen.
        public LejeAftaleService(ScooterLandContext context) : base(context) 
        {
            _context = context; // Initialiserer den private kontekstvariabel.
        }

        // Henter alle lejeaftaler med tilknyttede kunder og scootere.
        public async Task<IEnumerable<LejeAftale>> GetAllWithKundeAndScootersAsync()
        {
            return await _context.Set<LejeAftale>() // Henter lejeaftaler fra databasen.
                .Include(la => la.Kunde) // Inkluderer relaterede kunder.
                .Include(la => la.LejeScooter) // Inkluderer relaterede scootere.
                .ToListAsync(); // Konverterer resultatet til en liste.
        }

        // Henter en specifik lejeaftale med detaljer som kunde og scooter.
        public async Task<LejeAftale> GetLejeAftaleWithDetailsAsync(int id)
        {
            return await _context.Set<LejeAftale>() // Henter lejeaftaler fra databasen.
                .Include(la => la.Kunde) // Inkluderer relateret kunde.
                .Include(la => la.LejeScooter) // Inkluderer relaterede scootere.
                .FirstOrDefaultAsync(la => la.LejeId == id); // Finder den første lejeaftale med det givne id.
        }

        // Søger efter lejeaftaler baseret på forespørgselsstrengen.
        public async Task<IEnumerable<LejeAftale>> SearchLejeAftalerAsync(string query)
        {
            return await _context.Set<LejeAftale>() // Henter lejeaftaler fra databasen.
                .Include(la => la.Kunde) // Inkluderer relateret kunde.
                .Where(la => la.Kunde.Navn.Contains(query) || // Filtrerer på kundens navn.
                             la.KundeId.ToString().Contains(query) || // Filtrerer på KundeId.
                             la.LejeId.ToString().Contains(query)) // Filtrerer på LejeId.
                .ToListAsync(); // Konverterer resultatet til en liste.
        }

        // Opdaterer selvrisikoen for en given lejeaftale.
        public async Task<Ordre> UpdateSelvrisikoAsync(int lejeId, double selvrisiko)
        {
            var lejeAftale = await _context.Set<LejeAftale>() // Henter lejeaftalen fra databasen.
                .Include(la => la.Ordre) // Inkluderer relateret ordre.
                .FirstOrDefaultAsync(la => la.LejeId == lejeId); // Finder lejeaftalen med det givne id.

            if (lejeAftale == null) // Hvis lejeaftalen ikke findes, kastes en fejl.
                throw new KeyNotFoundException($"Lejeaftale med ID {lejeId} blev ikke fundet.");

            lejeAftale.Selvrisiko = selvrisiko; // Opdaterer selvrisikoen.

            _context.Update(lejeAftale); // Opdaterer lejeaftalen i databasen.
            await _context.SaveChangesAsync(); // Gemmer ændringerne.

            return await _context.Set<Ordre>() // Henter den relaterede ordre.
                .Include(o => o.LejeAftale) // Inkluderer lejeaftalen.
                .Include(o => o.OrdreProdukter) // Inkluderer ordreprodukter.
                .FirstOrDefaultAsync(o => o.OrdreId == lejeAftale.Ordre.OrdreId); // Returnerer ordren baseret på id.
        }

        // Opdaterer antallet af kørte kilometer for en given lejeaftale.
        public async Task<LejeAftale?> UpdateKortKilometerAsync(int lejeId, int? kortKilometer)
        {
            var lejeAftale = await _context.LejeAftaler // Henter lejeaftaler fra databasen.
                .Include(la => la.Ordre) // Inkluderer relateret ordre.
                .FirstOrDefaultAsync(la => la.LejeId == lejeId); // Finder lejeaftalen med det givne id.

            if (lejeAftale == null) // Hvis lejeaftalen ikke findes, kastes en fejl.
            {
                throw new KeyNotFoundException($"Lejeaftale med ID {lejeId} blev ikke fundet.");
            }

            lejeAftale.KortKilometer = kortKilometer; // Opdaterer antallet af kørte kilometer.

            if (lejeAftale.Ordre != null) // Hvis der er en relateret ordre, opdateres dens totalpris.
            {
                lejeAftale.Ordre.TotalPris = lejeAftale.TotalPris; // Genberegner totalprisen.
            }

            _context.LejeAftaler.Update(lejeAftale); // Opdaterer lejeaftalen i databasen.
            await _context.SaveChangesAsync(); // Gemmer ændringerne.
            return lejeAftale; // Returnerer den opdaterede lejeaftale.
        }
    }
}