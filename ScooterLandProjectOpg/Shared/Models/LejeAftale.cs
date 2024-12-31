using System; // Importerer grundlæggende funktionalitet som dato og tid.
using System.Collections.Generic; // Understøtter generiske samlinger som lister.
using System.ComponentModel.DataAnnotations.Schema; // Understøtter definition af database-relationer og fremmednøgler.
using System.ComponentModel.DataAnnotations; // Tilføjer funktionalitet til validering og metadata.
using System.Linq; // Tilbyder LINQ-funktionalitet til forespørgsler over samlinger.
using System.Text; // Giver funktionalitet til tekstmanipulation.
using System.Threading.Tasks; // Understøtter asynkron programmering.
using ScooterLandProjectOpg.Shared.Enum; // Importerer enums relateret til LejeAftale.

namespace ScooterLandProjectOpg.Shared.Models // Definerer namespace til organisering af projektets modeller.
{
    // Repræsenterer en lejeaftale mellem en kunde og virksomheden.
    public class LejeAftale 
    {
        [Key] // Marker LejeId som primærnøgle i databasen.
        public int? LejeId { get; set; } // Unik identifikator for lejeaftalen.

        public int KundeId { get; set; } // Fremmednøgle, der refererer til en kunde.

        [ForeignKey("KundeId")] // Angiver, at KundeId er en fremmednøgle, der refererer til Kunde.
        public Kunde Kunde { get; set; } // Navigationsegenskab til den relaterede kunde.

        [Required(ErrorMessage = "Startdato er påkrævet.")] // Validerer, at StartDato ikke er null.
        public DateTime? StartDato { get; set; } // Dato for starten af lejeaftalen.

        [Required(ErrorMessage = "Slutdato er påkrævet.")] // Validerer, at SlutDato ikke er null.
        public DateTime? SlutDato { get; set; } // Dato for afslutningen af lejeaftalen.

        [Required(ErrorMessage = "Daglig leje er påkrævet.")] // Validerer, at DagligLeje ikke er null.
        [Range(0, double.MaxValue, ErrorMessage = "Daglig leje skal være et positivt beløb.")] // Sikrer, at DagligLeje er positiv.
        public double DagligLeje { get; set; } // Beløb for daglig leje.

        [Required] // Gør ForsikringsPris obligatorisk.
        public double ForsikringsPris { get; set; } = 50; // Standard forsikringspris per dag.

        [Range(0, double.MaxValue, ErrorMessage = "Kilometerpris skal være et positivt beløb.")] // Sikrer, at KilometerPris er positiv.
        public double KilometerPris { get; set; } = 0.53; // Kilometerpris per kørte kilometer.

        [Range(0, double.MaxValue, ErrorMessage = "Selvrisiko skal være et positivt beløb.")] // Sikrer, at Selvrisiko er positiv.
        public double Selvrisiko { get; set; } // Beløb for selvrisiko.

        [Range(0, int.MaxValue, ErrorMessage = "Kort kilometer skal være et positivt tal.")] // Sikrer, at KortKilometer er positiv.
        public int? KortKilometer { get; set; } // Antal kørte kilometer i aftalen.

        public LejeAftaleStatus? Status { get; set; } // Status for lejeaftalen, fx Aktiv eller Afsluttet.

        [NotMapped] // Marker TotalPris som et beregnet felt, der ikke gemmes i databasen.
        public double TotalPris
        {
            get
            {
                var dage = (SlutDato - StartDato)?.Days ?? 0; // Beregner antal dage for aftalen.
                var forsikringsOmkostning = ForsikringsPris * dage; // Beregner forsikringsomkostning.
                var kilometerOmkostning = KortKilometer.HasValue ? KilometerPris * KortKilometer.Value : 0; // Beregner kilometeromkostning.

                return (DagligLeje * dage) + forsikringsOmkostning + kilometerOmkostning + Selvrisiko; // Returnerer samlet pris.
            }
        }

        public List<LejeScooter>? LejeScooter { get; set; } // Liste over scootere knyttet til lejeaftalen.

        [InverseProperty("LejeAftale")] // Definerer den omvendte relation til Ordre-modellen.
        public Ordre? Ordre { get; set; } // Relateret ordre til denne lejeaftale.

        public LejeAftale() // Standardkonstruktør for EF.
        {
        }
    }
}