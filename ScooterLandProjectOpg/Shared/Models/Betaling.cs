using System; // Inkluderer grundlæggende funktionalitet som datatyper og systemværktøjer.
using System.Collections.Generic; // Giver funktionalitet til generiske samlinger.
using System.ComponentModel.DataAnnotations.Schema; // Muliggør mapping af klasser til database-tabeller.
using System.ComponentModel.DataAnnotations; // Giver funktionalitet til validering og metadata.
using System.Linq; // Tilbyder LINQ-funktionalitet for forespørgsler over samlinger.
using System.Text; // Giver klasser til tekstmanipulation.
using System.Threading.Tasks; // Understøtter asynkron programmering og opgaver.
using ScooterLandProjectOpg.Shared.Enum; // Inkluderer enums defineret i projektet.

namespace ScooterLandProjectOpg.Shared.Models // Definerer et namespace til organisering af modellerne i projektet.
{
    // Definerer klassen Betaling, der repræsenterer en betaling i systemet.
    public class Betaling 
    {
        [Key] // Angiver, at BetalingsId er primærnøglen i databasen.
        public int BetalingsId { get; set; } // Unik identifikator for betalingen.

        public int OrdreId { get; set; } // Refererer til den tilknyttede ordre via dens ID.

        [ForeignKey("OrdreId")] // Angiver en fremmed nøgle til Ordre-tabellen.
        public Ordre Ordre { get; set; } // Navigationsegenskab til den relaterede ordre.

        public DateTime? BetalingsDato { get; set; } // Dato for betalingen. Kan være null, hvis betalingen ikke er gennemført.
        
        public double? Beløb { get; set; } // Det beløb, der skal betales. Kan være null, hvis ikke specificeret.
        
        public BetalingsMetodeStatus? BetalingsMetode { get; set; } // Betalingsmetode som Mobilepay, Kontant osv. Bruger en enum.
        
        public bool Betalt { get; set; } = false; // Angiver, om betalingen er gennemført. Standard er false.

        public Betaling() // Standardkonstruktør for Betaling-klassen.
        {
        }
    }
}