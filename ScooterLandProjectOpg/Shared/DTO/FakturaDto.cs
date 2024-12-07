using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.DTO
{
    public class FakturaDto
    {
        //Denne DTO er lavet for at vise hvad en faktura skal indeholde. 

        // Kundeoplysninger
        public int KundeId { get; set; }
        public string? KundeNavn { get; set; }
        public string? KundeAdresse { get; set; }
        public string? KundeTelefon { get; set; }
        public string? KundeEmail { get; set; }

        // Betaling
        public int BetalingsId { get; set; }
        public int OrdreId { get; set; }
        public double? Beløb { get; set; }
        public string? BetalingsMetode { get; set; }
        public string? BetalingsDato { get; set; }
        public bool Betalt { get; set; }
        // Ordre
        public DateTime? OrdreDato { get; set; }
        public double? TotalPris { get; set; }

        // Ydelser
        public List<FakturaYdelseDto>? Ydelser { get; set; } = new List<FakturaYdelseDto>();

        // KundeScooter
        public string? KundeScooter { get; set; }

        // Lejeaftale
        public FakturaLejeAftaleDto? Lejeaftale { get; set; }
		// Produkter
		public List<FakturaProduktDto>? Produkter { get; set; } = new List<FakturaProduktDto>();
	}
}
