using System; // Indeholder grundlæggende typer som DateTime.
using System.Collections.Generic; // Understøtter samlinger som List<T>.
using System.ComponentModel.DataAnnotations.Schema; // Understøtter attributter til databaserelationer som [ForeignKey].
using System.ComponentModel.DataAnnotations; // Indeholder valideringsattributter som [Key].
using System.Linq; // Indeholder LINQ-metoder til query-operationer.
using System.Text; // Understøtter tekstmanipulation.
using System.Threading.Tasks; // Understøtter asynkron programmering.

namespace ScooterLandProjectOpg.Shared.Models // Definerer namespace for klassen.
{
    // Repræsenterer en specifik ydelse i forbindelse med en ordre.
    public class OrdreYdelse 
    {
        [Key] // Angiver, at denne property er primærnøgle i databasen.
        public int OrdreYdelseId { get; set; } // Unik identifikator for OrdreYdelse.

        public int OrdreId { get; set; } // Fremmednøgle, der refererer til en ordre.
        [ForeignKey("OrdreId")] // Angiver, at OrdreId er en fremmednøgle relateret til Ordre-tabellen.
        public Ordre Ordre { get; set; } // Navigation property for den relaterede ordre.

        public int YdelseId { get; set; } // Fremmednøgle, der refererer til en ydelse.
        [ForeignKey("YdelseId")] // Angiver, at YdelseId er en fremmednøgle relateret til Ydelse-tabellen.
        public Ydelse Ydelse { get; set; } // Navigation property for den relaterede ydelse.

        public int? ScooterId { get; set; } // Fremmednøgle, der refererer til en scooter.
        [ForeignKey("ScooterId")] // Angiver, at ScooterId er en fremmednøgle relateret til KundeScooter-tabellen.
        public KundeScooter? Scooter { get; set; } // Navigation property for den relaterede scooter.

        public int? MekanikerId { get; set; } // Fremmednøgle, der refererer til en mekaniker.
        [ForeignKey("MekanikerId")] // Angiver, at MekanikerId er en fremmednøgle relateret til Mekaniker-tabellen.
        public Mekaniker? Mekaniker { get; set; } // Navigation property for den relaterede mekaniker.

        public DateTime? StartDato { get; set; } // Startdato for arbejdet.
        
        public DateTime? SlutDato { get; set; } // Slutdato for arbejdet.
        
        public double? Timer { get; set; } // Antal timer arbejdet forventes at tage.
        
        public DateTime? Dato { get; set; } // Datoen, hvor ydelsen blev oprettet.
        
        public double? AftaltPris { get; set; } // Den aftalte pris for ydelsen.

        [NotMapped] // Angiver, at denne property ikke skal gemmes i databasen.
        public double BeregnetPris // Udregner prisen for ydelsen.
        {
            get
            {
                // Hvis AftaltPris er angivet og større end 0, brug den. Ellers brug ydelsens StandardPris.
                return AftaltPris.HasValue && AftaltPris > 0
                    ? AftaltPris.Value
                    : Ydelse?.StandardPris ?? 0;
            }
        }

        public OrdreYdelse() // Standardkonstruktør.
        {
        }
    }
}