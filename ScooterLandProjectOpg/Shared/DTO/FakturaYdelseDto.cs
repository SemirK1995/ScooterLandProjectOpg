using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.DTO
{
    public class FakturaYdelseDto
    {
        //Denne DTO er lavet for at kunne vise hvilken ydelse der er lavet på en kunde scooter i en faktura. 
        public int YdelseId { get; set; }
        public string? YdelseNavn { get; set; }
        public double? BeregnetPris { get; set; }
        public string? ScooterMaerke { get; set; }
        public string? ScooterModel { get; set; }
		public string? MekanikerNavn { get; set; } // Mekanikerens navn
		public double? MekanikerTimer { get; set; } // Mekanikerens timer på ydelsen
	}
}
