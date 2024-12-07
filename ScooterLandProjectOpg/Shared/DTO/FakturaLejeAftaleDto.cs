using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.DTO
{
    public class FakturaLejeAftaleDto
    {
        //Denne DTO er lavet for at kunne vise en lejeaftale i en faktura hvis den er valgt.
        public DateTime? StartDato { get; set; }
        public DateTime? SlutDato { get; set; }
        public double? ForsikringsPris { get; set; }
        public int? KortKilometer { get; set; }
        public double? DagligLeje { get; set; }
        public double? Selvrisiko { get; set; }
        public List<string> Scootere { get; set; } = new List<string>();
    }
}
