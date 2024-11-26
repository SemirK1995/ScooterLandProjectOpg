using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.Models
{
	public class LejeScooter
	{
		// Primary Key
		[Key]
		public int LejeScooterId { get; set; }

		// Foreign Key
		public int LejeId { get; set; }

		// Navigation property
		[ForeignKey("LejeId")]
		public LejeAftale LejeAftale { get; set; }

		public string ScooterModel { get; set; }
		public string ScooterMaerke { get; set; }

        public string? RegistreringsNummer { get; set; }

        public bool ErTilgængelig { get; set; } = true; // Standard: Ledig

        //Attrbutter
        public DateTime? StartDato { get; set; }
		public DateTime? SlutDato { get; set; }


		//Null constructor
		public LejeScooter()
		{
		}
	}
}
