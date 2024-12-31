using System; // Indeholder grundlæggende typer som DateTime.
using System.Collections.Generic; // Indeholder samlinger som List<T>.
using System.ComponentModel.DataAnnotations.Schema; // Indeholder attributter til databaserelationer som [ForeignKey].
using System.ComponentModel.DataAnnotations; // Indeholder valideringsattributter som [Key].
using System.Linq; // Indeholder LINQ-metoder til query-operationer.
using System.Text; // Indeholder funktioner til tekstmanipulation.
using System.Threading.Tasks; // Understøtter asynkron programmering.

namespace ScooterLandProjectOpg.Shared.Models // Definerer namespace for klassen.
{
    // Repræsenterer en relation mellem en ordre og et produkt.
    public class OrdreProdukt 
    {
        [Key] // Angiver, at denne property er primærnøgle i databasen.
        public int OrdreProduktId { get; set; } // Unik identifikator for OrdreProdukt.

        public int? OrdreId { get; set; } // Fremmednøgle, der refererer til en ordre.

        [ForeignKey("OrdreId")] // Angiver, at OrdreId er en fremmednøgle relateret til Ordre-tabellen.
        public Ordre Ordre { get; set; } // Navigation property for den relaterede ordre.

        public int? ProduktId { get; set; } // Fremmednøgle, der refererer til et produkt.

        [ForeignKey("ProduktId")] // Angiver, at ProduktId er en fremmednøgle relateret til Produkt-tabellen.
        public Produkt Produkt { get; set; } // Navigation property for det relaterede produkt.

        public int Antal { get; set; } // Antallet af dette produkt i ordren.

        public double Pris { get; set; } // Prisen pr. enhed for produktet.

        public OrdreProdukt() // Standardkonstruktør.
        {
        }
    }
}