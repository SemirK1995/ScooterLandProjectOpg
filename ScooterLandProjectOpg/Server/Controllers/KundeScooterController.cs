using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KundeScooterController : ControllerBase
    {
        private readonly IKundeScooterRepository _kundeScooterRepository;

        public KundeScooterController (IKundeScooterRepository kundeScooterRepository)
        {
            _kundeScooterRepository = kundeScooterRepository;
        }
        [HttpPost("{kundeId}/add-scooter")]
        public async Task<IActionResult> AddScooterToKunde(int kundeId, [FromBody] KundeScooter scooter)
        {
            if (scooter == null)
            {
                return BadRequest("Scooter-data er ugyldig.");
            }

            scooter.KundeId = kundeId; // Sæt relationen til kunden
            var oprettetScooter = await _kundeScooterRepository.AddScooterAsync(scooter);

            if (oprettetScooter == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Fejl ved oprettelse af scooter.");
            }

            return Ok(oprettetScooter);
        }
        [HttpGet("{kundeId}/scootere")]
        public async Task<ActionResult<List<KundeScooter>>> GetScootersForKunde(int kundeId)
        {
            var scootere = await _kundeScooterRepository.GetScootersByKundeIdAsync(kundeId);
               

            if (!scootere.Any())
            {
                return NotFound("Ingen scootere fundet for denne kunde.");
            }

            return Ok(scootere);
        }

		[HttpGet("all")]
		public async Task<ActionResult<IEnumerable<KundeScooter>>> GetAllScootersWithKunder()
		{
			var allScooters = await _kundeScooterRepository.GetScootersWithKundeAsync();

			if (allScooters == null || !allScooters.Any())
			{
				return NotFound("Ingen scootere fundet.");
			}

			return Ok(allScooters);
		}



	}
}
