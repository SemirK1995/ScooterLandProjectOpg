using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.DTO
{
	public class OrdreYdelseDto
	{
		public int OrdreYdelseId { get; set; }
		public string? YdelseNavn { get; set; } // Navn på ydelsen
		public string? ScooterMaerke { get; set; } // Scooterens mærke
		public string? ScooterModel { get; set; } // Scooterens model
		public string? ProduktionsAar { get; set; } // Scooterens årgang
		public DateTime? StartDato { get; set; } // Startdato for arbejdet
		public DateTime? SlutDato { get; set; } // Slutdato for arbejdet
		public double? Timer { get; set; } // Timer for arbejdet
	}
}
