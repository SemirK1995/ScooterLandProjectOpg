using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.Models
{
	public class LejeAftale
	{
		// Primary Key
		[Key]
		public int LejeId { get; set; }

		// Foreign Key
		public int KundeId { get; set; }

		// Navigation property
		[ForeignKey("KundeId")]
		public Kunde Kunde { get; set; }

		//Attributter
		public DateTime? StartDato { get; set; }
		public DateTime? SlutDato { get; set; }
		public double? DagligLeje { get; set; }
		public bool? Forsikring { get; set; }
		public double? KilometerPris { get; set; }
		public double? Selvrisiko { get; set; }
		public int? KortKilometer { get; set; }

		// Navigation property til en liste af LejeScooter
		public List<LejeScooter>? LejeScooter { get; set; } = new List<LejeScooter>();

		//Null constructor til EF
		public LejeAftale()
		{
		}
	}
}
