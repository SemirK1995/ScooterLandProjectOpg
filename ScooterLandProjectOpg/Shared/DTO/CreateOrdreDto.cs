using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.DTO
{
    public class CreateOrdreDto
    {
        public int KundeId { get; set; }
        public DateTime Dato { get; set; } = DateTime.Now;
        public double TotalPris { get; set; }
        public List<CreateOrdreYdelseDto> OrdreYdelser { get; set; }

        // Valgfri lejeaftale
        public CreateLejeAftaleDto? LejeAftale { get; set; } // Valgfri
    }
}
