﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Client;
using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Shared.DTO;
using ScooterLandProjectOpg.Shared.Enum;
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
        // GET: api/Mekaniker
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ordre>>> GetAll()
        {
            var ordre = await _ordreRepository.GetAllAsync();
			return Ok(ordre);
        }
		public async Task<ActionResult<Ordre>> Add([FromBody] CreateOrdreDto ordreDTO)
		{
			if (ordreDTO == null || ordreDTO.KundeId == 0)
			{
				return BadRequest("Ordre data is invalid.");
			}

			try
			{
				var ordre = new Ordre
				{
					KundeId = ordreDTO.KundeId,
					Dato = ordreDTO.Dato,
					TotalPris = 0, // Start med 0 og beregn totalen senere
					OrdreYdelse = ordreDTO.OrdreYdelser?.Select(oy => new OrdreYdelse
					{
						YdelseId = oy.YdelseId,
						AftaltPris = oy.AftaltPris ?? 0,
						Dato = oy.Dato ?? DateTime.Now,
						ScooterId = oy.ScooterId
					}).ToList()
				};

				// Valider, at alle ydelser har en scooter valgt
				if (ordre.OrdreYdelse != null && ordre.OrdreYdelse.Any(oy => oy.ScooterId == null))
				{
					return BadRequest("Alle ydelser skal have en tilknyttet scooter.");
				}

				// Beregn totalpris fra ydelser
				if (ordre.OrdreYdelse != null && ordre.OrdreYdelse.Any())
				{
					foreach (var ydelse in ordre.OrdreYdelse)
					{
						// Brug AftaltPris hvis tilgængelig, ellers StandardPris
						var ydelsePris = ydelse.AftaltPris > 0
							? ydelse.AftaltPris
							: (await _context.Ydelser.FindAsync(ydelse.YdelseId))?.StandardPris ?? 0;

						ordre.TotalPris += ydelsePris;
					}
				}
				// Håndter lejeaftale, hvis der er en
				if (ordreDTO.LejeAftale != null)
				{
					var nyLejeAftale = new LejeAftale
					{
						KundeId = ordreDTO.KundeId,
						StartDato = ordreDTO.LejeAftale.StartDato,
						SlutDato = ordreDTO.LejeAftale.SlutDato,
						DagligLeje = ordreDTO.LejeAftale.DagligLeje,
						ForsikringsPris = ordreDTO.LejeAftale.ForsikringsPris,
						KilometerPris = ordreDTO.LejeAftale.KilometerPris,
						KortKilometer = ordreDTO.LejeAftale.KortKilometer
					};
					_context.LejeAftaler.Add(nyLejeAftale);
					await _context.SaveChangesAsync();

					ordre.LejeId = nyLejeAftale.LejeId;
					ordre.TotalPris += nyLejeAftale.TotalPris;
				}

				_context.Ordrer.Add(ordre);
				await _context.SaveChangesAsync();

				if (ordreDTO.LejeAftale?.LejeScooterId != null)
				{
					var lejeScooter = await _context.LejeScootere.FindAsync(ordreDTO.LejeAftale.LejeScooterId);
					if (lejeScooter == null)
					{
						return BadRequest($"Scooter med ID {ordreDTO.LejeAftale.LejeScooterId} findes ikke.");
					}

					lejeScooter.LejeId = ordre.LejeId;
					lejeScooter.ErTilgængelig = false; // Scooteren er nu ikke længere tilgængelig
					_context.LejeScootere.Update(lejeScooter);
				}

				await _context.SaveChangesAsync();

				// Håndter produkter
				if (ordreDTO.OrdreProdukter != null && ordreDTO.OrdreProdukter.Any())
				{
					foreach (var produktDTO in ordreDTO.OrdreProdukter)
					{
						var produkt = await _context.Produkter.FindAsync(produktDTO.ProduktId);
						if (produkt == null) return BadRequest($"Produkt med ID {produktDTO.ProduktId} findes ikke.");
						if (produkt.LagerAntal < produktDTO.KøbsAntal) return BadRequest($"Ikke nok på lager for produkt: {produkt.ProduktNavn}.");

						produkt.LagerAntal -= produktDTO.KøbsAntal;
						_context.Produkter.Update(produkt);

						var ordreProdukt = new OrdreProdukt
						{
							ProduktId = produkt.ProduktId,
							OrdreId = ordre.OrdreId,
							Antal = produktDTO.KøbsAntal,
							Pris = produkt.Pris ?? 0
						};
						_context.OrdreProdukter.Add(ordreProdukt);
						ordre.TotalPris += (produkt.Pris ?? 0) * produktDTO.KøbsAntal;
					}
				}

				await _context.SaveChangesAsync();

				var betaling = new Betaling
				{
					OrdreId = ordre.OrdreId,
					Beløb = ordre.TotalPris,
					Betalt = false
				};
				_context.Betalinger.Add(betaling);
				await _context.SaveChangesAsync();

				return CreatedAtAction(nameof(GetById), new { id = ordre.OrdreId }, ordre);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Fejl under oprettelse af ordre: {ex.Message}");
			}
		}

		// API-endpoint til opdatering af ordrestatus
		[HttpPut("{ordreId}/status")]
        public async Task<IActionResult> UpdateOrdreStatus(int ordreId, [FromBody] OrdreStatus nyStatus)
        {
            try
            {
                await _ordreRepository.UpdateOrdreStatusAsync(ordreId, nyStatus);
                return Ok(new { message = "Ordrestatus opdateret med succes." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Der opstod en fejl under opdatering af ordrestatus.", error = ex.Message });
            }
        }
	}
}
