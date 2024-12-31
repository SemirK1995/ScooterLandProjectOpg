using System; // Inkluderer funktionalitet til arbejde med datoer og tider.
using System.Collections.Generic; // Tillader brug af generiske samlinger som List.
using System.ComponentModel.DataAnnotations; // Indeholder funktioner til validering af data, f.eks. Required og Range.
using System.Linq; // Giver LINQ-metoder til datasæt.
using System.Text; // Indeholder funktioner til tekstmanipulation.
using System.Threading.Tasks; // Understøtter asynkron programmering.

namespace ScooterLandProjectOpg.Shared.DTO // Definerer navnerummet for Data Transfer Objects, der er del af projektet.
{
    // En klasse, der definerer en DTO til repræsentation af detaljer om en ordreydelse.
    public class OrdreYdelseDto 
    {
        public int OrdreYdelseId { get; set; } // ID, der entydigt identificerer ordreydelsen.

        public string? YdelseNavn { get; set; } // Navnet på ydelsen, f.eks. service eller reparation.

        public string? ScooterMaerke { get; set; } // Mærke på scooteren, der er forbundet med ydelsen.

        public string? ScooterModel { get; set; } // Model på scooteren, der er forbundet med ydelsen.

        public string? ProduktionsAar { get; set; } // Årgangen for scooteren, hvis det er relevant.

        [Required(ErrorMessage = "Startdato er påkrævet.")] // Validering, der kræver en startdato.
        public DateTime? StartDato { get; set; } // Startdato for det arbejde, der skal udføres.

        [Required(ErrorMessage = "Slutdato er påkrævet.")] // Validering, der kræver en slutdato.
        public DateTime? SlutDato { get; set; } // Slutdato for det arbejde, der skal udføres.

        [Range(1, int.MaxValue, ErrorMessage = "Timer skal være større end 0.")] // Validering for at sikre, at timer er positivt.
        public double? Timer { get; set; } // Antal timer brugt på arbejdet, kan være null hvis ikke oplyst.
    }
}