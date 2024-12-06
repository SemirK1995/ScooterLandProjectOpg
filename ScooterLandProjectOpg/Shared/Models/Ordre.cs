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

		// Foreign Key til LejeAftale (hvis der er en tilknyttet lejeaftale)
		[ForeignKey("LejeId")]
		public int? LejeId { get; set; }
		public LejeAftale? LejeAftale { get; set; }

		//Attributter
		public DateTime? Dato { get; set; } = DateTime.Now;
		public double? TotalPris { get; set; }
		[NotMapped]
		public double TotalOrdrePris
		{
			get
			{
				var ydelsesPris = OrdreYdelse?.Sum(oy => oy.BeregnetPris) ?? 0;
				var lejeAftalePris = LejeAftale?.TotalPris ?? 0;
				var produktPris = OrdreProdukter?.Sum(op => op.Pris * op.Antal) ?? 0;
				return ydelsesPris + lejeAftalePris+produktPris;
			}
		}

		public OrdreStatus? Status { get; set; } = OrdreStatus.Oprettet;

		// Navigation property til en liste af betalinger
		public List<Betaling>? Betalinger { get; set; } = new List<Betaling>();

		// Navigation property til en liste af OrdreYdelse
		public List<OrdreYdelse>? OrdreYdelse { get; set; } = new List<OrdreYdelse>();

		public List <OrdreProdukt>? OrdreProdukter { get; set; } = new List<OrdreProdukt> { };

		//Null constructor
		public Ordre()
		{
		}
	}
}
