using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.DTO
{
    public class CreateOrdreDto
    {
        //Denne DTO er lavet for at kunne oprette en ordre.

        public int KundeId { get; set; }
        public DateTime Dato { get; set; } = DateTime.Now;
        public double TotalPris { get; set; }
        public List<CreateOrdreYdelseDto> OrdreYdelser { get; set; } = new();


        // Valgfri lejeaftale
        public CreateLejeAftaleDto? LejeAftale { get; set; } 
		public List<CreateOrdreProduktDto>? OrdreProdukter { get; set; } = new();
	}
}
