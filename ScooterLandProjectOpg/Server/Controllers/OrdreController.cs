using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdreController : ControllerBase
    {
        private readonly IOrdreRepository _ordreRepository;

        public OrdreController(IOrdreRepository ordreRepository)
        {
            _ordreRepository = ordreRepository;
        }

        // POST: api/ordrer
        [HttpPost]
        public async Task<ActionResult<Ordre>> Add([FromBody] Ordre ordre)
        {
            if (ordre == null)
            {
                return BadRequest("Ordre data is null.");
            }

            var createdOrdre = await _ordreRepository.AddAsync(ordre);
            return CreatedAtAction(nameof(GetById), new { id = createdOrdre.OrdreId }, createdOrdre);
        }

        // GET: api/ordrer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Ordre>> GetById(int id)
        {
            var ordre = await _ordreRepository.GetWithDetailsByIdAsync(id);
            if (ordre == null)
            {
                return NotFound($"Ordre with ID {id} not found.");
            }

            return Ok(ordre);
        }
    }
}
