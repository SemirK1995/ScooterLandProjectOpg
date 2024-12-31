using System; // Importerer funktionalitet til arbejde med datoer og tid.
using System.Collections.Generic; // Gør det muligt at arbejde med generiske kollektioner som List (ikke brugt her).
using System.Linq; // Giver LINQ-forespørgsler til datahåndtering (ikke brugt her).
using System.Text; // Indeholder klasser til tekstmanipulation (ikke brugt her).
using System.Threading.Tasks; // Tilbyder funktioner til asynkron programmering (ikke brugt her).

namespace ScooterLandProjectOpg.Shared.DTO // Definerer navnerummet for dataoverførselsobjekter (DTO'er) relateret til ScooterLand-projektet.
{
    // En klasse designet til at repræsentere et produkt, der skal oprettes.
    public class CreateProduktDto 
    {
        public int ProduktId { get; set; } // Unikt ID for det specifikke produkt.
        public string? ProduktNavn { get; set; } // Navn på produktet; kan være null, hvis navnet ikke er obligatorisk.
        public double? Pris { get; set; } // Enhedsprisen for produktet; kan være null, hvis prisen ikke er obligatorisk.
    }
}