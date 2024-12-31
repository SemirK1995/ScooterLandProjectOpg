using System; // Importerer grundlæggende funktionalitet som dato og tid.
using System.Collections.Generic; // Understøtter generiske samlinger som lister.
using System.ComponentModel.DataAnnotations; // Giver funktionalitet til validering og metadata.
using System.Linq; // Tilbyder LINQ-funktionalitet for forespørgsler over samlinger.
using System.Text; // Giver funktionalitet til tekstmanipulation.
using System.Threading.Tasks; // Understøtter asynkron programmering.

namespace ScooterLandProjectOpg.Shared.Models // Definerer namespace til organisering af modeller i projektet.
{
    // Definerer klassen Kunde, som repræsenterer en kunde i systemet.
    public class Kunde 
    {
        [Key] // Markerer KundeId som primærnøgle i databasen.
        public int KundeId { get; set; } // Unik identifikator for kunden.

        [Required(ErrorMessage = "Navn er påkrævet.")] // Markerer, at feltet Navn er obligatorisk.
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Navn skal være mellem 2 og 50 tegn.")] // Begrænser længden af navnet.
        [RegularExpression(@"^[A-Za-zÆØÅæøå\s-]+$", ErrorMessage = "Navn må kun indeholde bogstaver og mellemrum.")] // Validerer, at navnet kun indeholder tilladte tegn.
        public string? Navn { get; set; } // Kundens navn.

        [Required(ErrorMessage = "Adresse er påkrævet.")] // Marker adresse som obligatorisk.
        [StringLength(200, ErrorMessage = "Adresse må ikke overstige 200 tegn.")] // Begrænser længden af adressen.
        public string? Adresse { get; set; } // Kundens adresse.

        [Required(ErrorMessage = "Telefonnummer er påkrævet.")] // Marker telefonnummer som obligatorisk.
        [Range(10000000, 99999999, ErrorMessage = "Telefonnummer skal være et gyldigt 8-cifret tal.")] // Sikrer, at telefonnummeret er 8 cifre.
        public int? Telefonnummer { get; set; } // Kundens telefonnummer.

        [Required(ErrorMessage = "Email er påkrævet.")] // Marker email som obligatorisk.
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Ugyldig email-adresse. Eksempel: eksempel@mail.com")] // Validerer e-mail-adressen.
        public string? Email { get; set; } // Kundens email.

        public List<KundeScooter>? KundeScooter { get; set; } = new List<KundeScooter>(); // Navigationsegenskab til en liste af Kundens scootere.
        
        public List<LejeAftale>? LejeAftale { get; set; } = new List<LejeAftale>(); // Navigationsegenskab til en liste af Kundens lejeaftaler.
        
        public List<Ordre>? Ordre { get; set; } = new List<Ordre>(); // Navigationsegenskab til en liste af Kundens ordrer.

        public Kunde() // Standardkonstruktør for klassen Kunde.
        {
        }
    }
}