using System; // Importerer funktionalitet til håndtering af datoer og tid (ikke brugt her).
using System.Collections.Generic; // Gør det muligt at bruge generiske kollektioner som List (ikke brugt her).
using System.Linq; // Giver funktioner til LINQ-forespørgsler (ikke brugt her).
using System.Text; // Indeholder klasser til tekstmanipulation (ikke brugt her).
using System.Threading.Tasks; // Tilbyder funktioner til asynkron programmering (ikke brugt her).

namespace ScooterLandProjectOpg.Shared.DTO // Definerer navnerummet, som grupperer dataoverførselsobjekter (DTO'er).
{
    // Definerer en klasse til dataoverførsel af produkter i en ordre.
    public class CreateOrdreProduktDto 
    {
        public int ProduktId { get; set; } // Indeholder ID for det produkt, der tilføjes til ordren.
        public int KøbsAntal { get; set; } // Angiver antallet af det specifikke produkt, der købes i ordren.
        public double? Pris { get; set; } // Indeholder prisen for det specifikke produkt. Pris er valgfri og kan være null.
    }
}