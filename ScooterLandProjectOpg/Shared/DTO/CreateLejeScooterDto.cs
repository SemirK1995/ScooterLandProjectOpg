using System; // Importerer funktioner til håndtering af datoer og tid.
using System.Collections.Generic; // Gør det muligt at bruge generiske kollektioner som List.
using System.ComponentModel.DataAnnotations; // Tilbyder valideringsattributter til modeller.
using System.Linq; // Giver mulighed for LINQ-forespørgsler, selvom det ikke bruges her.
using System.Text; // Indeholder klasser til tekstmanipulation, men bruges ikke her.
using System.Threading.Tasks; // Tilbyder funktioner til asynkron programmering, men bruges ikke her.

namespace ScooterLandProjectOpg.Shared.DTO // Definerer navnerummet for DTO-klassen, som bruges til dataoverførsel.
{
    // Definerer en DTO-klasse til at oprette eller overføre data om en leje-scooter.
    public class CreateLejeScooterDto 
    {
        public int LejeScooterId { get; set; } // Indeholder ID'et for den specifikke leje-scooter.

        [Required(ErrorMessage = "Scootermodel er påkrævet.")] // Validerer, at Scootermodel ikke er null eller tom og angiver en fejlbesked.
        
        public string ScooterModel { get; set; } // Modelnavn på scooteren.

        [Required(ErrorMessage = "Scootermærke er påkrævet.")] // Validerer, at Scootermærke ikke er null eller tom og angiver en fejlbesked.
        
        public string ScooterMaerke { get; set; } // Mærket på scooteren.

        public string? RegistreringsNummer { get; set; } // Registreringsnummer for scooteren, kan være null.

        public DateTime? StartDato { get; set; } // Startdato for scooteren, kan være null.

        public DateTime? SlutDato { get; set; } // Slutdato for scooteren, kan være null.

        public bool ErTilgængelig { get; set; } = true; // Angiver, om scooteren er tilgængelig for leje. Standardværdien er true.
    }
}