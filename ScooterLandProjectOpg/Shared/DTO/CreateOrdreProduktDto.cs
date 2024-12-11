using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.DTO
{
	public class CreateOrdreProduktDto
	{
		public int ProduktId { get; set; } 
		public int KøbsAntal { get; set; } 
		public double? Pris { get; set; } 
	}
}
