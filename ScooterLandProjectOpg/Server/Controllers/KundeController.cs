using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class KundeController : ControllerBase
	{
		private readonly IKundeRepository _kundeRepository;

		public KundeController(IKundeRepository kundeRepository)
		{
			_kundeRepository = kundeRepository;
		}

		// GET: api/Kunde
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Kunde>>> GetAll()
		{
			var kunder = await _kundeRepository.GetAllAsync();
			return Ok(kunder);
		}

		// GET: api/Kunde/with-orders
		[HttpGet("with-orders")]
		public async Task<ActionResult<IEnumerable<Kunde>>> GetAllWithOrders()
		{
			var kunder = await _kundeRepository.GetAllWithOrdersAsync();
			return Ok(kunder);
		}
		// GET: api/Kunde/with-scooters/5
		[HttpGet("with-scooters/{id}")]
		public async Task<ActionResult<Kunde>> GetKundeWithScooters(int id)
		{
			var kunde = await _kundeRepository.GetKundeWithScootersAsync(id);

			if (kunde == null)
			{
				return NotFound($"Kunde with ID {id} not found.");
			}

			return Ok(kunde);
		}

		//Eksempel GET: api/Kunde/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Kunde>> GetById(int id)
		{
			var kunde = await _kundeRepository.GetByIdAsync(id);
			if (kunde == null)
			{
				return NotFound();
			}
			return Ok(kunde);
		}

		// GET: api/Kunde/search?name=John
		[HttpGet("search")]
		public async Task<ActionResult<IEnumerable<Kunde>>> SearchByName([FromQuery] string name)
		{
			var kunder = await _kundeRepository.SearchByNameAsync(name);
			return Ok(kunder);
		}

		// POST: api/Kunde
		[HttpPost]
		public async Task<ActionResult<Kunde>> Add([FromBody] Kunde kunde)
		{
			if (kunde == null)
			{
				return BadRequest("Kunde data is null.");
			}

			var createdKunde = await _kundeRepository.AddAsync(kunde);
			return CreatedAtAction(nameof(GetById), new { id = createdKunde.KundeId }, createdKunde);
		}

		// PUT: api/Kunde/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] Kunde kunde)
		{
			if (kunde == null || kunde.KundeId != id)
			{
				return BadRequest("Invalid Kunde data.");
			}

			await _kundeRepository.UpdateAsync(kunde);
			return NoContent();
		}

		// DELETE: api/Kunde/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _kundeRepository.DeleteAsync(id);
			return NoContent();
		}
		
        [HttpGet("{kundeId}/ordrer")]
        public async Task<ActionResult<IEnumerable<Ordre>>> GetKundeOrdrer(int kundeId)
        {
            var ordrer = await _kundeRepository.GetOrdrerForKundeAsync(kundeId);

            if (!ordrer.Any())
            {
                return NotFound("Denne kunde har ingen ordrer.");
            }

            return Ok(ordrer);
        }
    }
}
