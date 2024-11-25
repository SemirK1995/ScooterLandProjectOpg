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
	public class Ordre
	{
		//Primary Key
		[Key]
		public int OrdreId { get; set; }

		//Foreign Key
		public int KundeId { get; set; }
		// Navigation property
		[ForeignKey("KundeId")]
		public Kunde Kunde { get; set; }

		//Attributter
		public DateTime? Dato { get; set; } = DateTime.Now;
		public double? TotalPris { get; set; }
		public OrdreStatus? Status { get; set; } = OrdreStatus.Oprettet;

		// Navigation property til en liste af betalinger
		public List<Betaling>? Betalinger { get; set; } = new List<Betaling>();

		// Navigation property til en liste af OrdreYdelse
		public List<OrdreYdelse>? OrdreYdelse { get; set; } = new List<OrdreYdelse>();

		//Null constructor
		public Ordre()
		{
		}
	}
}
