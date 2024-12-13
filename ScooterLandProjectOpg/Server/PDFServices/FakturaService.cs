using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Shared.Models;
using SixLabors.Fonts;
using System.Reflection.Metadata;

namespace ScooterLandProjectOpg.Server.PDFServices
{
	public class FakturaService
	{
		private readonly ScooterLandContext _context;

		public FakturaService(ScooterLandContext context)
		{
			_context = context;
		}

		public async Task<byte[]> GenererFakturaPdfAsync(int betalingsId)
		{
			var betaling = await _context.Betalinger
				 .Include(b => b.Ordre)
			.ThenInclude(o => o.LejeAftale)
				.ThenInclude(la => la.LejeScooter)
		.Include(b => b.Ordre)
			.ThenInclude(o => o.OrdreYdelse)
				.ThenInclude(oy => oy.Scooter)
		.Include(b => b.Ordre)
			.ThenInclude(o => o.OrdreYdelse)
				.ThenInclude(oy => oy.Ydelse)
		.Include(b => b.Ordre)
			.ThenInclude(o => o.OrdreYdelse)
				.ThenInclude(oy => oy.Mekaniker) // Tilføj denne linje for mekanikeroplysninger
		.Include(b => b.Ordre)
			.ThenInclude(o => o.Kunde)
		.Include(b => b.Ordre) // Tilføj produkterne
			.ThenInclude(o => o.OrdreProdukter)
				.ThenInclude(op => op.Produkt)
		.FirstOrDefaultAsync(b => b.BetalingsId == betalingsId);

			if (betaling == null)
				throw new KeyNotFoundException($"Betaling med ID {betalingsId} blev ikke fundet.");

			var ordre = betaling.Ordre;
			var kunde = ordre.Kunde;

			using (var ms = new MemoryStream())
			{
				PdfDocument pdf = new PdfDocument();
				PdfPage page = pdf.AddPage();
				XGraphics gfx = XGraphics.FromPdfPage(page);

				// Define fonts and colors
				var titleFont = new XFont("Arial", 16, XFontStyle.Bold);
				var headerFont = new XFont("Arial", 12, XFontStyle.Bold);
				var normalFont = new XFont("Arial", 10);
				var smallFont = new XFont("Arial", 8);
				var grayBrush = new XSolidBrush(XColor.FromArgb(150, 150, 150));
				var darkBrush = XBrushes.Black;
				var lightGrayBrush = new XSolidBrush(XColor.FromArgb(240, 240, 240));

				int yPoint = 40;

				// Draw Header Section
				gfx.DrawString("Scooterland Faktura", titleFont, darkBrush, new XRect(40, yPoint, page.Width - 80, 0), XStringFormats.TopLeft);
				gfx.DrawString($"Dato: {DateTime.Now:dd-MM-yyyy}", normalFont, darkBrush, new XRect(0, yPoint, page.Width - 40, 0), XStringFormats.TopRight);
				yPoint += 30;

				gfx.DrawLine(XPens.LightGray, 40, yPoint, page.Width - 40, yPoint);
				yPoint += 10;

				// Betaling detaljer
				gfx.DrawString("Betaling Detaljer", headerFont, darkBrush, new XRect(40, yPoint, page.Width - 80, 0), XStringFormats.TopLeft);
				yPoint += 30;
				gfx.DrawString($"Betalings ID: {betaling.BetalingsId}", normalFont, darkBrush, 40, yPoint);
				yPoint += 15;
				gfx.DrawString($"Betalingsmetode: {betaling.BetalingsMetode}", normalFont, darkBrush, 40, yPoint);
				yPoint += 15;
				gfx.DrawString($"Betalingsdato: {betaling.BetalingsDato:dd-MM-yyyy}", normalFont, darkBrush, 40, yPoint);
				yPoint += 30;

				// Kundeoplysninger
				gfx.DrawString("Kundeoplysninger", headerFont, darkBrush, new XRect(40, yPoint, page.Width - 80, 0), XStringFormats.TopLeft);
				yPoint += 30;
				gfx.DrawString($"KundeId: {kunde.KundeId}", normalFont, darkBrush, 40, yPoint);
				yPoint += 15;
				gfx.DrawString($"Kunde Navn: {kunde.Navn}", normalFont, darkBrush, 40, yPoint);
				yPoint += 15;
				gfx.DrawString($"Adresse: {kunde.Adresse}", normalFont, darkBrush, 40, yPoint);
				yPoint += 15;
				gfx.DrawString($"Telefon: {kunde.Telefonnummer}", normalFont, darkBrush, 40, yPoint);
				yPoint += 30;

				gfx.DrawLine(XPens.LightGray, 40, yPoint, page.Width - 40, yPoint);
				yPoint += 20;

				// Ordre detaljer
				gfx.DrawString("Ordre Detaljer", headerFont, darkBrush, new XRect(40, yPoint, page.Width - 80, 0), XStringFormats.TopLeft);
				yPoint += 30;
				gfx.DrawString($"Ordre ID: {ordre.OrdreId}", normalFont, darkBrush, 40, yPoint);
				yPoint += 15;
				gfx.DrawString($"Ordre Dato: {ordre.Dato?.ToString("dd-MM-yyyy")}", normalFont, darkBrush, 40, yPoint);
				yPoint += 30;

				// Lejeaftale
				if (ordre.LejeAftale != null)
				{
					gfx.DrawString("Lejeaftale Detaljer", headerFont, darkBrush, new XRect(40, yPoint, page.Width - 80, 0), XStringFormats.TopLeft);
					yPoint += 30;
					gfx.DrawString($"LejeId: {ordre.LejeAftale.LejeId}", normalFont, darkBrush, 40, yPoint);
					yPoint += 15;
					gfx.DrawString($"Startdato: {ordre.LejeAftale.StartDato:dd-MM-yyyy}", normalFont, darkBrush, 40, yPoint);
					yPoint += 15;
					gfx.DrawString($"Slutdato: {ordre.LejeAftale.SlutDato:dd-MM-yyyy}", normalFont, darkBrush, 40, yPoint);
					yPoint += 15;
					gfx.DrawString($"Daglig leje for scooter: {ordre.LejeAftale.DagligLeje} kr.", normalFont, darkBrush, 40, yPoint);
					yPoint += 15;
					gfx.DrawString($"Kilometer Pris: {ordre.LejeAftale.KilometerPris} kr.", normalFont, darkBrush, 40, yPoint);
					yPoint += 15;
					gfx.DrawString($"Antal kørte kilometer: {ordre.LejeAftale.KortKilometer}", normalFont, darkBrush, 40, yPoint);
					yPoint += 15;
					gfx.DrawString($"Selvrisiko: {ordre.LejeAftale.Selvrisiko} kr.", normalFont, darkBrush, 40, yPoint);
					yPoint += 30;
				}

				// Ydelser
				if (ordre.OrdreYdelse?.Any() == true)
				{
					gfx.DrawString("Ydelser", headerFont, darkBrush, new XRect(40, yPoint, page.Width - 80, 0), XStringFormats.TopLeft);
					yPoint += 30;
					foreach (var ydelse in ordre.OrdreYdelse)
					{
						gfx.DrawString($"- {ydelse.Ydelse?.Navn}: {ydelse.BeregnetPris.ToString("F2")} kr.", normalFont, darkBrush, 40, yPoint);
						yPoint += 15;
					}
					yPoint += 10;
				}


				// Produkter
				if (ordre.OrdreProdukter?.Any() == true)
				{
                    gfx.DrawString("Ydelser", headerFont, darkBrush, new XRect(40, yPoint, page.Width - 80, 0), XStringFormats.TopLeft);
                    yPoint += 30;

                    foreach (var ydelse in ordre.OrdreYdelse)
                    {
                        gfx.DrawString($"- {ydelse.Ydelse?.Navn}: {ydelse.BeregnetPris.ToString("F2")} kr.", normalFont, darkBrush, 40, yPoint);
                        yPoint += 15;

                        // Mekanikeroplysninger
                        if (ydelse.Mekaniker != null)
                        {
                            gfx.DrawString($"  Mekaniker: {ydelse.Mekaniker.Navn}", normalFont, XBrushes.Gray, 60, yPoint);
                            yPoint += 15;
                        }

                        // Timer arbejdet
                        if (ydelse.Timer.HasValue)
                        {
                            gfx.DrawString($"  Timer: {ydelse.Timer.Value:F2}", normalFont, XBrushes.Gray, 60, yPoint);
                            yPoint += 15;
                        }
                    }
                    yPoint += 10;
                }

				// Beregn totalpris
				double totalPris = ordre.TotalPris ?? 0;
				if (ordre.LejeAftale?.Selvrisiko > 0)
					totalPris += ordre.LejeAftale.Selvrisiko;

				yPoint += 30;
				gfx.DrawLine(XPens.LightGray, 40, yPoint, page.Width - 40, yPoint);
				yPoint += 20;

				gfx.DrawString($"Total Pris: {totalPris:F2} kr.", new XFont("Arial", 14, XFontStyle.Bold), darkBrush, new XRect(40, yPoint, page.Width - 80, 0), XStringFormats.TopLeft);

				// Save the PDF
				pdf.Save(ms);
				return ms.ToArray();
			}
		}


	}
}
