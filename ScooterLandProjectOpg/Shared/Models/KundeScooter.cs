using System; // Importerer grundlæggende funktionalitet som dato og tid.
using System.Collections.Generic; // Understøtter generiske samlinger som lister.
using System.ComponentModel.DataAnnotations.Schema; // Understøtter definition af database-relationer og fremmednøgler.
using System.ComponentModel.DataAnnotations; // Tilføjer funktionalitet til validering og metadata.
using System.Linq; // Tilbyder LINQ-funktionalitet til forespørgsler over samlinger.
using System.Text; // Giver funktionalitet til tekstmanipulation.
using System.Threading.Tasks; // Understøtter asynkron programmering.

namespace ScooterLandProjectOpg.Shared.Models // Definerer namespace til organisering af projektets modeller.
{
    // Repræsenterer en scooter, der er ejet af en kunde.
    public class KundeScooter 
    {
        [Key] // Marker ScooterId som primærnøgle i databasen.
        public int ScooterId { get; set; } // Unik identifikator for scooteren.

        public int KundeId { get; set; } // Definerer fremmednøgle til en kunde.

        [ForeignKey("KundeId")] // Angiver, at KundeId er en fremmednøgle, der refererer til Kunde.
        public Kunde? Kunde { get; set; } // Navigationsegenskab til den relaterede kunde.

        public string? Maerke { get; set; } // Scooterens mærke.
        
        public string? Model { get; set; } // Scooterens model.

        [Required(ErrorMessage = "Registreringsnummer er påkrævet.")] // Markerer registreringsnummer som obligatorisk.
        [RegularExpression(@"^[A-Za-z]{2}[0-9]{3}$", ErrorMessage = "Registreringsnummer skal bestå af præcis 2 bogstaver efterfulgt af 3 tal.")] // Validerer formatet for registreringsnummeret.
        public string? RegistreringsNummer { get; set; } // Scooterens registreringsnummer.

        [Required(ErrorMessage = "Produktionsår er påkrævet.")] // Marker produktionsår som obligatorisk.
        [Range(1000, 9999, ErrorMessage = "Produktionsår skal være et gyldigt 4-cifret tal.")] // Sikrer, at produktionsår er et gyldigt 4-cifret tal.
        public int? ProduktionsAar { get; set; } // Scooterens produktionsår.

        public KundeScooter() // Standardkonstruktør for klassen.
        {
        }
    }
}