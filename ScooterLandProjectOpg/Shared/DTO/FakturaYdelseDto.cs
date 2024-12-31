using System; // Inkluderer grundlæggende funktionalitet som dato og tid.
using System.Collections.Generic; // Tillader brug af generiske samlinger som List.
using System.Linq; // Giver LINQ-metoder til datasæt.
using System.Text; // Indeholder funktioner til arbejde med tekst.
using System.Threading.Tasks; // Understøtter asynkron programmering.

namespace ScooterLandProjectOpg.Shared.DTO // Definerer navnerum til Data Transfer Objects for delte komponenter.
{
    // En klasse, der definerer en DTO til repræsentation af en ydelse i en faktura.
    public class FakturaYdelseDto 
    {
        public int YdelseId { get; set; } // Id for ydelsen, bruges til unik identifikation.
        
        public string? YdelseNavn { get; set; } // Navnet på ydelsen, f.eks. reparation eller service.
       
        public double? BeregnetPris { get; set; } // Den beregnede pris for ydelsen, kan være null hvis ikke fastsat.
        
        public string? ScooterMaerke { get; set; } // Mærke på scooteren, der er forbundet med ydelsen, kan være null.
        
        public string? ScooterModel { get; set; } // Model på scooteren, der er forbundet med ydelsen, kan være null.
       
        public string? MekanikerNavn { get; set; } // Navnet på mekanikeren, der har udført ydelsen, kan være null.
       
        public double? MekanikerTimer { get; set; } // Antal timer brugt af mekanikeren, kan være null.
    }
}