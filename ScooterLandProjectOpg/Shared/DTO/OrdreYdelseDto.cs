using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.DTO
{
	public class OrdreYdelseDto
	{
		//Denne DTO er lavet for at kunne vise information omkring en specifik ydelse og hvilken scooter ydelsen laves på.
		public int OrdreYdelseId { get; set; }
		public string? YdelseNavn { get; set; } // Navn på ydelsen
		public string? ScooterMaerke { get; set; } // Scooterens mærke
		public string? ScooterModel { get; set; } // Scooterens model
		public string? ProduktionsAar { get; set; } // Scooterens årgang
		[Required(ErrorMessage = "Startdato er påkrævet.")]
		public DateTime? StartDato { get; set; } // Startdato for arbejdet
		[Required(ErrorMessage = "Slutdato er påkrævet.")]
		public DateTime? SlutDato { get; set; } // Slutdato for arbejdet

		[Range(1, int.MaxValue, ErrorMessage = "Timer skal være større end 0.")]
		public double? Timer { get; set; } // Timer for arbejdet
	}
}
