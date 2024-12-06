using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.DTO
{
	public class CreateOrdreProduktDto
	{
		public int ProduktId { get; set; } // ID for produktet
		public int Antal { get; set; } // Antal af produktet i ordren
		public double? Pris { get; set; } // Pris for produktet i ordren (kan være rabatpris)
	}
}
