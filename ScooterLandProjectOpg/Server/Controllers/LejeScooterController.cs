using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LejeScooterController : ControllerBase
    {
        private readonly IRepository<LejeScooter> _lejeScooterRepository;

        public LejeScooterController(IRepository<LejeScooter> lejeScooterRepository)
        {
            _lejeScooterRepository = lejeScooterRepository;
        }

        // GET: api/lejescootere
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LejeScooter>>> GetAll()
        {
            var lejeScootere = await _lejeScooterRepository.GetAllAsync();
            return Ok(lejeScootere);
        }

        // GET: api/lejescootere/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LejeScooter>> GetById(int id)
        {
            var lejeScooter = await _lejeScooterRepository.GetByIdAsync(id);
            if (lejeScooter == null)
            {
                return NotFound($"LejeScooter with ID {id} not found.");
            }

            return Ok(lejeScooter);
        }

        // POST: api/lejescootere
        [HttpPost]
        public async Task<ActionResult<LejeScooter>> Add([FromBody] LejeScooter lejeScooter)
        {
            if (lejeScooter == null)
            {
                return BadRequest("LejeScooter data is null.");
            }

            var createdLejeScooter = await _lejeScooterRepository.AddAsync(lejeScooter);
            return CreatedAtAction(nameof(GetById), new { id = createdLejeScooter.LejeScooterId }, createdLejeScooter);
        }

        // PUT: api/lejescootere/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LejeScooter lejeScooter)
        {
            if (lejeScooter == null || lejeScooter.LejeScooterId != id)
            {
                return BadRequest("LejeScooter ID mismatch.");
            }

            var existingLejeScooter = await _lejeScooterRepository.GetByIdAsync(id);
            if (existingLejeScooter == null)
            {
                return NotFound($"LejeScooter with ID {id} not found.");
            }

            await _lejeScooterRepository.UpdateAsync(lejeScooter);
            return NoContent();
        }

        // DELETE: api/lejescootere/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingLejeScooter = await _lejeScooterRepository.GetByIdAsync(id);
            if (existingLejeScooter == null)
            {
                return NotFound($"LejeScooter with ID {id} not found.");
            }

            await _lejeScooterRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
