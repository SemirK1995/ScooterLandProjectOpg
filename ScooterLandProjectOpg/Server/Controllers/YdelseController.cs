using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YdelseController : ControllerBase
    {
        private readonly IRepository<Ydelse> _ydelseRepository;

        public YdelseController(IRepository<Ydelse> ydelseRepository)
        {
            _ydelseRepository = ydelseRepository;
        }

        // GET: api/Ydelse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ydelse>>> GetAll()
        {
            var ydelser = await _ydelseRepository.GetAllAsync();
            return Ok(ydelser);
        }

        // GET: api/ydelser/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Ydelse>> GetById(int id)
        {
            var ydelse = await _ydelseRepository.GetByIdAsync(id);
            if (ydelse == null)
            {
                return NotFound($"Ydelse with ID {id} not found.");
            }
            return Ok(ydelse);
        }

        // POST: api/ydelser
        [HttpPost]
        public async Task<ActionResult<Ydelse>> Add([FromBody] Ydelse ydelse)
        {
            if (ydelse == null)
            {
                return BadRequest("Ydelse data is null.");
            }

            var createdYdelse = await _ydelseRepository.AddAsync(ydelse);
            return CreatedAtAction(nameof(GetById), new { id = createdYdelse.YdelseId }, createdYdelse);
        }

        // PUT: api/ydelser/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Ydelse ydelse)
        {
            if (ydelse == null || ydelse.YdelseId != id)
            {
                return BadRequest("Ydelse ID mismatch.");
            }

            var existingYdelse = await _ydelseRepository.GetByIdAsync(id);
            if (existingYdelse == null)
            {
                return NotFound($"Ydelse with ID {id} not found.");
            }

            await _ydelseRepository.UpdateAsync(ydelse);
            return NoContent();
        }

        // DELETE: api/ydelser/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingYdelse = await _ydelseRepository.GetByIdAsync(id);
            if (existingYdelse == null)
            {
                return NotFound($"Ydelse with ID {id} not found.");
            }

            await _ydelseRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
