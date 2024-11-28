using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.Models;
using ScooterLandProjectOpg.Shared.DTO;

namespace ScooterLandProjectOpg.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdreYdelseController : ControllerBase
	{
		private readonly IOrdreYdelseRepository _ordreYdelseRepository;

		public OrdreYdelseController(IOrdreYdelseRepository ordreYdelseRepository)
		{
			_ordreYdelseRepository = ordreYdelseRepository;
		}

		[HttpGet("ikke-tildelt")]
		public async Task<ActionResult<List<OrdreYdelseDto>>> GetIkkeTildelteOrdreYdelser()
		{
			var ordreYdelser = await _ordreYdelseRepository.GetAllWithDetailsAsync();

			var ikkeTildelteOrdreYdelser = ordreYdelser
				.Where(oy => oy.MekanikerId == null)
				.Select(oy => new OrdreYdelseDto
				{
					OrdreYdelseId = oy.OrdreYdelseId,
					YdelseNavn = oy.Ydelse?.Navn,
					ScooterMaerke = oy.Scooter?.Maerke,
					ScooterModel = oy.Scooter?.Model,
					ProduktionsAar = oy.Scooter?.ProduktionsAar.ToString(),
					StartDato = oy.StartDato,
					SlutDato = oy.SlutDato,
					Timer = oy.Timer
				})
				.ToList();

			if (!ikkeTildelteOrdreYdelser.Any())
			{
				return NotFound("Ingen ordreydelser fundet.");
			}

			return Ok(ikkeTildelteOrdreYdelser);
		}


		[HttpPut("tildel")]
		public async Task<IActionResult> TildelMekanikerTilOrdreYdelse([FromBody] TildelMekanikerDto dto)
		{
			var ordreYdelse = await _ordreYdelseRepository.GetWithDetailsByIdAsync(dto.OrdreYdelseId);
			if (ordreYdelse == null)
				return NotFound("OrdreYdelse blev ikke fundet.");

			//Tilføjer værdier fra DTO
			ordreYdelse.MekanikerId = dto.MekanikerId;
			ordreYdelse.StartDato = dto.StartDato;
			ordreYdelse.SlutDato = dto.SlutDato;
			if (dto.Timer.HasValue)
				ordreYdelse.Timer = dto.Timer.Value;

			await _ordreYdelseRepository.UpdateAsync(ordreYdelse);

			return Ok("Mekaniker tildelt.");
		}




	}
}
