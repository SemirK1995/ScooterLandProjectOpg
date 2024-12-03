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

        // Foreign Key 
        public int? ScooterId { get; set; }
        [ForeignKey("ScooterId")]
        public KundeScooter? Scooter { get; set; }

		public int? MekanikerId { get; set; }
		[ForeignKey("MekanikerId")]
		public Mekaniker? Mekaniker { get; set; }


		// Start og slutdato
		public DateTime? StartDato { get; set; }
		public DateTime? SlutDato { get; set; }

		//Attributter
		public double? Timer { get; set; }
		public DateTime? Dato { get; set; }
		public double? AftaltPris { get; set; }

		[NotMapped] // Ikke gemmes i databasen
		public double BeregnetPris
		{
			get
			{
				return AftaltPris.HasValue && AftaltPris > 0
					? AftaltPris.Value
					: Ydelse?.StandardPris ?? 0; // Brug StandardPris, hvis AftaltPris ikke er angivet
			}
		}


		//Null constructor
		public OrdreYdelse()
		{
		}
	}
}
