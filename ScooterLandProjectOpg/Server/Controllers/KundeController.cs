using Microsoft.AspNetCore.Http; // Giver adgang til HTTP-relaterede klasser, f.eks. IActionResult og HttpContext.
using Microsoft.AspNetCore.Mvc; // Gør det muligt at bruge [ApiController], [Route], ActionResult osv. inden for MVC-rammen.
using Microsoft.Data.SqlClient; // Gør det muligt at håndtere SQL-specifikke klasser, hvis nødvendigt.
using Microsoft.EntityFrameworkCore; // Giver adgang til EF Core-funktionalitet.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer interface, der definerer metoder til kunde-repository.
using ScooterLandProjectOpg.Server.Services; // Importerer lokale services, fx hvis man vil bruge en service til forretningslogik.
using ScooterLandProjectOpg.Shared.Models; // Importerer modelklasser (Kunde, Ordre osv.) fra Shared.Models.

namespace ScooterLandProjectOpg.Server.Controllers // Angiver, at denne controller ligger i ScooterLandProjectOpg.Server.Controllers-namespace.
{
    // Marker klassen som en API-controller, som håndterer HTTP-anmodninger.
    [Route("api/[controller]")]
    [ApiController]

    // Arver fra ControllerBase, som giver basale funktioner for en web-API-controller.
    public class KundeController : ControllerBase
    {
        // Felt til at gemme en reference til IKundeRepository for databaseoperationer.
        private readonly IKundeRepository _kundeRepository;

        // Constructor, der injicerer IKundeRepository, så vi kan tilgå kundedata.
        public KundeController(IKundeRepository kundeRepository)
        {
            _kundeRepository = kundeRepository;
        }

        // GET: api/Kunde
        [HttpGet] // Henter alle kunder fra databasen.
        public async Task<ActionResult<IEnumerable<Kunde>>> GetAll()
        {
            // Anvender repository til at hente en liste af kunder.
            var kunder = await _kundeRepository.GetAllAsync();
            // Returnerer en 200 OK-respons med listen af kunder.
            return Ok(kunder); 
        }

        // GET: api/Kunde/with-orders
        [HttpGet("with-orders")] // Henter alle kunder inklusive deres tilknyttede ordrer.
        public async Task<ActionResult<IEnumerable<Kunde>>> GetAllWithOrders()
        {
            // Henter kunder med ordrer.
            var kunder = await _kundeRepository.GetAllWithOrdersAsync();
            // Returnerer en 200 OK-respons med kunder og deres ordrer.
            return Ok(kunder); 
        }

        // GET: api/Kunde/with-scooters
        [HttpGet("with-scooters/{id}")] // Henter en bestemt kunde inklusiv dennes scootere (kunde-scootere).

        public async Task<ActionResult<Kunde>> GetKundeWithScooters(int id)
        {
            // Henter kunde med tilknyttede scootere.
            var kunde = await _kundeRepository.GetKundeWithScootersAsync(id);

            if (kunde == null)
            {
                // Hvis ingen kunde med det ID findes, returner 404.
                return NotFound($"Kunde with ID {id} not found."); 
            }

            // Returnerer 200 OK med kunde og dennes scootere.
            return Ok(kunde); 
        }

        // GET: api/Kunde
        [HttpGet("{id}")] // Henter en kunde ud fra ID.
        public async Task<ActionResult<Kunde>> GetById(int id)
        {
            // Finder kunde i databasen via ID.
            var kunde = await _kundeRepository.GetByIdAsync(id);

            if (kunde == null)
            {
                // Returnerer 404 Not Found, hvis ingen kunde er fundet.
                return NotFound(); 
            }
            
            // Returnerer 200 OK med den fundne kunde.
            return Ok(kunde); 
        }

        [HttpGet("search")] // Søger kunder på baggrund af navn.
        public async Task<ActionResult<IEnumerable<Kunde>>> SearchByName([FromQuery] string name)
        {
            // Søg i databasen efter kunder, hvis navn matcher søgeordet.
            var kunder = await _kundeRepository.SearchByNameAsync(name);
            // Returnerer 200 OK med en liste af fundne kunder.
            return Ok(kunder); 
        }

        // POST: api/Kunde
        [HttpPost] // Opretter en ny kunde i databasen.
        public async Task<ActionResult<Kunde>> Add([FromBody] Kunde kunde)
        {
            if (kunde == null)
            {
                // Returnerer 400 Bad Request, hvis kunde er null.
                return BadRequest("Kunde data is null."); 
            }

            // Tilføjer kunden til databasen via repository.
            var createdKunde = await _kundeRepository.AddAsync(kunde);
            // Returnerer 201 Created med et link til GetById-endpointet.
            return CreatedAtAction(nameof(GetById), new { id = createdKunde.KundeId }, createdKunde); 
        }

        // PUT: api/Kunde
        [HttpPut("{id}")] // Opdaterer eksisterende kunde i databasen.
        public async Task<IActionResult> Update(int id, [FromBody] Kunde kunde)
        {
            if (kunde == null || kunde.KundeId != id)
            {
                // Returnerer 400 Bad Request, hvis data ikke stemmer overens.
                return BadRequest("Invalid Kunde data."); 
            }

            // Bruger repository til at opdatere kundens data i databasen.
            await _kundeRepository.UpdateAsync(kunde);
            // Returnerer 204 No Content, hvis opdateringen lykkedes.
            return NoContent(); 
        }

        [HttpDelete("{id}")] // Sletter en kunde ud fra ID.

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Kontroller, om kunden findes
                var kunde = await _kundeRepository.GetByIdAsync(id);
                if (kunde == null)
                {
                    // Returnerer 404, hvis kunden ikke findes.
                    return NotFound($"Kunde med ID {id} blev ikke fundet."); 
                }

                // Kalder repository for at slette kunden fra databasen.
                await _kundeRepository.DeleteAsync(id);

                // Returner status 204 No Content, hvis alt lykkes
                return NoContent(); 
            }
            catch (Exception ex)
            {
                // Log fejl
                Console.WriteLine($"Fejl ved sletning af kunde: {ex.Message}");
                // Returnerer 500 (serverfejl), hvis en exception forekommer.
                return StatusCode(500, "Der opstod en fejl ved sletning af kunden."); 
            }
        }

        [HttpGet("{kundeId}/ordrer")] // Endpoint til at hente ordrer for en bestemt kunde.
        public async Task<ActionResult<IEnumerable<Ordre>>> GetKundeOrdrer(int kundeId)
        {
            // Henter ordrer for en kunde med ID = kundeId.
            var ordrer = await _kundeRepository.GetOrdrerForKundeAsync(kundeId);

            if (!ordrer.Any())
            {
                // Returnerer 404 Not Found, hvis ingen ordrer findes.
                return NotFound("Denne kunde har ingen ordrer."); 
            }

            // Returnerer 200 OK med kundens ordrer.
            return Ok(ordrer); 
        }

        [HttpGet("{kundeId}/details")] // Endpoint til at hente detaljer om en kunde, fx ordrer, scootere og andre relaterede data.
        public async Task<ActionResult<Kunde>> GetKundeDetails(int kundeId)
        {
            // Henter en kunde med diverse relaterede data.
            var kunde = await _kundeRepository.GetKundeWithManyDetailsByIdAsync(kundeId);

            if (kunde == null) // Returnerer 404, hvis ingen kunde findes.
                return NotFound($"Ingen kunde fundet med ID {kundeId}.");

            // Returnerer 200 OK med kundens detaljer.
            return Ok(kunde); 
        }

        [HttpGet("searchmany")] // Endpoint til at søge kunder på tværs af forskellige felter.
        public async Task<IActionResult> SøgKunder([FromQuery] string? søgeTekst)
        {
            // Anvender repository til at søge kunder baseret på tekstparametre.
            var result = await _kundeRepository.SearchKunderAsync(søgeTekst);
            // Returnerer 200 OK med listen af fundne kunder.
            return Ok(result); 
        }
    }
}