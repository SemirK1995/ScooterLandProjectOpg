using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ScooterLandProjectOpg.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LejeScooterController : ControllerBase
    {
        private readonly ILejeScooterRepository _lejeScooterRepository;

        public LejeScooterController(ILejeScooterRepository lejeScooterRepository)
        {
            _lejeScooterRepository = lejeScooterRepository;
        }
        
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<LejeScooter>>> GetAvailableScooters()
        {
            var availableScooters = await _lejeScooterRepository.GetScootersAvailableAsync();
            if (availableScooters == null || !availableScooters.Any())
            {
                return NotFound("Ingen ledige scootere fundet.");
            }
            return Ok(availableScooters);
        }

        [HttpPut("{id}/assign")]
        public async Task<IActionResult> AssignScooterToLeje(int id, [FromBody] int lejeId)
        {
            try
            {
                await _lejeScooterRepository.UpdateScooterLejeIdAsync(id, lejeId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
