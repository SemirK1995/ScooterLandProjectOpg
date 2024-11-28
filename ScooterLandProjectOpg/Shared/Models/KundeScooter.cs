using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.Models
{
	public class KundeScooter
	{
		// Primary Key
		[Key]
		public int ScooterId { get; set; }

		// Foreign Key
		public int KundeId { get; set; }

		// Navigation property
		[ForeignKey("KundeId")]
		public Kunde? Kunde { get; set; }

		//Attributter
		public string? Maerke { get; set; }
		public string? Model { get; set; }

		public string? RegistreringsNummer { get; set; }
		public int? ProduktionsAar { get; set; }
	

		//Null constructor til EF
		public KundeScooter()
		{
		}
	}
}
