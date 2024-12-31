using Microsoft.AspNetCore.Http; // Giver adgang til HTTP-specifikke klasser, såsom StatusCodes og HttpContext.
using Microsoft.AspNetCore.Mvc; // Gør det muligt at bruge ControllerBase, ActionResult og andre MVC-funktioner i denne controller.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer interfacet IProduktRepository, der håndterer databaseoperationer for Produkt-modellen.
using ScooterLandProjectOpg.Server.Services; // Importerer eventuelle services, hvis de er nødvendige (f.eks. forretningslogik eller andre funktioner).
using ScooterLandProjectOpg.Shared.Models; // Indeholder modelklassen Produkt og andre relaterede modelklasser.

namespace ScooterLandProjectOpg.Server.Controllers // Angiver, at denne controller hører til i ScooterLandProjectOpg.Server.Controllers-namespace.
{
    // Marker klassen som en web-API-controller, der håndterer HTTP-anmodninger på 'api/Produkt'.
    [Route("api/[controller]")]
    [ApiController]
    
    // Arver fra ControllerBase, hvilket giver grundlæggende funktioner til en web-API-controller.
    public class ProduktController : ControllerBase
    {
        // Felt, der gemmer en instans af IProduktRepository, som udfører databaserelaterede operationer.
        private readonly IProduktRepository _produktRepository;

        // Constructor, der injicerer produktRepository for at få adgang til databasehandlinger for Produkt-modellen.
        public ProduktController(IProduktRepository produktRepository)
        {
            _produktRepository = produktRepository;
        }

        [HttpGet] // GET-endpoint til at hente alle produkter fra databasen.
        public async Task<ActionResult<IEnumerable<Produkt>>> GetAll()
        {
            // Henter alle produkter via repository og returnerer 200 OK med resultatet.
            return Ok(await _produktRepository.GetAllAsync());
        }

        [HttpGet("{id}")] // GET-endpoint med et id-parameter til at hente ét specifikt produkt.
        public async Task<ActionResult<Produkt>> GetById(int id)
        {
            // Finder produktet i databasen via ID.
            var produkt = await _produktRepository.GetByIdAsync(id);
            if (produkt == null)
            {
                // Returnerer 404 Not Found, hvis produktet ikke eksisterer.
                return NotFound();
            }
            // Returnerer 200 OK med det fundne produkt, hvis det eksisterer.
            return Ok(produkt);
        }

        [HttpPost] // POST-endpoint til at oprette et nyt produkt i databasen.
        public async Task<ActionResult<Produkt>> Add([FromBody] Produkt produkt)
        {
            // Lader repository oprette et nyt produkt og gemme det i databasen.
            var createdProdukt = await _produktRepository.AddAsync(produkt);
            // Returnerer 201 Created med link til GET-by-id og det oprettede produkt.
            return CreatedAtAction(nameof(GetById), new { id = createdProdukt.ProduktId }, createdProdukt);
        }

        [HttpPut("{id}")] // PUT-endpoint til at opdatere et eksisterende produkt ved angivet id.
        public async Task<IActionResult> Update(int id, [FromBody] Produkt produkt)
        {
            // Tjekker, om ID i stien matcher ID på produktobjektet.
            if (id != produkt.ProduktId)
            {
                // Returnerer 400, hvis der er uoverensstemmelse mellem de to ID'er.
                return BadRequest("ID mismatch.");
            }

            try
            {
                // Opdaterer produktet i databasen via repositoryet.
                await _produktRepository.UpdateAsync(produkt);
            }
            catch (KeyNotFoundException ex)
            {
                // Returnerer 404, hvis produktet ikke kunne findes under opdatering.
                return NotFound(ex.Message);
            }

            // Returnerer 204 No Content ved en succesfuld opdatering.
            return NoContent();
        }

        [HttpDelete("{id}")] // DELETE-endpoint til at slette et produkt fra databasen.
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Forsøger at slette produktet via repositoryet.
                await _produktRepository.DeleteAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                // Returnerer 404 Not Found, hvis produktet ikke eksisterer.
                return NotFound(ex.Message);
            }

            // Returnerer 204 No Content, hvis sletningen lykkedes.
            return NoContent();
        }
    }
}