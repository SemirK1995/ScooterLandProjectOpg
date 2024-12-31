using System; // Indeholder grundlæggende typer og funktioner som DateTime.
using System.Collections.Generic; // Til arbejde med samlinger som lister.
using System.ComponentModel.DataAnnotations.Schema; // Til brug af attributter relateret til databasen.
using System.ComponentModel.DataAnnotations; // Til brug af valideringsattributter.
using System.Linq; // Indeholder LINQ-funktionalitet.
using System.Text; // Til tekstmanipulation.
using System.Threading.Tasks; // Til asynkrone operationer.

namespace ScooterLandProjectOpg.Shared.Models // Definerer det namespace, hvor klassen hører hjemme.
{
    // Definerer en klasse, der repræsenterer en scooter til leje.
    public class LejeScooter 
    {
        [Key] // Angiver, at denne property er primærnøglen i databasen.
        public int LejeScooterId { get; set; } // Unik identifikator for scooteren.

        public int? LejeId { get; set; } // Angiver den relaterede lejeaftales ID, hvis relevant.

        [ForeignKey("LejeId")] // Angiver, at LejeId er en fremmednøgle, der refererer til LejeAftale.
        public LejeAftale? LejeAftale { get; set; } // Navigation property, der forbinder scooteren med en specifik lejeaftale.

        public string ScooterModel { get; set; } // Modelnavnet på scooteren.

        public string ScooterMaerke { get; set; } // Mærket på scooteren.

        public string? RegistreringsNummer { get; set; } // Scooteren kan have et registreringsnummer, men det er valgfrit.

        public bool ErTilgængelig { get; set; } = true; // Angiver, om scooteren er ledig til leje, standard er true (ledig).

        public DateTime? StartDato { get; set; } // Startdatoen for, hvornår scooteren er tilgængelig.

        public DateTime? SlutDato { get; set; } // Slutdatoen for, hvornår scooteren er tilgængelig.

        public LejeScooter() // Standardkonstruktør.
        {
        }
    }
}