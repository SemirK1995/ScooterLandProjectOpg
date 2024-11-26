using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.DTO;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdreController : ControllerBase
    {
        private readonly IOrdreRepository _ordreRepository;
        private readonly ScooterLandContext _context;

        public OrdreController(IOrdreRepository ordreRepository, ScooterLandContext context)
        {
            _ordreRepository = ordreRepository;
            _context = context;
        }
        // GET: api/ordrer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Ordre>> GetById(int id)
        {
            // Hent ordren med detaljer via repository
            var ordre = await _ordreRepository.GetWithDetailsByIdAsync(id);

            if (ordre == null)
            {
                return NotFound($"Ordre with ID {id} not found.");
            }

            return Ok(ordre); // Returner ordredetaljerne
        }

        //[HttpPost]
        //public async Task<ActionResult<Ordre>> Add([FromBody] CreateOrdreDto ordreDTO)
        //{
        //    if (ordreDTO == null || ordreDTO.KundeId == 0 || ordreDTO.OrdreYdelser == null || !ordreDTO.OrdreYdelser.Any())
        //    {
        //        return BadRequest("Ordre data is invalid.");
        //    }

        //    // Skab en ny Ordre-entity fra DTO
        //    var ordre = new Ordre
        //    {
        //        KundeId = ordreDTO.KundeId,
        //        Dato = ordreDTO.Dato,
        //        TotalPris = ordreDTO.TotalPris,
        //        OrdreYdelse = ordreDTO.OrdreYdelser.Select(oy => new OrdreYdelse
        //        {
        //            YdelseId = oy.YdelseId,
        //            AftaltPris = oy.AftaltPris,
        //            Dato = oy.Dato
        //        }).ToList()
        //    };

        //    // Gem ordren
        //    _context.Ordrer.Add(ordre);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetById), new { id = ordre.OrdreId }, ordre);
        //}
        [HttpPost]
        public async Task<ActionResult<Ordre>> Add([FromBody] CreateOrdreDto ordreDTO)
        {
            if (ordreDTO == null || ordreDTO.KundeId == 0 || ordreDTO.OrdreYdelser == null || !ordreDTO.OrdreYdelser.Any())
            {
                return BadRequest("Ordre data is invalid.");
            }

            // Skab en ny Ordre-entity fra DTO
            var ordre = new Ordre
            {
                KundeId = ordreDTO.KundeId,
                Dato = ordreDTO.Dato,
                TotalPris = ordreDTO.TotalPris,
                OrdreYdelse = ordreDTO.OrdreYdelser.Select(oy => new OrdreYdelse
                {
                    YdelseId = oy.YdelseId,
                    AftaltPris = oy.AftaltPris.HasValue ? oy.AftaltPris.Value : 0, // Fjern standardprisen, hvis tom
                    Dato = oy.Dato
                }).ToList()
            };

            try
            {
                _context.Ordrer.Add(ordre); // Tilføj ordren til databasen
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = ordre.OrdreId }, ordre);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved oprettelse af ordre: {ex.Message}");
                return StatusCode(500, "Der opstod en fejl ved oprettelse af ordren.");
            }
        }
    }
}
