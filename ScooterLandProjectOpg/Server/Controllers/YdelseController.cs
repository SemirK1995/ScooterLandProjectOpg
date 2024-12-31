using Microsoft.AspNetCore.Http; // Tilføjer adgang til HTTP-specifik funktionalitet, fx StatusCodes og HttpContext.
using Microsoft.AspNetCore.Mvc; // Gør det muligt at bruge klasser og attributter fra ASP.NET Core MVC, fx ControllerBase, ActionResult, osv.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer interfacet IRepository, der definerer generelle databaseoperationer for Ydelse.
using ScooterLandProjectOpg.Shared.Models; // Indeholder modelklassen Ydelse og andre relaterede klasser i Shared.Models.

namespace ScooterLandProjectOpg.Server.Controllers // Angiver, at denne controller ligger i ScooterLandProjectOpg.Server.Controllers-namespace.
{
    // Markerer klassen som en API-controller med standardrute "api/Ydelse".
    [Route("api/[controller]")]
    [ApiController]

    // Arver fra ControllerBase for at implementere en RESTful web-API.
    public class YdelseController : ControllerBase
    {
        // Felt, der gemmer en instans af IRepository<Ydelse>, som udfører CRUD-handlinger i databasen.
        private readonly IRepository<Ydelse> _ydelseRepository;

        // Constructor, der modtager et ydelseRepository-objekt til databaseoperationer for Ydelse.
        public YdelseController(IRepository<Ydelse> ydelseRepository)
        {
            _ydelseRepository = ydelseRepository;
        }

        // GET: api/Ydelse
        [HttpGet] // GET-endpoint til at hente alle ydelser fra databasen.
        public async Task<ActionResult<IEnumerable<Ydelse>>> GetAll()
        {
            // Henter alle Ydelse-objekter asynkront via repository.
            var ydelser = await _ydelseRepository.GetAllAsync();
            // Returnerer 200 OK med listen af ydelser.
            return Ok(ydelser);
        }

        // GET: api/ydelser/{id}
        [HttpGet("{id}")] // GET-endpoint til at hente en ydelse ud fra ét specifikt ID.
        public async Task<ActionResult<Ydelse>> GetById(int id)
        {
            // Finder en specifik Ydelse baseret på ID.
            var ydelse = await _ydelseRepository.GetByIdAsync(id);
            if (ydelse == null)
            {
                // Returnerer 404 Not Found, hvis ydelsen ikke findes i databasen.
                return NotFound($"Ydelse with ID {id} not found.");
            }
            // Returnerer 200 OK med ydelsen, hvis den blev fundet.
            return Ok(ydelse);
        }

        // POST: api/ydelser
        [HttpPost] // POST-endpoint til at oprette en ny Ydelse i databasen.
        public async Task<ActionResult<Ydelse>> Add([FromBody] Ydelse ydelse)
        {
            if (ydelse == null)
            {
                // Returnerer 400 Bad Request, hvis ydelse-objektet er null.
                return BadRequest("Ydelse data is null.");
            }

            // Opretter en ny ydelse via repositoryet.
            var createdYdelse = await _ydelseRepository.AddAsync(ydelse);
            // Returnerer 201 Created med et link til GET-metoden for den nye ydelse.
            return CreatedAtAction(nameof(GetById), new { id = createdYdelse.YdelseId }, createdYdelse);
        }

        // PUT: api/ydelser/{id}
        [HttpPut("{id}")] // Endpoint til at opdatere en eksisterende Ydelse ud fra ét specifikt ID.
        public async Task<IActionResult> Update(int id, [FromBody] Ydelse ydelse)
        {
            if (ydelse == null || ydelse.YdelseId != id)
            {
                // Returnerer 400, hvis ydelse er null, eller hvis ID'et ikke stemmer overens.
                return BadRequest("Ydelse ID mismatch.");
            }

            var existingYdelse = await _ydelseRepository.GetByIdAsync(id);
            // Finder den eksisterende ydelse i databasen for at tjekke, om den findes.
            if (existingYdelse == null)
            {
                // Returnerer 404, hvis den ikke blev fundet.
                return NotFound($"Ydelse with ID {id} not found.");
            }

            // Opdaterer ydelsen via repository.
            await _ydelseRepository.UpdateAsync(ydelse);
            // Returnerer 204 No Content for at vise, at opdateringen lykkedes.
            return NoContent();
        }

        // DELETE: api/ydelser/{id}
        [HttpDelete("{id}")] // Endpoint til at slette en ydelse ud fra et ID.
        public async Task<IActionResult> Delete(int id)
        {
            var existingYdelse = await _ydelseRepository.GetByIdAsync(id);
            // Finder ydelsen, der skal slettes.
            if (existingYdelse == null)
            {
                // Returnerer 404, hvis ydelsen ikke eksisterer i databasen.
                return NotFound($"Ydelse with ID {id} not found.");
            }

            // Sletter ydelsen via repository.
            await _ydelseRepository.DeleteAsync(id);
            // Returnerer 204 No Content, når sletningen er gennemført.
            return NoContent();
        }
    }
}