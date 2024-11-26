using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.DTO
{
    public class LejeAftaleDto
    {
        public int LejeId { get; set; } // ID for lejeaftalen
        public int KundeId { get; set; }
        public string KundeNavn { get; set; }

        public DateTime StartDato { get; set; }
        public DateTime SlutDato { get; set; }
        public double DagligLeje { get; set; }
        public bool Forsikring { get; set; }
        public double KilometerPris { get; set; } = 0.53; // Standard kilometerpris
        public double Selvrisiko { get; set; } = 1000; // Standard selvrisiko
        public int? KortKilometer { get; set; }

        public double TotalPris { get; set; } // Beregnes på backend og sendes til frontend
    }

}
