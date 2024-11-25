using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.Models
{
	public class MekanikerYdelse
	{
		// Primary Key
		[Key]
		public int MekanikerYdelseId { get; set; }

		// Foreign Key
		public int MekanikerId { get; set; }
		// Navigation property
		[ForeignKey("MekanikerId")]
		public Mekaniker Mekaniker { get; set; }

		// Foreign Key
		public int YdelseId { get; set; }
		// Navigation property
		[ForeignKey("YdelseId")]
		public Ydelse Ydelse { get; set; }


		//Attributter
		public DateTime? StartDato { get; set; }
		public DateTime? SlutDato { get; set; }

		//Null constructor
		public MekanikerYdelse()
		{
		}
	}
}
