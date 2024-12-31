using System; // Importerer funktionalitet til arbejde med datoer og tid (ikke brugt direkte her).
using System.Collections.Generic; // Gør det muligt at arbejde med generiske kollektioner som List (ikke brugt direkte her).
using System.Linq; // Tilbyder LINQ-forespørgsler til datahåndtering (ikke brugt her).
using System.Text; // Indeholder klasser til tekstmanipulation (ikke brugt her).
using System.Threading.Tasks; // Tilbyder funktioner til asynkron programmering (ikke brugt her).

namespace ScooterLandProjectOpg.Shared.DTO // Definerer navnerummet for dataoverførselsobjekter (DTO'er) relateret til ScooterLand-projektet.
{
    // En klasse, der repræsenterer produktoplysninger til brug i en faktura.
    public class FakturaProduktDto 
    {
        public int ProduktId { get; set; } // ID for det produkt, der er købt.
        public string ProduktNavn { get; set; } // Navnet på det produkt, der er købt.
        public int Antal { get; set; } // Antallet af det pågældende produkt, der er købt.
        public double Pris { get; set; } // Prisen per enhed af produktet.
    }
}