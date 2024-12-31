using System; // Indeholder grundlæggende typer og funktioner som DateTime.
using System.Collections.Generic; // Til arbejde med samlinger som lister.
using System.ComponentModel.DataAnnotations; // Til valideringsattributter som [Key].
using System.Linq; // Indeholder LINQ-funktionalitet.
using System.Text; // Til tekstmanipulation.
using System.Threading.Tasks; // Til asynkrone operationer.

namespace ScooterLandProjectOpg.Shared.Models // Definerer namespace for klassen.
{
    // Definerer en klasse, der repræsenterer en mekaniker.
    public class Mekaniker 
    {
        [Key] // Angiver, at denne property er primærnøglen i databasen.
        public int MekanikerId { get; set; } // Unik identifikator for mekanikeren.

        public string? Navn { get; set; } // Navnet på mekanikeren. Kan være null.

        public int? Telefonnummer { get; set; } // Telefonnummeret til mekanikeren. Kan være null.

        public string? Speciale { get; set; } // Mekanikerens speciale eller ekspertiseområde. Kan være null.

        public Mekaniker() // Standardkonstruktør, der tillader oprettelse af en tom instans af klassen.
        {
        }
    }
}