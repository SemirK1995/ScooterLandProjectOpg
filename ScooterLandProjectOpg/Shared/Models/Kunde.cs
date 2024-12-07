using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.Models
{
	public class Kunde
	{
		// Primary Key
		[Key]
		public int KundeId { get; set; }

		// Attributter

		[Required(ErrorMessage = "Navn er påkrævet.")]
		public string? Navn { get; set; }

		[Required(ErrorMessage = "Adresse er påkrævet.")]
		public string? Adresse { get; set; }

		[Required(ErrorMessage = "Telefonnummer er påkrævet.")]
		[Range(10000000, 99999999, ErrorMessage = "Telefonnummer skal være et gyldigt 8-cifret tal.")]
		public int? Telefonnummer { get; set; }

		[Required(ErrorMessage = "Email er påkrævet.")]
		[EmailAddress(ErrorMessage = "Indtast en gyldig email-adresse.")]
		public string? Email { get; set; }


		// Navigation property til en liste af KundeScooter
		public List<KundeScooter>? KundeScooter { get; set; } = new List<KundeScooter>();
		// Navigation property til en liste af LejeaAftale
		public List<LejeAftale>? LejeAftale { get; set; } = new List<LejeAftale>();
		// Navigation property til en liste af Ordre
		public List<Ordre>? Ordre { get; set; } = new List<Ordre>();

		public Kunde()
		{
		}
	}
}

