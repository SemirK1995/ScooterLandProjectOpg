using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.Models
{
	public class OrdreYdelse
	{
		// Primary Key
		[Key]
		public int OrdreYdelseId { get; set; }

		// Foreign Key
		public int OrdreId { get; set; }
		// Navigation property
		[ForeignKey("OrdreId")]
		public Ordre Ordre { get; set; }

		// Foreign Key
		public int YdelseId { get; set; }
		// Navigation property
		[ForeignKey("YdelseId")]
		public Ydelse Ydelse { get; set; }
		
		//Attributter
		public double? Timer { get; set; }
		public DateTime? Dato { get; set; }
		public double? AftaltPris { get; set; }

		//Null constructor
		public OrdreYdelse()
		{
		}
	}
}
