using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.DTO
{
    public class FakturaYdelseDto
    {
        public int YdelseId { get; set; }
        public string? YdelseNavn { get; set; }
        public double? BeregnetPris { get; set; }
        public string? ScooterMaerke { get; set; }
        public string? ScooterModel { get; set; }
    }
}
