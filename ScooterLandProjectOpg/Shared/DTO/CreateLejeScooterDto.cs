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
		public int LejeScooterId { get; set; }

		[Required(ErrorMessage = "Scootermodel er påkrævet.")]
        public string ScooterModel { get; set; }

        [Required(ErrorMessage = "Scootermærke er påkrævet.")]
        public string ScooterMaerke { get; set; }

        public string? RegistreringsNummer { get; set; }

        public DateTime? StartDato { get; set; }

        public DateTime? SlutDato { get; set; }

        public bool ErTilgængelig { get; set; } = true; // Standard: Tilgængelig

    }
}
