using System; // Indeholder grundlæggende typer som DateTime.
using System.Collections.Generic; // Indeholder samlinger som List<T>.
using System.ComponentModel.DataAnnotations.Schema; // Indeholder attributter til databaserelationer som [ForeignKey].
using System.ComponentModel.DataAnnotations; // Indeholder valideringsattributter som [Key].
using System.Linq; // Indeholder LINQ-metoder som Sum.
using System.Text; // Indeholder funktioner til tekstmanipulation.
using System.Threading.Tasks; // Indeholder funktioner til asynkron programmering.
using ScooterLandProjectOpg.Shared.Enum; // Indeholder brugerdefinerede enums som OrdreStatus.

namespace ScooterLandProjectOpg.Shared.Models // Definerer namespace for klassen.
{
    // Repræsenterer en ordre i systemet.
    public class Ordre 
    {
        [Key] // Angiver, at denne property er primærnøgle i databasen.
        public int OrdreId { get; set; } // Unik identifikator for ordren.

        public int KundeId { get; set; } // Fremmednøgle til den relaterede kunde.

        [ForeignKey("KundeId")] // Angiver relationen mellem KundeId og Kunde.
        public Kunde Kunde { get; set; } // Navigation property til den relaterede kunde.

        [ForeignKey("LejeId")] // Angiver relationen mellem LejeId og LejeAftale.
        public int? LejeId { get; set; } // Fremmednøgle til en eventuel relateret lejeaftale.

        public LejeAftale? LejeAftale { get; set; } // Navigation property til den relaterede lejeaftale.

        public DateTime? Dato { get; set; } = DateTime.Now; // Dato for oprettelse af ordren, standard til nuværende tidspunkt.

        public OrdreStatus? Status { get; set; } = OrdreStatus.Oprettet; // Status for ordren, standard er 'Oprettet'.

        public double? TotalPris { get; set; } // Totalpris for ordren.

        [NotMapped] // Angiver, at denne property ikke skal gemmes i databasen.
        public double TotalOrdrePris // Beregner den samlede pris for ordren.
        {
            get
            {
                var ydelsesPris = OrdreYdelse?.Sum(oy => oy.BeregnetPris) ?? 0; // Samlet pris for ydelser.
                var lejeAftalePris = LejeAftale?.TotalPris ?? 0; // Pris for tilknyttet lejeaftale.
                var produktPris = OrdreProdukter?.Sum(op => op.Pris * op.Antal) ?? 0; // Samlet pris for produkter.
                return ydelsesPris + lejeAftalePris + produktPris; // Totalpris som summen af alle dele.
            }
        }

        public List<Betaling>? Betalinger { get; set; } = new List<Betaling>(); // Liste af betalinger relateret til ordren.

        public List<OrdreYdelse>? OrdreYdelse { get; set; } = new List<OrdreYdelse>(); // Liste af ydelser tilknyttet ordren.

        public List<OrdreProdukt>? OrdreProdukter { get; set; } = new List<OrdreProdukt>(); // Liste af produkter tilknyttet ordren.

        public Ordre() // Standardkonstruktør.
        {
        }
    }
}