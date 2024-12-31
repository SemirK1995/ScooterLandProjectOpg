using System; // Importerer System-biblioteket, som indeholder grundlæggende funktionalitet som dato og tid.
using System.Collections.Generic; // Forberedt til at arbejde med generiske kollektioner, selvom det ikke bruges her.
using System.Linq; // Tilbyder LINQ-metoder til databehandling, men bruges ikke i denne fil.
using System.Text; // Indeholder klasser til tekstmanipulation, men bruges ikke i denne fil.
using System.Threading.Tasks; // Tilbyder funktioner til asynkron programmering, men bruges ikke her.

namespace ScooterLandProjectOpg.Shared.DTO // Definerer navnerummet, som grupperer relaterede klasser og gør dem nemmere at organisere.
{
    // Definerer en Data Transfer Object (DTO)-klasse til opdatering af betalingsstatus.
    public class BetalingUpdateDto 
    {
        public bool Betalt { get; set; } // Egenskab, der angiver, om betalingen er betalt (true) eller ej (false).

        public DateTime? BetalingsDato { get; set; } // Egenskab, der angiver datoen for betalingen; kan være null, hvis datoen ikke er sat.
    }
}