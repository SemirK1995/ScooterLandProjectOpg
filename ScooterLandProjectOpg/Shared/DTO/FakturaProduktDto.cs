using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.DTO
{
	public class FakturaProduktDto
	{
		public int ProduktId { get; set; }
		public string ProduktNavn { get; set; }
		public int Antal { get; set; }
		public double Pris { get; set; }
	}
}
