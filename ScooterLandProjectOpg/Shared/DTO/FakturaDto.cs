using System; // Importerer funktionalitet til arbejde med datoer og tid.
using System.Collections.Generic; // Gør det muligt at arbejde med generiske kollektioner som List.
using System.Linq; // Giver LINQ-forespørgsler til datahåndtering (ikke brugt her).
using System.Text; // Indeholder klasser til tekstmanipulation (ikke brugt her).
using System.Threading.Tasks; // Tilbyder funktioner til asynkron programmering (ikke brugt her).

namespace ScooterLandProjectOpg.Shared.DTO // Definerer navnerummet for dataoverførselsobjekter (DTO'er) relateret til ScooterLand-projektet.
{
    // En klasse, der repræsenterer en faktura og dens indhold.
    public class FakturaDto 
    {
        // Kundeoplysninger
        public int KundeId { get; set; } // ID for den kunde, der modtager fakturaen.
        public string? KundeNavn { get; set; } // Navnet på kunden.
        public string? KundeAdresse { get; set; } // Kundens adresse.
        public string? KundeTelefon { get; set; } // Kundens telefonnummer.
        public string? KundeEmail { get; set; } // Kundens e-mailadresse.

        // Betaling
        public int BetalingsId { get; set; } // ID for betalingen.
        public int OrdreId { get; set; } // ID for den ordre, der er relateret til fakturaen.
        public double? Beløb { get; set; } // Beløb, der skal betales.
        public string? BetalingsMetode { get; set; } // Den anvendte betalingsmetode (fx kreditkort, kontant).
        public string? BetalingsDato { get; set; } // Dato for betaling.
        public bool Betalt { get; set; } // Angiver, om betalingen er gennemført.

        // Ordre
        public DateTime? OrdreDato { get; set; } // Dato for oprettelsen af ordren.
        public double? TotalPris { get; set; } // Den samlede pris for ordren.

        // Ydelser
        public List<FakturaYdelseDto>? Ydelser { get; set; } = new List<FakturaYdelseDto>(); // Liste over ydelser, der er inkluderet i fakturaen.

        // KundeScooter
        public string? KundeScooter { get; set; } // Beskrivelse af kundens scooter.

        // Lejeaftale
        public FakturaLejeAftaleDto? Lejeaftale { get; set; } // Detaljer om lejeaftalen, hvis relevant.

        // Produkter
        public List<FakturaProduktDto>? Produkter { get; set; } = new List<FakturaProduktDto>(); // Liste over produkter, der er inkluderet i fakturaen.
    }
}