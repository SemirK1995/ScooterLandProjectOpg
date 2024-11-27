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

		[HttpPost]
		public async Task<ActionResult<Ordre>> Add([FromBody] CreateOrdreDto ordreDTO)
		{
			if (ordreDTO == null || ordreDTO.KundeId == 0)
			{
				return BadRequest("Ordre data is invalid.");
			}

			try
			{
				// Skab en ny Ordre fra DTO
				var ordre = new Ordre
				{
					KundeId = ordreDTO.KundeId,
					Dato = ordreDTO.Dato,
					TotalPris = ordreDTO.TotalPris,
					OrdreYdelse = ordreDTO.OrdreYdelser?.Select(oy => new OrdreYdelse
					{
						YdelseId = oy.YdelseId,
						AftaltPris = oy.AftaltPris ?? 0,
						Dato = oy.Dato ?? DateTime.Now
					}).ToList()
				};

				// Håndter lejeaftalen, hvis en er valgt
				if (ordreDTO.LejeAftale != null)
				{
					// Overfør KundeId, hvis det ikke er sat
					if (ordreDTO.LejeAftale.KundeId == 0)
					{
						ordreDTO.LejeAftale.KundeId = ordreDTO.KundeId;
					}
					// Skab lejeaftalen
					var lejeAftale = new LejeAftale
					{
						KundeId = ordreDTO.LejeAftale.KundeId,
						StartDato = ordreDTO.LejeAftale.StartDato,
						SlutDato = ordreDTO.LejeAftale.SlutDato,
						DagligLeje = ordreDTO.LejeAftale.DagligLeje,
						Forsikring = ordreDTO.LejeAftale.Forsikring,
						KilometerPris = ordreDTO.LejeAftale.KilometerPris,
						Selvrisiko = ordreDTO.LejeAftale.Selvrisiko,
						KortKilometer = ordreDTO.LejeAftale.KortKilometer,
					};

					// Tilføj lejeaftalen til databasen og gem for at få LejeId
					_context.LejeAftaler.Add(lejeAftale);
					await _context.SaveChangesAsync(); // Gem her for at få genereret LejeId

					// Valider og opdater scooteren, hvis en scooter er valgt
					if (ordreDTO.LejeAftale.LejeScooterId > 0)
					{
						var lejeScooter = await _context.LejeScootere.FindAsync(ordreDTO.LejeAftale.LejeScooterId);
						if (lejeScooter == null || !lejeScooter.ErTilgængelig)
						{
							return BadRequest("Den valgte scooter er ikke tilgængelig eller eksisterer ikke.");
						}

						lejeScooter.LejeId = lejeAftale.LejeId;
						lejeScooter.StartDato = ordreDTO.LejeAftale.StartDato;
						lejeScooter.SlutDato = ordreDTO.LejeAftale.SlutDato;
						lejeScooter.ErTilgængelig = false;

						_context.LejeScootere.Update(lejeScooter);
					}

					// Tilføj lejeaftalens pris til ordren
					ordre.TotalPris += lejeAftale.TotalPris;
				}

				// Tilføj ordren til databasen
				_context.Ordrer.Add(ordre);
				await _context.SaveChangesAsync();

				return CreatedAtAction(nameof(GetById), new { id = ordre.OrdreId }, ordre);
			}
			catch (DbUpdateException dbEx)
			{
				Console.WriteLine($"Database fejl ved oprettelse af ordre: {dbEx.Message}");
				return StatusCode(500, $"Databasefejl: {dbEx.Message}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Generel fejl ved oprettelse af ordre: {ex.Message}");
				return StatusCode(500, $"Fejl: {ex.Message}");
			}
		}



	}
}
