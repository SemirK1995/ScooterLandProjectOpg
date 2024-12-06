using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.DTO
{
	public class CreateProduktDto
	{
		public int ProduktId { get; set; } // ID for det valgte produkt
		public string? ProduktNavn { get; set; } // Antal af produktet
		public double? Pris { get; set; } // Enhedspris for produktet (kan være null)
	}
}
