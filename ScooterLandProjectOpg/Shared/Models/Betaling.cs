using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScooterLandProjectOpg.Shared.Enum;

namespace ScooterLandProjectOpg.Shared.Models
{
	public class Betaling
	{
		// Primary Key
		[Key]
		public int BetalingsId { get; set; }

		// Foreign Key
		public int OrdreId { get; set; }

		// Navigation property
		[ForeignKey("OrdreId")]
		public Ordre Ordre { get; set; }

		//Attributter
		public DateTime? BetalingsDato { get; set; }
		public double? Beløb { get; set; }
		public BetalingsMetodeStatus? BetalingsMetode { get; set; }
		public bool Betalt { get; set; } = false;



		//Null constructor til EF
		public Betaling()
		{
		}
	}
}
