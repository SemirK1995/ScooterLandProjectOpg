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
        [Required(ErrorMessage = "Startdato er påkrævet.")]
        public DateTime? StartDato { get; set; }
        [Required(ErrorMessage = "Slutdato er påkrævet.")]
        public DateTime? SlutDato { get; set; }

        [Required(ErrorMessage = "Daglig leje er påkrævet.")]
        [Range(0, double.MaxValue, ErrorMessage = "Daglig leje skal være et positivt beløb.")]
        public double DagligLeje { get; set; }

		// Forsikringspris pr. dag (standard 50 kr.)
		[Required]
        public double ForsikringsPris { get; set; } = 50;

        // Kilometerpris (fastsat til 0,53 kr.)
        [Range(0, double.MaxValue, ErrorMessage = "Kilometerpris skal være et positivt beløb.")]
        public double KilometerPris { get; set; } = 0.53;

        // Selvrisiko (standardværdi 1000, hvis forsikring er valgt)
        [Range(0, double.MaxValue, ErrorMessage = "Selvrisiko skal være et positivt beløb.")]
        public double Selvrisiko { get; set; }

        // Korte kilometer (f.eks. inkluderede kilometer uden ekstra omkostninger)
        [Range(0, int.MaxValue, ErrorMessage = "Kort kilometer skal være et positivt tal.")]
        public int? KortKilometer { get; set; }

        
        [NotMapped] // Gemmes ikke i databasen
        public double TotalPris
        {
            get
            {
				var dage = (SlutDato - StartDato)?.Days ?? 0;
				var forsikringsOmkostning = ForsikringsPris * dage;
				var kilometerOmkostning = KortKilometer.HasValue ? KilometerPris * KortKilometer.Value : 0;

				return (DagligLeje * dage) + forsikringsOmkostning + kilometerOmkostning;
			}
        }

        // Navigation property til en liste af LejeScooter
        public List<LejeScooter>? LejeScooter { get; set; }

        //Null constructor til EF
        public LejeAftale()
        {
        }
    }
}
