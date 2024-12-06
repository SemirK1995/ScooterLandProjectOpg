using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Server.PDFServices;
using ScooterLandProjectOpg.Shared.DTO;
using ScooterLandProjectOpg.Shared.Enum;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetalingsController : ControllerBase
    {
        private readonly IBetalingRepository _betalingRepository;
        private readonly FakturaService _fakturaService;

        public BetalingsController(IBetalingRepository betalingRepository, FakturaService fakturaService)
        {
            _betalingRepository = betalingRepository;
            _fakturaService = fakturaService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Betaling>>> GetAll()
        {
            var betalinger = await _betalingRepository.GetAllAsync();
            return Ok(betalinger);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Betaling>>> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Søgeforespørgslen er tom.");

            var betalinger = await _betalingRepository.SearchByQueryAsync(query);

            if (!betalinger.Any())
                return NotFound("Ingen betalinger fundet.");

            return Ok(betalinger);
        }

        //[HttpPut("{betalingsId}/status")]
        //public async Task<IActionResult> UpdateStatus(int betalingsId, [FromBody] bool betaltStatus)
        //{
        //    try
        //    {
        //        await _betalingRepository.UpdateBetalingsStatusAsync(betalingsId, betaltStatus);
        //        return Ok("Betalingsstatus opdateret.");
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}
        [HttpPut("{betalingsId}/status")]
        public async Task<IActionResult> UpdateStatus(int betalingsId, [FromBody] BetalingUpdateDto betalingUpdate)
        {
            try
            {
                await _betalingRepository.UpdateBetalingsStatusAsync(betalingsId, betalingUpdate);
                return Ok("Betalingsstatus og dato opdateret.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Fejl ved opdatering af betaling: {ex.Message}");
            }
        }


        [HttpPut("{betalingsId}/metode")]
        public async Task<IActionResult> UpdateBetalingsMetode(int betalingsId, [FromBody] BetalingsMetodeStatus nyMetode)
        {
            try
            {
                await _betalingRepository.UpdateBetalingsMetodeAsync(betalingsId, nyMetode);
                return Ok("Betalingsmetode opdateret.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{betalingsId}/faktura")]
        public async Task<ActionResult<FakturaDto>> GenererFaktura(int betalingsId)
        {
            try
            {
                var betaling = await _betalingRepository.GetFakturaDetaljerAsync(betalingsId);
                if (betaling == null)
                    return NotFound($"Betaling med ID {betalingsId} blev ikke fundet.");

                var ordre = betaling.Ordre;
                var kunde = ordre.Kunde;

                var fakturaDto = new FakturaDto
                {
                    // Kundeoplysninger
                    KundeId = kunde.KundeId,
                    KundeNavn = kunde.Navn,
                    KundeAdresse = kunde.Adresse,
                    KundeTelefon = kunde.Telefonnummer?.ToString(),
                    KundeEmail = kunde.Email,

                    // Betaling
                    BetalingsId = betaling.BetalingsId,
                    OrdreId = betaling.OrdreId,
                    Beløb = betaling.Beløb,
                    BetalingsMetode = betaling.BetalingsMetode?.ToString(),
                    BetalingsDato = betaling.BetalingsDato?.ToString("dd-MM-yyyy"),
                    Betalt = betaling.Betalt,

                    // Ordre
                    OrdreDato = ordre.Dato,
                    TotalPris = ordre.TotalPris,

                    // Ydelser
                    Ydelser = ordre.OrdreYdelse?.Select(oy => new FakturaYdelseDto
                    {
                        YdelseId = oy.YdelseId,
                        YdelseNavn = oy.Ydelse?.Navn,
                        BeregnetPris = oy.BeregnetPris,
                        ScooterMaerke = oy.Scooter?.Maerke, // Forudsat at relationen findes
                        ScooterModel = oy.Scooter?.Model    // Forudsat at relationen findes

                    }).ToList(),

                    // KundeScooter
                    KundeScooter = ordre.OrdreYdelse?.FirstOrDefault()?.Scooter?.Model ?? "Ingen scooter valgt",

                    // Lejeaftale (hvis relevant)
                    Lejeaftale = ordre.LejeAftale == null
                        ? null
                        : new FakturaLejeAftaleDto
                        {
                            StartDato = ordre.LejeAftale.StartDato,
                            SlutDato = ordre.LejeAftale.SlutDato,
                            ForsikringsPris = ordre.LejeAftale.ForsikringsPris,
                            KortKilometer = ordre.LejeAftale.KortKilometer ?? 0,
                            DagligLeje = ordre.LejeAftale.DagligLeje,
                            Selvrisiko = ordre.LejeAftale?.Selvrisiko ?? 0,
                            Scootere = ordre.LejeAftale.LejeScooter?.Select(ls => $"{ls.ScooterMaerke} {ls.ScooterModel}").ToList() ?? new List<string>()
                        },
                    // Produkter
                    Produkter = ordre.OrdreProdukter?.Select(op => new FakturaProduktDto
                    {
                        ProduktId = op.Produkt.ProduktId,
                        ProduktNavn = op.Produkt.ProduktNavn,
                        Antal = op.Antal,
                        Pris = op.Pris
                    }).ToList()
                };

                return Ok(fakturaDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Fejl ved generering af faktura: {ex.Message}");
            }
        }

        [HttpGet("{betalingsId}/download")]
        public async Task<IActionResult> DownloadFaktura(int betalingsId)
        {
            try
            {
                var pdf = await _fakturaService.GenererFakturaPdfAsync(betalingsId);
                return File(pdf, "application/pdf", $"Faktura_{betalingsId}.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest($"Fejl ved generering af faktura: {ex.Message}");
            }
        }
        [HttpPost("opret-betalinger-til-eksisterende-ordrer")]
        public async Task<IActionResult> OpretBetalingerTilEksisterendeOrdrer()
        {
            try
            {
                var antalBetalingerOprettet = await _betalingRepository.OpretBetalingerTilEksisterendeOrdrerAsync();
                return Ok($"Der blev oprettet betalinger for {antalBetalingerOprettet} ordrer.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Fejl under oprettelse af betalinger: {ex.Message}");
            }
        }
        [HttpPut("{betalingsId}/dato")]
        public async Task<IActionResult> UpdateBetalingsDato(int betalingsId, [FromBody] DateTime? nyDato)
        {
            try
            {
                var betaling = await _betalingRepository.GetByIdAsync(betalingsId);
                if (betaling == null)
                {
                    return NotFound($"Betaling med ID {betalingsId} blev ikke fundet.");
                }

                betaling.BetalingsDato = nyDato;
                await _betalingRepository.UpdateAsync(betaling);

                return Ok("Betalingsdato opdateret.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Fejl ved opdatering af betalingsdato: {ex.Message}");
            }
        }
    }
}
