using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.DTO
{
	public class TildelMekanikerDto
	{
		//En specifik DTO, lavet for at kunne tildele en mekaniker en arbejdsopgave

		public int OrdreYdelseId { get; set; }
		public int MekanikerId { get; set; }
		public DateTime? StartDato { get; set; }
		public DateTime? SlutDato { get; set; }
		public double? Timer { get; set; }
	}
}
