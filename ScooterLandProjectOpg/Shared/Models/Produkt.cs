using System; // Indeholder grundlæggende datatyper som DateTime.
using System.Collections.Generic; // Understøtter samlinger som List<T>.
using System.ComponentModel.DataAnnotations; // Indeholder valideringsattributter som [Key].
using System.ComponentModel.DataAnnotations.Schema; // Understøtter attributter til databaserelationer som [ForeignKey].
using System.Linq; // Understøtter LINQ-metoder til query-operationer.
using System.Text; // Understøtter tekstmanipulation.
using System.Threading.Tasks; // Understøtter asynkron programmering.

namespace ScooterLandProjectOpg.Shared.Models // Definerer namespace for klassen.
{
    // Repræsenterer et produkt i systemet.
    public class Produkt 
    {
        [Key] // Angiver, at denne property er primærnøgle i databasen.
        public int ProduktId { get; set; } // Unik identifikator for produktet.

        public string? ProduktNavn { get; set; } // Navn på produktet, kan være null hvis ikke angivet.
        
        public double? Pris { get; set; } // Prisen på produktet, kan være null hvis ikke angivet.
        
        public int? LagerAntal { get; set; } // Antal enheder af produktet på lager, kan være null hvis ikke angivet.
        
        public int KøbsAntal { get; set; } = 1; // Standardkøbsantal for produktet, som starter med 1.

        public Produkt() // Standardkonstruktør, bruges af Entity Framework og andre systemer til initialisering.
        {

        }
    }
}