using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.DTO
{
    public class CreateLejeScooterDto
    {
        [Required(ErrorMessage = "Scootermodel er påkrævet.")]
        public string ScooterModel { get; set; }

        [Required(ErrorMessage = "Scootermærke er påkrævet.")]
        public string ScooterMaerke { get; set; }

        public string? RegistreringsNummer { get; set; }

        [Required(ErrorMessage = "Startdato for leje af scooter er påkrævet.")]
        public DateTime? StartDato { get; set; }

        [Required(ErrorMessage = "Slutdato for leje af scooter er påkrævet.")]
        public DateTime? SlutDato { get; set; }

        public bool ErTilgængelig { get; set; } = true; // Standard: Tilgængelig

        public int LejeScooterId { get; set; }
    }
}
