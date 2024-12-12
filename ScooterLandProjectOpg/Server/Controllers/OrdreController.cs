using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
					var lejeAftale = new LejeAftale
					{
						KundeId = ordreDTO.KundeId,
						StartDato = ordreDTO.LejeAftale.StartDato,
						SlutDato = ordreDTO.LejeAftale.SlutDato,
						DagligLeje = ordreDTO.LejeAftale.DagligLeje,
						ForsikringsPris = ordreDTO.LejeAftale.ForsikringsPris,
						KilometerPris = ordreDTO.LejeAftale.KilometerPris,
						KortKilometer = ordreDTO.LejeAftale.KortKilometer
					};
					_context.LejeAftaler.Add(lejeAftale);
					await _context.SaveChangesAsync();

					ordre.LejeId = lejeAftale.LejeId;
					ordre.TotalPris += lejeAftale.TotalPris;
				}

				_context.Ordrer.Add(ordre);
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

        //[HttpPut("{ordreId}/status")]
        //      public async Task<IActionResult> UpdateOrdreStatus(int ordreId, [FromBody] OrdreStatus nyStatus)
        //      {
        //	var ordre = await _ordreRepository.GetByIdAsync(ordreId);
        //	if (ordre == null)
        //	{
        //		return NotFound($"Ordre med ID {ordreId} blev ikke fundet.");
        //	}

        //	ordre.Status = nyStatus;
        //	await _ordreRepository.UpdateAsync(ordre);

        //	return NoContent();
        //}
        public async Task UpdateOrdreStatusAsync(int ordreId, OrdreStatus nyStatus)
        {
            // Hent ordren med relaterede data
            var ordre = await _context.Ordrer
                .Include(o => o.OrdreYdelse) // Inkluder arbejdsopgaver
                .Include(o => o.OrdreProdukter) // Inkluder ordreprodukter
                .Include(o => o.Betalinger) // Inkluder betalinger
                .Include(o => o.LejeAftale) // Inkluder lejeaftale
                .ThenInclude(la => la.LejeScooter) // Inkluder lejescootere
                .FirstOrDefaultAsync(o => o.OrdreId == ordreId);

            if (ordre == null)
                throw new KeyNotFoundException($"Ordre med ID {ordreId} blev ikke fundet.");

            // Opdater ordrestatus
            ordre.Status = nyStatus;

            // Fjern arbejdsopgaver for status Betalt eller Annulleret
            if (nyStatus == OrdreStatus.Betalt || nyStatus == OrdreStatus.Annulleret)
            {
                foreach (var ydelse in ordre.OrdreYdelse)
                {
                    ydelse.MekanikerId = null; // Fjern mekanikertildeling
                }
            }

            // Hvis status er Annulleret, fjern alle relaterede data
            if (nyStatus == OrdreStatus.Annulleret)
            {
                // Fjern betalinger
                if (ordre.Betalinger != null && ordre.Betalinger.Any())
                {
                    _context.Betalinger.RemoveRange(ordre.Betalinger);
                }

                // Fjern ordreprodukter
                if (ordre.OrdreProdukter != null && ordre.OrdreProdukter.Any())
                {
                    _context.OrdreProdukter.RemoveRange(ordre.OrdreProdukter);
                }

                // Fjern ordreydelser
                if (ordre.OrdreYdelse != null && ordre.OrdreYdelse.Any())
                {
                    _context.OrdreYdelser.RemoveRange(ordre.OrdreYdelse);
                }

                // Fjern lejeaftale og relaterede lejescootere
                if (ordre.LejeAftale != null)
                {
                    if (ordre.LejeAftale.LejeScooter != null && ordre.LejeAftale.LejeScooter.Any())
                    {
                        _context.LejeScootere.RemoveRange(ordre.LejeAftale.LejeScooter);
                    }
                    _context.LejeAftaler.Remove(ordre.LejeAftale);
                }

                // Fjern selve ordren
                _context.Ordrer.Remove(ordre);
            }
            else
            {
                // Hvis status ikke er Annulleret, opdater ordren
                _context.Ordrer.Update(ordre);
            }

            // Gem ændringer i databasen
            await _context.SaveChangesAsync();
        }


        [HttpPut("{ordreId}/selvrisiko")]
        public async Task<IActionResult> TilføjSelvrisiko(int ordreId)
        {
            try
            {
                await _ordreRepository.TilføjSelvrisikoAsync(ordreId);
                return Ok($"Selvrisiko tilføjet til ordre ID {ordreId}.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

		[HttpPut("{ordreId}/remove-selvrisiko")]
		public async Task<IActionResult> FjernSelvrisiko(int ordreId)
		{
			try
			{
				await _ordreRepository.FjernSelvrisikoAsync(ordreId); // Kald den forenklede service-metode
				return Ok($"Selvrisiko fjernet fra ordre ID {ordreId}.");
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message); // Returner en 404-fejl, hvis ordren ikke findes
			}
			catch (Exception ex)
			{
				return BadRequest($"En fejl opstod: {ex.Message}"); // Håndter andre fejl
			}
		}
	}
}
