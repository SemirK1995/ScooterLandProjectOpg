using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Server.Services;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProduktController : ControllerBase
	{
		private readonly IProduktRepository _produktRepository;

		public ProduktController (IProduktRepository produktRepository)
		{
			_produktRepository = produktRepository;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Produkt>>> GetAll()
		{
			return Ok(await _produktRepository.GetAllAsync());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Produkt>> GetById(int id)
		{
			var produkt = await _produktRepository.GetByIdAsync(id);
			if (produkt == null)
			{
				return NotFound();
			}
			return Ok(produkt);
		}

		[HttpPost]
		public async Task<ActionResult<Produkt>> Add([FromBody] Produkt produkt)
		{
			var createdProdukt = await _produktRepository.AddAsync(produkt);
			return CreatedAtAction(nameof(GetById), new { id = createdProdukt.ProduktId }, createdProdukt);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] Produkt produkt)
		{
			if (id != produkt.ProduktId)
			{
				return BadRequest("ID mismatch.");
			}

			try
			{
				await _produktRepository.UpdateAsync(produkt);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _produktRepository.DeleteAsync(id);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}

			return NoContent();
		}
	}
}
