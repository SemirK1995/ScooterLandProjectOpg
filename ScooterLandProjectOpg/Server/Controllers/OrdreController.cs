using Microsoft.AspNetCore.Http; // Gør HTTP-relaterede klasser og StatusCodes tilgængelige.
using Microsoft.AspNetCore.Mvc; // Giver adgang til MVC-funktionalitet, som ControllerBase, ActionResult, osv.
using Microsoft.EntityFrameworkCore; // Muliggør brug af EF Core-funktioner, fx til databaseopslag.
using ScooterLandProjectOpg.Client; // Inkluderer klient-relateret kode, hvis der er behov for at referere til klient-side aspekter.
using ScooterLandProjectOpg.Server.Context; // Giver adgang til ScooterLandContext, der repræsenterer databasen.
using ScooterLandProjectOpg.Server.Services.Interfaces;
using ScooterLandProjectOpg.Shared.DTO; // Importerer data transfer objekter, fx CreateOrdreDto, der bruges til at oprette ordrer.
using ScooterLandProjectOpg.Shared.Enum; // Giver adgang til enum-typer, eksempelvis OrdreStatus.
using ScooterLandProjectOpg.Shared.Models; // Giver adgang til model-klasser som Ordre, Betaling, LejeAftale osv.

namespace ScooterLandProjectOpg.Server.Controllers // Angiver, at denne controller hører til i ScooterLandProjectOpg.Server.Controllers-namespace.

{
    // Marker klassen som en API-controller, der håndterer HTTP-requests på ruten 'api/Ordre'.
    [Route("api/[controller]")]
    [ApiController]

    // Arver fra ControllerBase, hvilket giver grundlæggende funktioner til en web-API.
    public class OrdreController : ControllerBase
    {
        private readonly IOrdreService _ordreRepository; // Felt til at holde en instans af IOrdreRepository, der håndterer databasekald for ordrer.
        private readonly ScooterLandContext _context; // Databasekontekst, så vi kan lave ændringer i databasen direkte (ud over repository).

        // Constructor, der injicerer IOrdreRepository og ScooterLandContext.
        public OrdreController(IOrdreService ordreRepository, ScooterLandContext context)
        {
            _ordreRepository = ordreRepository;
            _context = context;
        }

        // GET: api/ordrer/{id}
        [HttpGet("{id}")] // Endpoint til at hente en specifik ordre inklusiv detaljer.
        public async Task<ActionResult<Ordre>> GetById(int id)
        {
            // Bruger repository til at hente ordren med dens tilknyttede detaljer.
            var ordre = await _ordreRepository.GetWithDetailsByIdAsync(id);

            // Returnerer 404 Not Found, hvis ordren ikke eksisterer.
            if (ordre == null)
            {
                return NotFound($"Ordre with ID {id} not found."); 
            }

            // Returnerer 200 OK med ordren, hvis den blev fundet.
            return Ok(ordre); 
        }

        // GET: api/Mekaniker
        [HttpGet] // Endpoint til at hente en liste af ordrer.
        public async Task<ActionResult<IEnumerable<Ordre>>> GetAll()
        {
            // Henter alle ordrer fra databasen via repository.
            var ordre = await _ordreRepository.GetAllAsync();
            // Returnerer 200 OK med listen af ordrer.
            return Ok(ordre); 
        }

        // Metode til at oprette en ny ordre baseret på et CreateOrdreDto-objekt.
        public async Task<ActionResult<Ordre>> Add([FromBody] CreateOrdreDto ordreDTO)
        {
            if (ordreDTO == null || ordreDTO.KundeId == 0)
            {
                // Returnerer 400 Bad Request, hvis DTO er null eller KundeId mangler.
                return BadRequest("Ordre data is invalid."); 
            }

            try
            {
                var ordre = new Ordre
                {
                    KundeId = ordreDTO.KundeId, // Sætter KundeId fra DTO'en.
                    Dato = ordreDTO.Dato, // Bruger Dato fra DTO'en, ellers standardværdi.
                    TotalPris = 0, // Start på 0 og læg til, når ydelser og produkter er beregnet
                    OrdreYdelse = ordreDTO.OrdreYdelser?.Select(oy => new OrdreYdelse // Mapper OrdreYdelse fra DTO til en liste af OrdreYdelse-objekter. 
                    {
                        YdelseId = oy.YdelseId, 
                        AftaltPris = oy.AftaltPris ?? 0, // Bruger enten aftalt pris eller 0, hvis den ikke er sat.
                        Dato = oy.Dato ?? DateTime.Now, // Bruger enten givet dato eller nuværende tidspunkt som fallback.
                        ScooterId = oy.ScooterId // ScooterId bestemmer, hvilken kunde-scooter ydelsen knyttes til.
                    }).ToList()
                };

                // Tjek om alle ydelser har en scooter valgt
                if (ordre.OrdreYdelse != null && ordre.OrdreYdelse.Any(oy => oy.ScooterId == null))
                {
                    // Forhindrer oprettelse, hvis nogen ydelser ikke har en scooter.
                    return BadRequest("Alle ydelser skal have en tilknyttet scooter."); 
                }

                // Beregn totalpris fra ydelser
                if (ordre.OrdreYdelse != null && ordre.OrdreYdelse.Any())
                {
                    foreach (var ydelse in ordre.OrdreYdelse)
                    {
                        // Brug aftalt pris, hvis > 0, ellers find ydelsens standardpris i databasen.
                        var ydelsePris = ydelse.AftaltPris > 0
                            ? ydelse.AftaltPris
                            : (await _context.Ydelser.FindAsync(ydelse.YdelseId))?.StandardPris ?? 0;

                        // Læg ydelsens pris til ordre-totalen.
                        ordre.TotalPris += ydelsePris;
                    }
                }

                // Hvis der er en lejeaftale i DTO'en, håndteres den her
                if (ordreDTO.LejeAftale != null)
                {
                    // Opret et nyt LejeAftale-objekt baseret på de givne data.
                    var nyLejeAftale = new LejeAftale
                    {
                        KundeId = ordreDTO.KundeId,
                        StartDato = ordreDTO.LejeAftale.StartDato,
                        SlutDato = ordreDTO.LejeAftale.SlutDato,
                        DagligLeje = ordreDTO.LejeAftale.DagligLeje,
                        ForsikringsPris = ordreDTO.LejeAftale.ForsikringsPris,
                        KilometerPris = ordreDTO.LejeAftale.KilometerPris,
                        KortKilometer = ordreDTO.LejeAftale.KortKilometer
                    };
                    
                    // Tilføj lejeaftalen til konteksten.
                    _context.LejeAftaler.Add(nyLejeAftale); 
                    // Gem ændringer for at få genereret en LejeId.
                    await _context.SaveChangesAsync(); 

                    // Sæt ordre.LejeId til den netop oprettede lejeaftales ID.
                    ordre.LejeId = nyLejeAftale.LejeId; 
                    // Læg lejeaftalens totalpris til ordretotalen, hvis relevant.
                    ordre.TotalPris += nyLejeAftale.TotalPris; 
                }

                // Tilføj ordren til databasen
                _context.Ordrer.Add(ordre); 
                // Gem ændringer for at få et OrdreId.
                await _context.SaveChangesAsync(); 

                if (ordreDTO.LejeAftale?.LejeScooterId != null)
                {
                    // Find den valgte leje-scooter.
                    var lejeScooter = await _context.LejeScootere.FindAsync(ordreDTO.LejeAftale.LejeScooterId);
                    if (lejeScooter == null)
                    {
                        // Returnerer fejl, hvis scooteren ikke findes.
                        return BadRequest($"Scooter med ID {ordreDTO.LejeAftale.LejeScooterId} findes ikke.");
                    }
                    
                    // Tildeler scooteren den oprettede lejeaftale og markerer den som ikke længere ledig.
                    lejeScooter.LejeId = ordre.LejeId;
                    lejeScooter.ErTilgængelig = false; // Gør scooteren utilgængelig, da den er lejet
                    _context.LejeScootere.Update(lejeScooter);
                }

                // Gem eventuelle ændringer vedr. leje-scooter
                await _context.SaveChangesAsync(); 

                // Håndter produkter
                if (ordreDTO.OrdreProdukter != null && ordreDTO.OrdreProdukter.Any())
                {
                    foreach (var produktDTO in ordreDTO.OrdreProdukter)
                    {
                        // Find produktet i databasen.
                        var produkt = await _context.Produkter.FindAsync(produktDTO.ProduktId);
                        if (produkt == null) return BadRequest($"Produkt med ID {produktDTO.ProduktId} findes ikke.");
                        // Tjek lagerbeholdning.
                        if (produkt.LagerAntal < produktDTO.KøbsAntal) return BadRequest($"Ikke nok på lager for produkt: {produkt.ProduktNavn}.");

                        // Reducer lagerantallet baseret på det købte antal.
                        produkt.LagerAntal -= produktDTO.KøbsAntal;
                        _context.Produkter.Update(produkt);

                        // Opret et OrdreProdukt-objekt, der beskriver, hvilke produkter der er købt i denne ordre.
                        var ordreProdukt = new OrdreProdukt
                        {
                            ProduktId = produkt.ProduktId,
                            OrdreId = ordre.OrdreId,
                            Antal = produktDTO.KøbsAntal,
                            Pris = produkt.Pris ?? 0
                        };
                        // Tilføj koblingen mellem ordre og produkt til konteksten.
                        _context.OrdreProdukter.Add(ordreProdukt);
                        // Læg produktets pris * antal til den samlede ordre-total.
                        ordre.TotalPris += (produkt.Pris ?? 0) * produktDTO.KøbsAntal;
                    }
                }

                // Gem ændringer for at opdatere ordre-totalen og lageret.
                await _context.SaveChangesAsync(); 

                // Opret en betaling for ordren
                var betaling = new Betaling
                {
                    OrdreId = ordre.OrdreId,
                    Beløb = ordre.TotalPris,
                    Betalt = false
                };
                
                // Tilføj betalingen til konteksten.
                _context.Betalinger.Add(betaling);
                // Gem ændringer for at oprette betalingen i databasen.
                await _context.SaveChangesAsync();

                // Returnerer 201 Created med den nyligt oprettede ordre.
                return CreatedAtAction(nameof(GetById), new { id = ordre.OrdreId }, ordre);
            }
            catch (Exception ex)
            {
                // Returnerer 500 Internal Server Error, hvis en uventet fejl opstår.
                return StatusCode(500, $"Fejl under oprettelse af ordre: {ex.Message}");
            }
        }

        [HttpPut("{ordreId}/status")] // PUT-endpoint på 'api/Ordre/{ordreId}/status' til at opdatere en ordres status.
        public async Task<IActionResult> UpdateOrdreStatus(int ordreId, [FromBody] OrdreStatus nyStatus)
        {
            try
            {
                // Opdaterer ordren i databasen med den nye status.
                await _ordreRepository.UpdateOrdreStatusAsync(ordreId, nyStatus);
                // Returnerer 200 OK med en succesbesked.
                return Ok(new { message = "Ordrestatus opdateret med succes." });
            }
            catch (KeyNotFoundException ex)
            {
                // Returnerer 404, hvis ordren ikke findes.
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Returnerer 500, hvis en serverfejl opstår.
                return StatusCode(500, new { message = "Der opstod en fejl under opdatering af ordrestatus.", error = ex.Message });
            }
        }
    }
}