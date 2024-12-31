using System; // Importerer funktionalitet til arbejde med datoer og tid.
using System.Collections.Generic; // Gør det muligt at arbejde med generiske kollektioner som List.
using System.Linq; // Tilbyder LINQ-forespørgsler til datahåndtering (ikke brugt her).
using System.Text; // Indeholder klasser til tekstmanipulation (ikke brugt her).
using System.Threading.Tasks; // Tilbyder funktioner til asynkron programmering (ikke brugt her).

namespace ScooterLandProjectOpg.Shared.DTO // Definerer navnerummet for dataoverførselsobjekter (DTO'er) relateret til ScooterLand-projektet.
{
    // En klasse, der repræsenterer data for en lejeaftale, som skal vises i en faktura.
    public class FakturaLejeAftaleDto 
    {
        public DateTime? StartDato { get; set; } // Dato for lejeaftalens start.
        public DateTime? SlutDato { get; set; } // Dato for lejeaftalens afslutning.
        public double? ForsikringsPris { get; set; } // Prisen for forsikring relateret til lejeaftalen.
        public double KilometerPris { get; set; } = 0.53; // Pris per kilometer, standard er 0.53.
        public int? KortKilometer { get; set; } // Antallet af kørte kilometer i lejeaftalen.
        public double? DagligLeje { get; set; } // Pris for daglig leje af scooteren.
        public double? Selvrisiko { get; set; } // Selvrisikoen, hvis lejeaftalen omfatter skader.
        public List<string> Scootere { get; set; } = new List<string>(); // Liste over scootere, der er inkluderet i lejeaftalen.
    }
}