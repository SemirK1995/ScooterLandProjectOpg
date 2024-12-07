using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.Models
{
	public class OrdreProdukt
	{
		[Key]
		public int OrdreProduktId { get; set; }

		// Fremmednøgler
		public int? OrdreId { get; set; }
		[ForeignKey("OrdreId")]
		public Ordre Ordre { get; set; }

		public int? ProduktId { get; set; }
		[ForeignKey("ProduktId")]
		public Produkt Produkt { get; set; }

		// Attributter
		public int Antal { get; set; } 
		public double Pris { get; set; } 

		public OrdreProdukt() 
		{
		}	
	}
}
