using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MekanikerController : ControllerBase
    {
        private readonly IMekanikerRepository _mekanikerRepository;

        public MekanikerController(IMekanikerRepository mekanikerRepository)
        {
            _mekanikerRepository = mekanikerRepository;
        }

        // GET: api/Mekaniker
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mekaniker>>> GetAll()
        {
            var mekanikere = await _mekanikerRepository.GetAllAsync();
            return Ok(mekanikere);
        }

        // GET: api/Mekaniker/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mekaniker>> GetById(int id)
        {
            var mekaniker = await _mekanikerRepository.GetByIdAsync(id);
            if (mekaniker == null)
            {
                return NotFound($"Mekaniker with ID {id} not found.");
            }
            return Ok(mekaniker);
        }

        // POST: api/Mekaniker
        [HttpPost]
        public async Task<ActionResult<Mekaniker>> Add([FromBody] Mekaniker mekaniker)
        {
            if (mekaniker == null)
            {
                return BadRequest("Invalid mekaniker data.");
            }

            var createdMekaniker = await _mekanikerRepository.AddAsync(mekaniker);
            return CreatedAtAction(nameof(GetById), new { id = createdMekaniker.MekanikerId }, createdMekaniker);
        }

        // PUT: api/Mekaniker/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Mekaniker mekaniker)
        {
            if (mekaniker == null || mekaniker.MekanikerId != id)
            {
                return BadRequest("Invalid mekaniker data.");
            }

            await _mekanikerRepository.UpdateAsync(mekaniker);
            return NoContent();
        }

        // DELETE: api/Mekaniker/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var mekaniker = await _mekanikerRepository.GetByIdAsync(id);
            if (mekaniker == null)
            {
                return NotFound($"Mekaniker with ID {id} not found.");
            }

            await _mekanikerRepository.DeleteAsync(id);
            return NoContent();
        }

		[HttpGet("{mekanikerId}/arbejdsopgaver")]
		public async Task<ActionResult<IEnumerable<OrdreYdelse>>> GetArbejdsopgaverForMekaniker(int mekanikerId)
		{
			try
			{
				var arbejdsopgaver = await _mekanikerRepository.GetArbejdsopgaverForMekanikerAsync(mekanikerId);

				// Returner altid en tom liste, hvis der ikke findes data
				if (arbejdsopgaver == null || !arbejdsopgaver.Any())
				{
					return Ok(new List<OrdreYdelse>());
				}

				return Ok(arbejdsopgaver);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Fejl ved hentning af arbejdsopgaver: {ex.Message}");
			}
		}
	}
}

