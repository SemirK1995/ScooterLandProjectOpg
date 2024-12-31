using System; // Importerer funktionalitet til arbejde med datoer og tid.
using System.Collections.Generic; // Gør det muligt at arbejde med generiske kollektioner som List (ikke brugt her).
using System.Linq; // Giver LINQ-forespørgsler til datahåndtering (ikke brugt her).
using System.Text; // Indeholder klasser til arbejde med tekstmanipulation (ikke brugt her).
using System.Threading.Tasks; // Tilbyder funktioner til asynkron programmering (ikke brugt her).

namespace ScooterLandProjectOpg.Shared.DTO // Definerer navnerummet for dataoverførselsobjekter (DTO'er) relateret til ScooterLand-projektet.
{
    // En klasse designet til at repræsentere en ydelse i en ordre, der skal oprettes.
    public class CreateOrdreYdelseDto 
    {
        public int YdelseId { get; set; } // ID for den specifikke ydelse, der er en del af ordren.
        public double? AftaltPris { get; set; } // Valgfri pris, der kan være blevet aftalt for ydelsen.
        public DateTime? Dato { get; set; } = DateTime.Now; // Datoen for ydelsen, som standard er sat til nuværende dato og tid.
        public int? ScooterId { get; set; } // Relation til en specifik scooter (kunde-scooter) tilknyttet ydelsen.
    }
}