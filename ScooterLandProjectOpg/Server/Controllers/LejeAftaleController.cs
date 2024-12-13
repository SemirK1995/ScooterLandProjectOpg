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
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<LejeAftale>>> SearchLejeAftaler([FromQuery] string query)
        {
            var lejeAftaler = await _lejeaftaleRepository.SearchLejeAftalerAsync(query);
            return Ok(lejeAftaler);
        }

        [HttpPut("{lejeId}/selvrisiko")]
        public async Task<IActionResult> UpdateSelvrisiko(int lejeId, [FromBody] double selvrisiko)
        {
            try
            {
                await _lejeaftaleRepository.UpdateSelvrisikoAsync(lejeId, selvrisiko);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
		[HttpPut("{lejeId}/kilometer")]
		public async Task<IActionResult> UpdateKortKilometer(int lejeId, [FromBody] int kortKilometer)
		{
			try
			{
				// Opdater kilometer og hent den opdaterede lejeaftale
				var lejeAftale = await _lejeaftaleRepository.UpdateKortKilometerAsync(lejeId, kortKilometer);

				if (lejeAftale == null)
				{
					return NotFound($"Lejeaftale med ID {lejeId} blev ikke fundet.");
				}

				// Returner den opdaterede lejeaftale eller ordre
				return Ok(lejeAftale.Ordre);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Der opstod en fejl: {ex.Message}");
			}
		}
		[HttpGet]
        public async Task<ActionResult<IEnumerable<LejeAftale>>> GetAllLejeAftaler()
        {
            try
            {
                var lejeAftaler = await _lejeaftaleRepository.GetAllWithKundeAndScootersAsync();
                return Ok(lejeAftaler);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Serverfejl: {ex.Message}");
            }
        }
    }
}
