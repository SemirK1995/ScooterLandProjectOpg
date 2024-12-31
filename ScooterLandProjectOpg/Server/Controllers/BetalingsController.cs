using Microsoft.AspNetCore.Http; // Gør Http-relaterede klasser tilgængelige, f.eks. IActionResult.
using Microsoft.AspNetCore.Mvc; // Gør controller- og MVC-relaterede klasser tilgængelige.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer det lokale interface IBetalingRepository, der håndterer databaseoperationer for betaling.
using ScooterLandProjectOpg.Server.PDFServices; // Importerer FakturaService, der genererer PDF-fakturaer.
using ScooterLandProjectOpg.Shared.DTO; // Importerer Data Transfer Objects (DTO-klasser) fra Shared.DTO-namespace.
using ScooterLandProjectOpg.Shared.Enum; // Importerer enum-typer, som f.eks. BetalingsMetodeStatus.
using ScooterLandProjectOpg.Shared.Models; // Importerer modelklasser fra Shared.Models-namespace.

namespace ScooterLandProjectOpg.Server.Controllers // Definerer, at denne controller ligger i ScooterLandProjectOpg.Server.Controllers-namespace.
{
    // Angiver, at denne klasse er en API-controller, der håndterer HTTP-requests.
    [Route("api/[controller]")]
    [ApiController]
    
    // ControllerBase er en grundklasse for web-API-controllere (uden Views).
    public class BetalingsController : ControllerBase
    {
        private readonly IBetalingRepository _betalingRepository; // Repository-interface til at udføre databaseoperationer for Betaling.
        private readonly FakturaService _fakturaService; // Service-klasse, der håndterer generering af fakturaer i PDF-format.

        // Constructor, der injicerer IBetalingRepository og FakturaService.
        public BetalingsController(IBetalingRepository betalingRepository, FakturaService fakturaService)
        {
            // Gemmer den injicerede IBetalingRepository i et felt, så vi kan håndtere betalinger i databasen.
            _betalingRepository = betalingRepository; 
            // Gemmer FakturaService i et felt, så vi kan generere fakturaer til betalinger.
            _fakturaService = fakturaService; 
        }

        [HttpGet] // En GET-endpoint, der henter alle betalinger.
        public async Task<ActionResult<IEnumerable<Betaling>>> GetAll()
        {
            // Anvender repository til at hente alle betalinger asynkront.
            var betalinger = await _betalingRepository.GetAllAsync();
            return Ok(betalinger); // Returnerer en 200 OK-respons med listen af betalinger.
        }

        [HttpGet("search")] // En GET-endpoint til at søge efter betalinger baseret på en query.
        public async Task<ActionResult<IEnumerable<Betaling>>> Search([FromQuery] string query)
        {
            // Hvis query-strengen er tom, returneres en 400 Bad Request med fejlmeddelelse.
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Søgeforespørgslen er tom.");

            // Søger betalinger i databasen ved hjælp af repositoriet.
            var betalinger = await _betalingRepository.SearchByQueryAsync(query);

            // Hvis ingen resultater, returneres en 404 Not Found med besked.
            if (!betalinger.Any())
                return NotFound("Ingen betalinger fundet.");

            return Ok(betalinger); // Returnerer 200 OK med de fundne betalinger.
        }

        [HttpPut("{betalingsId}/status")] // En PUT-endpoint til at opdatere betalingsstatus for en specifik betaling.
        public async Task<IActionResult> UpdateStatus(int betalingsId, [FromBody] BetalingUpdateDto betalingUpdate)
        {
            try
            {
                // Bruger repositoriet til at opdatere status og dato for betaling.
                await _betalingRepository.UpdateBetalingsStatusAsync(betalingsId, betalingUpdate);
                return Ok("Betalingsstatus og dato opdateret."); // Returnerer en 200 OK med succesbesked.
            }
            catch (KeyNotFoundException ex)
            {
                // Hvis ID'et ikke findes, returneres 404 Not Found med exception-besked.
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                // Returnerer 400 Bad Request ved anden fejl, sammen med fejlmeddelelse.
                return BadRequest($"Fejl ved opdatering af betaling: {ex.Message}"); 
            }
        }

        [HttpPut("{betalingsId}/metode")] // En PUT-endpoint til at opdatere betalingsmetoden for en specifik betaling.
        public async Task<IActionResult> UpdateBetalingsMetode(int betalingsId, [FromBody] BetalingsMetodeStatus nyMetode)
        {
            try
            {
                // Anvender repositoriet til at opdatere betalingsmetoden.
                await _betalingRepository.UpdateBetalingsMetodeAsync(betalingsId, nyMetode);
                // Returnerer en succes-besked ved OK-kald.
                return Ok("Betalingsmetode opdateret."); 
            }
            catch (KeyNotFoundException ex)
            {
                // Hvis betalingen ikke findes, returneres 404 Not Found.
                return NotFound(ex.Message); 
            }
        }

        [HttpGet("{betalingsId}/faktura")] // En GET-endpoint til at generere fakturadata for en betaling.
        public async Task<ActionResult<FakturaDto>> GenererFaktura(int betalingsId)
        {
            try
            {
                // Henter detaljeret Betaling-objekt med tilhørende data (Ordre, Kunde, osv.).
                var betaling = await _betalingRepository.GetFakturaDetaljerAsync(betalingsId);

                // Hvis betalingen ikke findes, returneres 404.
                if (betaling == null)
                    return NotFound($"Betaling med ID {betalingsId} blev ikke fundet.");

                // Lokale variabler til at lette adgangen til Ordre og Kunde.
                var ordre = betaling.Ordre;
                var kunde = ordre.Kunde;

                // Beregn den opdaterede TotalPris inklusive selvrisiko fra en eventuel lejeaftale.
                double totalPris = ordre.TotalPris ?? 0;
                if (ordre.LejeAftale != null)
                {
                    totalPris += ordre.LejeAftale.Selvrisiko; // Lægger selvrisiko til totalprisen, hvis en lejeaftale eksisterer.
                }

                // Opretter et FakturaDto-objekt, der indeholder alle nødvendige data til fakturaen.
                var fakturaDto = new FakturaDto
                {
                    // Kundeoplysninger
                    KundeId = kunde.KundeId, // Kunde-id fra kundeobjektet.
                    KundeNavn = kunde.Navn, // Kundens navn.
                    KundeAdresse = kunde.Adresse, // Kundens adresse.
                    KundeTelefon = kunde.Telefonnummer?.ToString(), // Kundens telefonnummer konverteret til streng.
                    KundeEmail = kunde.Email, // Kundens e-mailadresse.

                    // Betaling
                    BetalingsId = betaling.BetalingsId, // Angiver betalings-id, så faktura kan spores.
                    OrdreId = betaling.OrdreId, // Tilknyttet ordre-id fra betaling.
                    Beløb = totalPris, // Brug den opdaterede TotalPris
                    BetalingsMetode = betaling.BetalingsMetode?.ToString(), // Konverterer enum-værdien til tekst.
                    BetalingsDato = betaling.BetalingsDato?.ToString("dd-MM-yyyy"), // Formaterer betalingsdatoen.
                    Betalt = betaling.Betalt, // Angiver om betalingen er foretaget eller ej.

                    // Ordre
                    OrdreDato = ordre.Dato, // Ordredato fra ordreobjektet.
                    TotalPris = totalPris, // Indeholdt selvrisiko, hvis relevant.

                    // Ydelser: lister alle ydelser, der er tilknyttet ordren.
                    Ydelser = ordre.OrdreYdelse?.Select(oy => new FakturaYdelseDto
                    {
                        YdelseId = oy.YdelseId, // Id for ydelsen.
                        YdelseNavn = oy.Ydelse?.Navn, // Navn på ydelsen.
                        MekanikerNavn = oy.Mekaniker?.Navn, // Navn på den mekaniker, der udfører ydelsen.
                        MekanikerTimer = oy.Timer, // Antal timer brugt af mekanikeren.
                        BeregnetPris = oy.BeregnetPris, // Den beregnede pris (evt. inkl. timer).
                        ScooterMaerke = oy.Scooter?.Maerke, // Mærket på den scooter, ydelsen er udført på.
                        ScooterModel = oy.Scooter?.Model // Modellen på scooteren.
                    }).ToList(),

                    // KundeScooter: viser blot den første scooter, hvis en findes i OrdreYdelse-listen, ellers "Ingen scooter valgt".
                    KundeScooter = ordre.OrdreYdelse?.FirstOrDefault()?.Scooter?.Model ?? "Ingen scooter valgt",

                    // Lejeaftale (hvis relevant)
                    Lejeaftale = ordre.LejeAftale == null
                        ? null
                        : new FakturaLejeAftaleDto
                        {
                            StartDato = ordre.LejeAftale.StartDato, // Startdato for lejeperioden.
                            SlutDato = ordre.LejeAftale.SlutDato, // Slutdato for lejeperioden.
                            ForsikringsPris = ordre.LejeAftale.ForsikringsPris, // Daglig forsikringspris.
                            KortKilometer = ordre.LejeAftale.KortKilometer ?? 0, // Antal kørte kilometer (kan være null, derfor ?? 0).
                            DagligLeje = ordre.LejeAftale.DagligLeje, // Pris pr. dag for leje.
                            Selvrisiko = ordre.LejeAftale?.Selvrisiko ?? 0, // Angiver selvrisikoen for lejeaftalen.
                            Scootere = ordre.LejeAftale.LejeScooter?.Select(ls => $"{ls.ScooterMaerke} {ls.ScooterModel}").ToList() ?? new List<string>() // Viser alle scootere tilknyttet lejeaftalen.
                        },

                    // Produkter: lister alle produkter, der er købt på denne ordre.
                    Produkter = ordre.OrdreProdukter?.Select(op => new FakturaProduktDto
                    {
                        ProduktId = op.Produkt.ProduktId, // Produktets ID.
                        ProduktNavn = op.Produkt.ProduktNavn, // Navn på produktet.
                        Antal = op.Antal, // Antal købte enheder.
                        Pris = op.Pris // Pris pr. enhed.
                    }).ToList()
                };

                // Returnerer 200 OK med det genererede FakturaDto-objekt.
                return Ok(fakturaDto); 
            }
            catch (Exception ex)
            {
                // Returnerer 400 Bad Request, hvis der opstår en fejl under processen.
                return BadRequest($"Fejl ved generering af faktura: {ex.Message}"); 
            }
        }

        [HttpGet("{betalingsId}/download")] // En GET-endpoint til at downloade fakturaen som en PDF.
        public async Task<IActionResult> DownloadFaktura(int betalingsId)
        {
            try
            {
                // Genererer en PDF baseret på faktura-oplysninger for den givne betaling.
                var pdf = await _fakturaService.GenererFakturaPdfAsync(betalingsId);
                // Returnerer en fil med MIME-typen application/pdf og et foreslået filnavn.
                return File(pdf, "application/pdf", $"Faktura_{betalingsId}.pdf"); 
            }
            catch (Exception ex)
            {
                // Returnerer 400 Bad Request, hvis PDF-genereringen mislykkes.
                return BadRequest($"Fejl ved generering af faktura: {ex.Message}"); 
            }
        }

        [HttpPut("{betalingsId}/dato")] // En PUT-endpoint til at opdatere betalingsdato for en specifik betaling.
        public async Task<IActionResult> UpdateBetalingsDato(int betalingsId, [FromBody] DateTime? nyDato)
        {
            try
            {
                // Finder den betaling, der skal opdateres, ud fra ID.
                var betaling = await _betalingRepository.GetByIdAsync(betalingsId);
                if (betaling == null)
                {
                    // Returnerer 404 Not Found, hvis betalingen ikke eksisterer.
                    return NotFound($"Betaling med ID {betalingsId} blev ikke fundet."); 
                }

                // Opdaterer betalingen med den nye dato (kan være null).
                betaling.BetalingsDato = nyDato;
                // Gemmer ændringen i databasen via repositoriet.
                await _betalingRepository.UpdateAsync(betaling); 

                // Returnerer 200 OK med en besked om succesfuld opdatering.
                return Ok("Betalingsdato opdateret."); 
            }
            catch (Exception ex)
            {
                // Returnerer 400 Bad Request, hvis en generel fejl opstår.
                return BadRequest($"Fejl ved opdatering af betalingsdato: {ex.Message}"); 
            }
        }
    }
}