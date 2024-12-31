using Microsoft.AspNetCore.Http; // Muliggør brug af HTTP-specifik funktionalitet, herunder eksempelvis StatusCodes.
using Microsoft.AspNetCore.Mvc; // Giver adgang til MVC-komponenter, såsom ControllerBase, ActionResult osv.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer interfacet IMekanikerRepository, som definerer dataadgangsmetoder for Mekaniker.
using ScooterLandProjectOpg.Shared.Models; // Gør modelklassen Mekaniker (og andre modeller) tilgængelige fra Shared.Models-namespace.
using ScooterLandProjectOpg.Server.Services; // Importerer ekstra services, hvis de er nødvendige i denne controller.

namespace ScooterLandProjectOpg.Server.Controllers // Angiver, at denne controller ligger i ScooterLandProjectOpg.Server.Controllers-namespace.

{
    // Marker klassen som en API-controller, der håndterer HTTP-anmodninger på 'api/Mekaniker'.
    [Route("api/[controller]")]
    [ApiController]

    // Arver fra ControllerBase for at implementere RESTful-endpoints uden Views.
    public class MekanikerController : ControllerBase
    {
        // Felt til at holde en instans af IMekanikerRepository til dataoperationer for Mekaniker.
        private readonly IMekanikerRepository _mekanikerRepository;

        // Constructor, der injicerer IMekanikerRepository, så vi kan udføre databasehandlinger.
        public MekanikerController(IMekanikerRepository mekanikerRepository)
        {
            _mekanikerRepository = mekanikerRepository;
        }

        // GET: api/Mekaniker
        [HttpGet] // Et GET-endpoint, som henter alle mekanikere fra databasen.
        public async Task<ActionResult<IEnumerable<Mekaniker>>> GetAll()
        {
            // Bruger repository til at hente en liste over alle Mekaniker-objekter asynkront.
            var mekanikere = await _mekanikerRepository.GetAllAsync();
            // Returnerer 200 OK med listen af mekanikere.
            return Ok(mekanikere); 
        }

        // GET: api/Mekaniker/
        [HttpGet("{id}")] // Et GET-endpoint, der henter en enkelt Mekaniker baseret på ID.
        public async Task<ActionResult<Mekaniker>> GetById(int id)
        {
            // Finder Mekaniker i databasen via repository ud fra ID.
            var mekaniker = await _mekanikerRepository.GetByIdAsync(id);
            if (mekaniker == null)
            {
                // Returnerer 404 Not Found, hvis mekaniker ikke eksisterer.
                return NotFound($"Mekaniker with ID {id} not found."); 
            }
            // Returnerer 200 OK med den fundne Mekaniker.
            return Ok(mekaniker); 
        }

        // POST: api/Mekaniker
        [HttpPost] // Opretter en ny Mekaniker-post i databasen.
        public async Task<ActionResult<Mekaniker>> Add([FromBody] Mekaniker mekaniker)
        {
            if (mekaniker == null)
            {
                // Returnerer 400 Bad Request, hvis det indsendte objekt er null.
                return BadRequest("Invalid mekaniker data."); 
            }

            // Tilføjer Mekaniker til databasen via repository-metode.
            var createdMekaniker = await _mekanikerRepository.AddAsync(mekaniker);
            // Returnerer 201 Created med link til GetById-endpointet for den nye mekaniker.
            return CreatedAtAction(nameof(GetById), new { id = createdMekaniker.MekanikerId }, createdMekaniker);
        }

        // PUT: api/Mekaniker
        [HttpPut("{id}")] // Opdaterer en eksisterende Mekaniker ved at ID angives i stien, og data i body.

        public async Task<IActionResult> Update(int id, [FromBody] Mekaniker mekaniker)
        {
            if (mekaniker == null || mekaniker.MekanikerId != id)
            {
                // Returnerer 400, hvis objektet er null eller ID ikke stemmer overens.
                return BadRequest("Invalid mekaniker data."); 
            }

            // Opdaterer mekanikerdata i databasen via repository.
            await _mekanikerRepository.UpdateAsync(mekaniker); 

            // Returnerer 204 No Content for at indikere, at opdateringen lykkedes.
            return NoContent(); 
        }

        // DELETE: api/Mekaniker
        [HttpDelete("{id}")] // Sletter en Mekaniker ud fra ID.

        public async Task<IActionResult> Delete(int id)
        {
            // Finder Mekaniker i databasen for at kontrollere, om den eksisterer.
            var mekaniker = await _mekanikerRepository.GetByIdAsync(id);
            if (mekaniker == null)
            {
                // Returnerer 404, hvis mekaniker-objektet ikke er fundet.
                return NotFound($"Mekaniker with ID {id} not found.");                 
            }

            // Sletter mekanikeren fra databasen.
            await _mekanikerRepository.DeleteAsync(id);

            // Returnerer 204 No Content ved succesfuld sletning.
            return NoContent(); 
        }

        [HttpGet("{mekanikerId}/arbejdsopgaver/aktive")] // Endpoint til at hente en mekanikers aktive arbejdsopgaver.
        public async Task<IActionResult> GetAktiveArbejdsopgaver(int mekanikerId)
        {
            // Henter liste over aktive arbejdsopgaver for en given mekaniker-ID.
            var aktiveArbejdsopgaver = await _mekanikerRepository.GetAktiveArbejdsopgaverForMekanikerAsync(mekanikerId);

            if (!aktiveArbejdsopgaver.Any())
            {
                // Returnerer 404, hvis listen over arbejdsopgaver er tom.
                return NotFound($"Ingen aktive arbejdsopgaver fundet for mekaniker med ID {mekanikerId}."); 
            }
            
            // Returnerer 200 OK med de fundne arbejdsopgaver.
            return Ok(aktiveArbejdsopgaver); 
        }
    }
}