using Microsoft.AspNetCore.Http; // Giver adgang til HTTP-specifikke klasser, som eksempelvis StatusCodes.
using Microsoft.AspNetCore.Mvc; // Muliggør brug af controller-funktionalitet, ActionResult, osv.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer interfacet ILejeAftaleRepository, som definerer metodekontrakter for LejeAftale.
using ScooterLandProjectOpg.Shared.Models; // Giver adgang til modelklassen LejeAftale og andre relaterede klasser i Shared.Models.

namespace ScooterLandProjectOpg.Server.Controllers // Angiver, at denne controller ligger i namespace ScooterLandProjectOpg.Server.Controllers.
{
    // Markerer klassen som en API-controller, som håndterer HTTP-anmodninger.
    [Route("api/[controller]")]
    [ApiController]

    // Arver fra ControllerBase, hvilket giver grundlæggende web-API-funktioner.
    public class LejeAftaleController : ControllerBase
    {
        // Felt til at gemme en reference til ILejeAftaleRepository for databaseoperationer.
        private readonly ILejeAftaleRepository _lejeaftaleRepository;

        // Constructor, der injicerer ILejeAftaleRepository for at tilgå lejeaftale-data.
        public LejeAftaleController(ILejeAftaleRepository lejeaftaleRepository)
        {
            _lejeaftaleRepository = lejeaftaleRepository;
        }

        // POST: api/lejeaftaler
        [HttpPost] // Endpoint til at oprette en ny LejeAftale i databasen.
        public async Task<ActionResult<LejeAftale>> Add([FromBody] LejeAftale lejeAftale)
        {
            if (lejeAftale == null)
            {
                // Returnerer 400 Bad Request, hvis lejeaftale-objektet er null.
                return BadRequest("Lejeaftale data is null."); 
            }

            // Tilføjer lejeaftalen til databasen via repository.
            var createdLejeaftale = await _lejeaftaleRepository.AddAsync(lejeAftale);

            // Returnerer 201 Created med link til GetById-endpointet for den oprettede lejeaftale.
            return CreatedAtAction(nameof(GetById), new { id = createdLejeaftale.LejeId }, createdLejeaftale);
        }

        // GET: api/lejeaftaler/{id}
        [HttpGet("{id}")] // Endpoint til at hente en LejeAftale inkl. detaljer ud fra dens ID.

        public async Task<ActionResult<LejeAftale>> GetById(int id)
        {
            // Henter LejeAftale fra databasen via repository, med relaterede detaljer (kunde, scootere, osv.).
            var lejeaftale = await _lejeaftaleRepository.GetLejeAftaleWithDetailsAsync(id);

            if (lejeaftale == null)
            {
                // Returnerer 404 Not Found, hvis ingen aftale med dette ID findes.
                return NotFound($"Lejeaftale with ID {id} not found."); 
            }

            // Returnerer 200 OK med den fundne LejeAftale.
            return Ok(lejeaftale); 
        }

        [HttpGet("search")] // Endpoint til at søge efter LejeAftaler baseret på en tekstforespørgsel (query).
        public async Task<ActionResult<IEnumerable<LejeAftale>>> SearchLejeAftaler([FromQuery] string query)
        {
            // Søger lejeaftaler i databasen ved hjælp af repositoriet og query-strengen.
            var lejeAftaler = await _lejeaftaleRepository.SearchLejeAftalerAsync(query);
            // Returnerer 200 OK med de fundne lejeaftaler.
            return Ok(lejeAftaler); 
        }

        [HttpPut("{lejeId}/selvrisiko")] // Endpoint til at opdatere selvrisiko for en LejeAftale ud fra dens ID.
        public async Task<IActionResult> UpdateSelvrisiko(int lejeId, [FromBody] double selvrisiko)
        {
            try
            {
                // Bruger repositoriet til at opdatere selvrisiko.
                await _lejeaftaleRepository.UpdateSelvrisikoAsync(lejeId, selvrisiko);
                // Returnerer 204 No Content, hvis opdateringen lykkedes.
                return NoContent(); 
            }
            catch (KeyNotFoundException ex)
            {
                // Returnerer 404 Not Found, hvis LejeAftale ikke findes.
                return NotFound(ex.Message); 
            }
        }

        [HttpPut("{lejeId}/kilometer")] // Endpoint til at opdatere kørte kilometer for en LejeAftale.
        public async Task<IActionResult> UpdateKortKilometer(int lejeId, [FromBody] int kortKilometer)
        {
            try
            {
                // Opdaterer kilometer i repositoriet og henter den opdaterede LejeAftale.
                var lejeAftale = await _lejeaftaleRepository.UpdateKortKilometerAsync(lejeId, kortKilometer);

                if (lejeAftale == null)
                {
                    // Returnerer 404 Not Found, hvis ID ikke eksisterer i databasen.
                    return NotFound($"Lejeaftale med ID {lejeId} blev ikke fundet."); 
                }

                // Returnerer 200 OK med den tilhørende Ordre for LejeAftalen.
                return Ok(lejeAftale.Ordre); 
            }
            catch (KeyNotFoundException ex)
            {
                // Returnerer 404, hvis nøglen ikke findes.
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                // Returnerer 500 Internal Server Error ved generelle fejl.
                return StatusCode(500, $"Der opstod en fejl: {ex.Message}"); 
            }
        }

        [HttpGet] // GET-endpoint til at hente alle LejeAftaler med kunde- og scooterinformation.
        public async Task<ActionResult<IEnumerable<LejeAftale>>> GetAllLejeAftaler()
        {
            try
            {
                // Henter alle LejeAftaler fra databasen med tilhørende kunde og scootere.
                var lejeAftaler = await _lejeaftaleRepository.GetAllWithKundeAndScootersAsync();

                // Returnerer 200 OK med listen af LejeAftaler.
                return Ok(lejeAftaler); 
            }
            catch (Exception ex)
            {
                // Returnerer 500, hvis der opstår en uventet serverfejl.
                return StatusCode(500, $"Serverfejl: {ex.Message}"); 
            }
        }
    }
}