using System; // Inkluderer funktioner til arbejde med datoer og tider.
using System.Collections.Generic; // Understøtter brug af generiske samlinger som List.
using System.Linq; // Indeholder LINQ-metoder til datahåndtering.
using System.Text; // Tilbyder funktioner til tekstmanipulation.
using System.Threading.Tasks; // Understøtter asynkron programmering.

namespace ScooterLandProjectOpg.Shared.DTO // Definerer navnerummet for Data Transfer Objects i projektet.
{
    // En klasse, der er designet til at tildele en mekaniker en arbejdsopgave.
    public class TildelMekanikerDto 
    {
        public int OrdreYdelseId { get; set; } // ID for den ordreydelse, som mekanikeren skal tildeles.

        public int MekanikerId { get; set; } // ID for mekanikeren, der skal udføre arbejdsopgaven.

        public DateTime? StartDato { get; set; } // Startdato for mekanikerens arbejdsopgave, hvis angivet.

        public DateTime? SlutDato { get; set; } // Slutdato for mekanikerens arbejdsopgave, hvis angivet.

        public double? Timer { get; set; } // Antallet af timer, der er beregnet eller brugt på arbejdsopgaven, hvis tilgængeligt.
    }
}