using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.DTO;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdreController : ControllerBase
	{
		private readonly IOrdreRepository _ordreRepository;
		private readonly ScooterLandContext _context;

		public OrdreController(IOrdreRepository ordreRepository, ScooterLandContext context)
		{
			_ordreRepository = ordreRepository;
			_context = context;
		}
		// GET: api/ordrer/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<Ordre>> GetById(int id)
		{
			// Hent ordren med detaljer via repository
			var ordre = await _ordreRepository.GetWithDetailsByIdAsync(id);

			if (ordre == null)
			{
				return NotFound($"Ordre with ID {id} not found.");
			}

			return Ok(ordre); // Returner ordredetaljerne
		}

		public async Task<ActionResult<Ordre>> Add([FromBody] CreateOrdreDto ordreDTO)
		{
			if (ordreDTO == null || ordreDTO.KundeId == 0 || ordreDTO.OrdreYdelser == null || !ordreDTO.OrdreYdelser.Any())
			{
				return BadRequest("Ordre data er ugyldigt.");
			}

			// Skab en ny Ordre-entity fra DTO
			var ordre = new Ordre
			{
				KundeId = ordreDTO.KundeId,
				Dato = ordreDTO.Dato,
				TotalPris = ordreDTO.TotalPris,
				OrdreYdelse = ordreDTO.OrdreYdelser.Select(oy => new OrdreYdelse
				{
					YdelseId = oy.YdelseId,
					AftaltPris = oy.AftaltPris.HasValue ? oy.AftaltPris.Value : 0,
					Dato = oy.Dato
				}).ToList()
			};

			// Hvis der er en lejeaftale inkluderet
			if (ordreDTO.LejeAftale != null)
			{
				var lejeAftale = new LejeAftale
				{
					KundeId = ordreDTO.KundeId, // Sørg for korrekt kunde
					StartDato = ordreDTO.LejeAftale.StartDato,
					SlutDato = ordreDTO.LejeAftale.SlutDato,
					DagligLeje = ordreDTO.LejeAftale.DagligLeje,
					Forsikring = ordreDTO.LejeAftale.Forsikring,
					KilometerPris = ordreDTO.LejeAftale.KilometerPris,
					Selvrisiko = ordreDTO.LejeAftale.Selvrisiko,
					KortKilometer = ordreDTO.LejeAftale.KortKilometer
				};

				_context.LejeAftaler.Add(lejeAftale);
				await _context.SaveChangesAsync(); // Gem for at få LejeId

				if (ordreDTO.LejeAftale.LejeScootere != null)
				{
					foreach (var scooterDto in ordreDTO.LejeAftale.LejeScootere)
					{
						var existingScooter = await _context.LejeScootere.FindAsync(scooterDto.LejeScooterId);
						if (existingScooter != null)
						{
							existingScooter.LejeId = lejeAftale.LejeId; // Knyt scooter til lejeaftale
							existingScooter.ErTilgængelig = false;      // Markér scooter som ikke-tilgængelig
							_context.LejeScootere.Update(existingScooter); // Opdater scooteren
						}
					}
				}

				ordre.TotalPris += lejeAftale.TotalPris; // Tilføj lejeaftalens pris
			}

			try
			{
				_context.Ordrer.Add(ordre);
				await _context.SaveChangesAsync(); // Gem både ordre og opdateringer til scootere
				return CreatedAtAction(nameof(GetById), new { id = ordre.OrdreId }, ordre);
			}
			catch (DbUpdateException ex)
			{
				Console.WriteLine($"Database fejl: {ex.Message}");
				return StatusCode(500, "Databasefejl opstod ved oprettelse af ordren.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Generel fejl: {ex.Message}");
				return StatusCode(500, "En uventet fejl opstod.");
			}
		}


	}
}
