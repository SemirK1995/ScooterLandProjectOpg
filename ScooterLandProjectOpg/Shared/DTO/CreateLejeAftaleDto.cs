using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.DTO
{
    public class CreateLejeAftaleDto
    {

        public int KundeId { get; set; } // ID på kunden, der lejer scooteren

		public int LejeScooterId { get; set; }

		[Required(ErrorMessage = "Startdato er påkrævet.")]
        public DateTime? StartDato { get; set; }

        [Required(ErrorMessage = "Slutdato er påkrævet.")]
        public DateTime? SlutDato { get; set; }

        [Required(ErrorMessage = "Daglig leje er påkrævet.")]
        [Range(0, double.MaxValue, ErrorMessage = "Daglig leje skal være et positivt beløb.")]
        public double DagligLeje { get; set; }

        public double KilometerPris { get; set; } = 0.53;
        public double Selvrisiko { get; set; } = 1000; // Standardværdi
        public bool Forsikring { get; set; } // Valg af forsikring (true/false)
        public int? KortKilometer { get; set; } // Antal kilometer inkluderet i lejen

        // Ikke nødvendig at inkludere TotalPris, da den kan beregnes dynamisk på backend

    }
}
