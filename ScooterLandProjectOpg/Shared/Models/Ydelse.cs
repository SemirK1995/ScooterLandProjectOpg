using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.Models
{
	public class Ydelse
	{
		// Primary Key
		[Key]
		public int YdelseId { get; set; }

		//Attributter
		public string? Navn { get; set; }
		public double? StandardPris { get; set; }

		// Navigation property til en liste af OrdreYdelse
		public List<OrdreYdelse>? OrdreYdelse { get; set; } = new List<OrdreYdelse>();

		public Ydelse()
		{
		}
	}
}
