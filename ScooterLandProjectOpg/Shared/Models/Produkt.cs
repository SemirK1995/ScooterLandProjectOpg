using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.Models
{
	public class Produkt
	{
		//PK
		[Key]
		public int ProduktId { get; set; }

		//Attributter
		public string? ProduktNavn { get; set; }
		public double? Pris { get; set; }
		public int? Antal { get; set; }

		public Produkt()
		{

		}

	}

}
