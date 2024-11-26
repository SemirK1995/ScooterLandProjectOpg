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

        [Required(ErrorMessage = "Startdato er påkrævet.")]
        public DateTime StartDato { get; set; }

        [Required(ErrorMessage = "Slutdato er påkrævet.")]
        public DateTime SlutDato { get; set; }

        [Required(ErrorMessage = "Daglig leje er påkrævet.")]
        [Range(0, double.MaxValue, ErrorMessage = "Daglig leje skal være et positivt beløb.")]
        public double DagligLeje { get; set; }

        public bool Forsikring { get; set; } // Valg af forsikring (true/false)
        public int? KortKilometer { get; set; } // Antal kilometer inkluderet i lejen

        public List<CreateLejeScooterDto>? LejeScootere { get; set; }

        // Ikke nødvendig at inkludere TotalPris, da den kan beregnes dynamisk på backend

    }
}
