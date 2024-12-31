using Microsoft.AspNetCore.Http; // Giver adgang til HTTP-relaterede klasser, som fx StatusCodes og HttpContext.
using Microsoft.AspNetCore.Mvc; // Gør det muligt at bruge MVC-elementer såsom ControllerBase, ActionResult osv.
using Microsoft.EntityFrameworkCore; // Tilbyder EF Core-funktionalitet, hvis det skulle være nødvendigt her.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer interfacet IOrdreYdelseRepository, der definerer adgang til OrdreYdelse-data i databasen.
using ScooterLandProjectOpg.Shared.Models; // Indeholder modelklasser, fx OrdreYdelse, Mekaniker, Scooter osv.
using ScooterLandProjectOpg.Shared.DTO; // Indeholder Data Transfer Objects, såsom OrdreYdelseDto og TildelMekanikerDto.
namespace ScooterLandProjectOpg.Server.Controllers // Angiver, at denne controller ligger i ScooterLandProjectOpg.Server.Controllers-namespace.

{
    // Marker denne klasse som en API-controller, der håndterer HTTP-anmodninger på 'api/OrdreYdelse'.
    [Route("api/[controller]")]
    [ApiController]
    
    // Arver fra ControllerBase, der giver basale web-API-funktioner.
    public class OrdreYdelseController : ControllerBase
    {
        // Felt til at gemme en instans af repositoryet, der håndterer OrdreYdelse-relaterede databaseoperationer.
        private readonly IOrdreYdelseRepository _ordreYdelseRepository;

        // Constructor, der modtager et IOrdreYdelseRepository, så vi kan tilgå datalaget.
        public OrdreYdelseController(IOrdreYdelseRepository ordreYdelseRepository)
        {
            _ordreYdelseRepository = ordreYdelseRepository;
        }

        [HttpGet("ikke-tildelt")] // GET-endpoint på 'api/OrdreYdelse/ikke-tildelt' for at hente ordreydelser, som ikke er tildelt en mekaniker.
        public async Task<ActionResult<List<OrdreYdelseDto>>> GetIkkeTildelteOrdreYdelser()
        {
            // Henter en liste af OrdreYdelse fra databasen inklusiv detaljer (relationer).
            var ordreYdelser = await _ordreYdelseRepository.GetAllWithDetailsAsync();

            var ikkeTildelteOrdreYdelser = ordreYdelser
                .Where(oy => oy.MekanikerId == null) // Filtrerer på OrdreYdelse uden mekaniker tilknyttet.
                .Select(oy => new OrdreYdelseDto
                {
                    OrdreYdelseId = oy.OrdreYdelseId, // Sætter ID for OrdreYdelsen.
                    YdelseNavn = oy.Ydelse?.Navn, // Navnet på selve ydelsen.
                    ScooterMaerke = oy.Scooter?.Maerke, // Scooterens mærke, hvis den findes.
                    ScooterModel = oy.Scooter?.Model, // Scooterens model.
                    ProduktionsAar = oy.Scooter?.ProduktionsAar.ToString(), // Giver produktionsåret som streng.
                    StartDato = oy.StartDato, // Angiver startdato for ydelsen.
                    SlutDato = oy.SlutDato, // Angiver slutdato for ydelsen.
                    Timer = oy.Timer // Timer planlagt eller brugt på ydelsen.
                })
                .ToList();

            if (!ikkeTildelteOrdreYdelser.Any())
            {
                // Returnerer 404 Not Found, hvis der ikke er nogen ordreydelser uden mekaniker.
                return NotFound("Ingen ordreydelser fundet.");
            }

            // Returnerer 200 OK med listen over ikke-tildelte ordreydelser.
            return Ok(ikkeTildelteOrdreYdelser);
        }

        [HttpPut("tildel")] // PUT-endpoint på 'api/OrdreYdelse/tildel', der opdaterer mekaniker-tilknytningen til en OrdreYdelse.
        public async Task<IActionResult> TildelMekanikerTilOrdreYdelse([FromBody] TildelMekanikerDto dto)
        {
            // Finder en specifik OrdreYdelse baseret på OrdreYdelseId og henter detaljer, hvis de findes.
            var ordreYdelse = await _ordreYdelseRepository.GetWithDetailsByIdAsync(dto.OrdreYdelseId);

            // Returnerer 404, hvis OrdreYdelsen ikke eksisterer.
            if (ordreYdelse == null)
                return NotFound("OrdreYdelse blev ikke fundet.");

            // Tilføjer eller opdaterer værdier fra DTO på ordreYdelses-objektet.
            ordreYdelse.MekanikerId = dto.MekanikerId;
            ordreYdelse.StartDato = dto.StartDato;
            ordreYdelse.SlutDato = dto.SlutDato;
            if (dto.Timer.HasValue)
                ordreYdelse.Timer = dto.Timer.Value;

            // Opdaterer OrdreYdelsen i databasen via repositoryet.
            await _ordreYdelseRepository.UpdateAsync(ordreYdelse);

            // Returnerer 200 OK med en besked om succesfuld tildeling.
            return Ok("Mekaniker tildelt.");
        }
    }
}