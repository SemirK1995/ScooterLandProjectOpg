using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.DTO
{
	public class FakturaProduktDto
	{
		//Denne DTO er lavet for at kunne vise hvilket produkt/produkter der er købt så det kan vises i en faktura. 
		public int ProduktId { get; set; }
		public string ProduktNavn { get; set; }
		public int Antal { get; set; }
		public double Pris { get; set; }
	}
}
