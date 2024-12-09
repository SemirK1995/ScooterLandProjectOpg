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

        [Required(ErrorMessage = "Registreringsnummer er påkrævet.")]
        [RegularExpression(@"^[A-Za-z]{2}[0-9]{3}$",
           ErrorMessage = "Registreringsnummer skal bestå af præcis 2 bogstaver efterfulgt af 3 tal.")]
        public string? RegistreringsNummer { get; set; }

        [Required(ErrorMessage = "Produktionsår er påkrævet.")]
        [Range(1000, 9999, ErrorMessage = "Produktionsår skal være et gyldigt 4-cifret tal.")]
        public int? ProduktionsAar { get; set; }

		public KundeScooter()
		{
		}
	}
}
