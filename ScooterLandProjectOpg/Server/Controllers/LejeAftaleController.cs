using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LejeAftaleController : ControllerBase
    {
        private readonly ILejeAftaleRepository _lejeaftaleRepository;

        public LejeAftaleController(ILejeAftaleRepository lejeaftaleRepository)
        {
            _lejeaftaleRepository = lejeaftaleRepository;
        }
        // POST: api/lejeaftaler
        [HttpPost]
        public async Task<ActionResult<LejeAftale>> Add([FromBody] LejeAftale lejeAftale)
        {
            if (lejeAftale == null)
            {
                return BadRequest("Lejeaftale data is null.");
            }

            var createdLejeaftale = await _lejeaftaleRepository.AddAsync(lejeAftale);
            return CreatedAtAction(nameof(GetById), new { id = createdLejeaftale.LejeId }, createdLejeaftale);
        }
        // GET: api/lejeaftaler/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LejeAftale>> GetById(int id)
        {
            var lejeaftale = await _lejeaftaleRepository.GetLejeAftaleWithDetailsAsync(id);
            if (lejeaftale == null)
            {
                return NotFound($"Lejeaftale with ID {id} not found.");
            }

            return Ok(lejeaftale);
        }
    }
}
