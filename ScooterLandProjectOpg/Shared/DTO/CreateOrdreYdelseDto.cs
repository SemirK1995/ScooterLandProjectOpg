using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.DTO
{
    public class CreateOrdreYdelseDto
    {
        public int YdelseId { get; set; }
        public double? AftaltPris { get; set; }
        public DateTime? Dato { get; set; } = DateTime.Now;
    }
}
