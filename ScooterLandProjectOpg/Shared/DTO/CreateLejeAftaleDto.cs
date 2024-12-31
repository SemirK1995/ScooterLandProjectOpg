using System; // Importerer grundlæggende funktionalitet som dato og tid.
using System.Collections.Generic; // Gør det muligt at bruge generiske kollektioner som List.
using System.ComponentModel.DataAnnotations; // Tilbyder funktioner til validering af data, som f.eks. attributter til modelvalidering.
using System.Linq; // Tilbyder LINQ-metoder til databehandling, selvom det ikke bruges her.
using System.Text; // Indeholder klasser til tekstmanipulation, men bruges ikke her.
using System.Threading.Tasks; // Tilbyder funktioner til asynkron programmering, men bruges ikke her.

namespace ScooterLandProjectOpg.Shared.DTO // Definerer navnerummet for DTO-klassen, som bruges til dataoverførsel.
{
    // Definerer en Data Transfer Object (DTO)-klasse for at oprette en lejeaftale.
    public class CreateLejeAftaleDto 
    {
        public int KundeId { get; set; } // Indeholder ID'et på kunden, der opretter lejeaftalen.

        public int LejeScooterId { get; set; } // Indeholder ID'et på den scooter, der skal lejes.

        [Required(ErrorMessage = "Startdato er påkrævet.")] // Marker startdato som påkrævet og angiv en fejlbesked.
        public DateTime? StartDato { get; set; } // Startdato for lejeaftalen, kan være null.

        [Required(ErrorMessage = "Slutdato er påkrævet.")] // Marker slutdato som påkrævet og angiv en fejlbesked.
        public DateTime? SlutDato { get; set; } // Slutdato for lejeaftalen, kan være null.

        [Required(ErrorMessage = "Daglig leje er påkrævet.")] // Marker daglig leje som påkrævet og angiv en fejlbesked.
        [Range(0, double.MaxValue, ErrorMessage = "Daglig leje skal være et positivt beløb.")] // Sørger for, at daglig leje er et positivt tal.
        
        public double DagligLeje { get; set; } // Angiver prisen for daglig leje.

        public double ForsikringsPris { get; set; } = 50; // Standardforsikringspris for lejeaftalen, kan overskrives.

        public double KilometerPris { get; set; } = 0.53; // Standardpris pr. kilometer, kan overskrives.

        public int? KortKilometer { get; set; } // Antal inkluderede kilometer i lejeaftalen, kan være null.

        public List<int> Scootere { get; set; } = new List<int>(); // Liste over scooter-ID'er, der er en del af aftalen, initialiseret som en tom liste.
    }
}