using Microsoft.AspNetCore.Http; // Giver adgang til HTTP-relaterede funktioner (f.eks. StatusCodes).
using Microsoft.AspNetCore.Mvc; // Gør det muligt at bruge MVC-elementer, som ControllerBase, ActionResult, osv.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer interfacet ILejeScooterRepository, der definerer adgang til LejeScooter-data.
using ScooterLandProjectOpg.Shared.Models; // Giver adgang til modelklassen LejeScooter og andre relaterede klasser i Shared.Models.
using Microsoft.EntityFrameworkCore; // Gør EF Core-funktionalitet tilgængelig, hvis det skal anvendes direkte i controlleren.

namespace ScooterLandProjectOpg.Server.Controllers // Angiver, at denne controller ligger i ScooterLandProjectOpg.Server.Controllers-namespace.
{
    // Markerer klassen som en API-controller, der håndterer HTTP-anmodninger på ruten 'api/LejeScooter'.
    [Route("api/[controller]")]
    [ApiController]

    // ControllerBase giver de grundlæggende funktioner for en web-API-controller.
    public class LejeScooterController : ControllerBase
    {
        // Felt til at gemme en instans af ILejeScooterRepository, som håndterer databaseoperationer for LejeScooter.
        private readonly ILejeScooterRepository _lejeScooterRepository;

        // Constructor, der injicerer ILejeScooterRepository for at få adgang til LejeScooter-data.
        public LejeScooterController(ILejeScooterRepository lejeScooterRepository)
        {
            _lejeScooterRepository = lejeScooterRepository;
        }

        [HttpGet("available")] // GET-endpoint på 'api/LejeScooter/available' for at hente alle ledige scootere.
        public async Task<ActionResult<IEnumerable<LejeScooter>>> GetAvailableScooters()
        {
            // Anvender repositoryets metode til at hente en liste over alle ledige (ikke tildelte) scootere.
            var availableScooters = await _lejeScooterRepository.GetScootersAvailableAsync();

            if (availableScooters == null || !availableScooters.Any())
            {
                // Returnerer en 404 Not Found, hvis ingen scootere er fundet.
                return NotFound("Ingen ledige scootere fundet."); 
            }
            // Returnerer 200 OK med listen af ledige scootere.
            return Ok(availableScooters); 
        }

        [HttpPut("{id}/assign")] // PUT-endpoint på 'api/LejeScooter/{id}/assign' til at tilknytte en scooter til en eksisterende lejeaftale.
        public async Task<IActionResult> AssignScooterToLeje(int id, [FromBody] int lejeId)
        {
            try
            {
                // Bruger repositoryet til at opdatere scooterens LejeId og tildeler den dermed til en bestemt lejeaftale.
                await _lejeScooterRepository.UpdateScooterLejeIdAsync(id, lejeId);

                // Returnerer 204 No Content, hvis opdateringen lykkedes.
                return NoContent(); 
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); // Returnerer 404 Not Found, hvis scooteren eller lejeaftalen ikke eksisterer, eller hvis data er ugyldig.
            }
        }
    }
}