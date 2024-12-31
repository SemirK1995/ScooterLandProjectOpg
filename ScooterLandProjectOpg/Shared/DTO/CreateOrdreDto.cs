using System; // Importerer funktioner til håndtering af datoer og tid.
using System.Collections.Generic; // Muliggør brug af generiske kollektioner som List.
using System.Linq; // Giver funktioner til LINQ-forespørgsler, selvom det ikke bruges her.
using System.Text; // Indeholder klasser til tekstmanipulation, men bruges ikke her.
using System.Threading.Tasks; // Tilbyder funktioner til asynkron programmering, men bruges ikke her.

namespace ScooterLandProjectOpg.Shared.DTO // Definerer navnerummet, som grupperer DTO-klasser til dataoverførsel.
{
    // Definerer en klasse til oprettelse af en ordre via dataoverførsel.
    public class CreateOrdreDto 
    {
        public int KundeId { get; set; } // Indeholder ID på den kunde, der placerer ordren.
        
        public int? LejeId { get; set; } // Indeholder ID for en valgfri lejeaftale, hvis relevant for ordren.
        
        public DateTime Dato { get; set; } = DateTime.Now; // Datoen for ordren, som som standard er sat til nuværende tidspunkt.
        
        public double TotalPris { get; set; } // Den samlede pris for ordren, som skal beregnes og gemmes.
        
        public List<CreateOrdreYdelseDto> OrdreYdelser { get; set; } = new(); // Liste over ydelser, der er inkluderet i ordren, initialiseret som en tom liste.
        
        public CreateLejeAftaleDto? LejeAftale { get; set; } // En valgfri lejeaftale tilknyttet ordren, hvis der er en.
        
        public List<CreateOrdreProduktDto>? OrdreProdukter { get; set; } = new(); // En valgfri liste af produkter, der er inkluderet i ordren, initialiseret som en tom liste.
    }
}