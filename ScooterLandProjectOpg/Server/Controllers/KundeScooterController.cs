using Microsoft.AspNetCore.Http; // Giver adgang til HTTP-relaterede klasser, som f.eks. StatusCodes og HttpContext.
using Microsoft.AspNetCore.Mvc; // Muliggør brugen af MVC-funktionalitet såsom ControllerBase, [Route], [HttpGet] osv.
using Microsoft.EntityFrameworkCore; // Gør EF Core-funktioner tilgængelige, men bruges eventuelt indirekte via repository.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer interface IKundeScooterRepository, der definerer metodekontrakter.
using ScooterLandProjectOpg.Shared.Models; // Giver adgang til KundeScooter-klassen og andre model-klasser i Shared.Models.

namespace ScooterLandProjectOpg.Server.Controllers // Angiver, at denne controller ligger i namespace 'ScooterLandProjectOpg.Server.Controllers'.
{
    // Gør klassen til en API-controller, der kan håndtere HTTP-requests på ruten 'api/KundeScooter'.
    [Route("api/[controller]")]
    [ApiController]
    
    // Arver fra ControllerBase for at skabe en RESTful controller.
    public class KundeScooterController : ControllerBase
    {
        // Felt til at holde en instans af repository-interface, som håndterer KundeScooter-databasen.
        private readonly IKundeScooterRepository _kundeScooterRepository;

        // Constructor injicerer IKundeScooterRepository, så controlleren kan bruge repositoriet.
        public KundeScooterController(IKundeScooterRepository kundeScooterRepository)
        {
            _kundeScooterRepository = kundeScooterRepository;
        }

        [HttpPost("{kundeId}/add-scooter")] // HTTP POST-endpoint. Ruten bliver 'api/KundeScooter/{kundeId}/add-scooter'.
        public async Task<IActionResult> AddScooterToKunde(int kundeId, [FromBody] KundeScooter scooter)
        {
            if (scooter == null)
            {
                // Returnerer en 400 Bad Request, hvis scooter-objektet er null.
                return BadRequest("Scooter-data er ugyldig."); 
            }

            scooter.KundeId = kundeId; // Sætter fremmednøgle til den kunde, scooteren skal tilknyttes.
            var oprettetScooter = await _kundeScooterRepository.AddScooterAsync(scooter); // Kalder repository-metoden for at oprette en ny KundeScooter i databasen.

            if (oprettetScooter == null)
            {
                // Returnerer 500 Serverfejl, hvis repository ikke kunne oprette scooteren.
                return StatusCode(StatusCodes.Status500InternalServerError, "Fejl ved oprettelse af scooter."); 
            }

            // Returnerer 200 OK med den oprettede scooter, hvis alt lykkes.
            return Ok(oprettetScooter); 
        }

        [HttpGet("{kundeId}/scootere")] // HTTP GET-endpoint. Ruten bliver 'api/KundeScooter/{kundeId}/scootere'.
        public async Task<ActionResult<List<KundeScooter>>> GetScootersForKunde(int kundeId)
        {
            // Henter en liste af KundeScooter-objekter, der tilhører den ønskede kunde-ID.
            var scootere = await _kundeScooterRepository.GetScootersByKundeIdAsync(kundeId);

            if (!scootere.Any())
            {
                // Returnerer 404 Not Found, hvis listen er tom.
                return NotFound("Ingen scootere fundet for denne kunde."); 
            }

            // Returnerer 200 OK med listen af scootere, hvis nogen fundet.
            return Ok(scootere); 
        }

        [HttpGet("all")] // HTTP GET-endpoint på ruten 'api/KundeScooter/all' til at hente alle KundeScootere inkl. kundeinfo.
        public async Task<ActionResult<IEnumerable<KundeScooter>>> GetAllScootersWithKunder()
        {
            // Kalder repository-metoden for at hente alle KundeScooter med tilknyttet kunde.
            var allScooters = await _kundeScooterRepository.GetScootersWithKundeAsync();

            if (allScooters == null || !allScooters.Any())
            {
                // Returnerer 404 Not Found, hvis ingen scootere blev fundet.
                return NotFound("Ingen scootere fundet."); 
            }

            // Returnerer 200 OK med samlingen af scootere og deres kunder.
            return Ok(allScooters); 
        }
    }
}