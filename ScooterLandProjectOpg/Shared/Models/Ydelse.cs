using System; // Indeholder grundlæggende datatyper og funktionalitet som DateTime.
using System.Collections.Generic; // Understøtter samlinger som List<T>.
using System.ComponentModel.DataAnnotations; // Indeholder valideringsattributter som [Key].
using System.Linq; // Understøtter LINQ-metoder til at udføre query-operationer.
using System.Text; // Understøtter tekstmanipulation.
using System.Threading.Tasks; // Understøtter asynkron programmering.

namespace ScooterLandProjectOpg.Shared.Models // Definerer det namespace, som klassen hører til.
{
    // Repræsenterer en ydelse, som kan knyttes til ordrer i systemet.
    public class Ydelse 
    {
        [Key] // Angiver, at YdelseId er primærnøglen for denne klasse i databasen.
        public int YdelseId { get; set; } // Unik identifikator for ydelsen.

        public string? Navn { get; set; } // Navnet på ydelsen, kan være null, hvis det ikke er angivet.
        
        public double? StandardPris { get; set; } // Standardprisen for ydelsen, kan være null, hvis ikke angivet.

        public List<OrdreYdelse>? OrdreYdelse { get; set; } = new List<OrdreYdelse>(); // Navigation property tilknyttet en liste af OrdreYdelse, initialiseret som en tom liste.

        public Ydelse() // Standardkonstruktør, som anvendes af Entity Framework eller andre værktøjer til objektinitialisering.
        {
        }
    }
}