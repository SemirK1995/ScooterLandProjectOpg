using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using ScooterLandProjectOpg.Server.Context;

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
                .Include(b => b.Ordre)
                    .ThenInclude(o => o.OrdreYdelse)
                    .ThenInclude(oy => oy.Ydelse)
                .Include(b => b.Ordre)
                    .ThenInclude(o => o.Kunde)
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
                XFont font = new XFont("Arial", 12);

                int yPoint = 40;

                // Faktura header
                gfx.DrawString("Faktura", new XFont("Arial", 18, XFontStyle.Bold), XBrushes.Black, new XRect(0, yPoint, page.Width, 0), XStringFormats.TopCenter);
                yPoint += 40;

                // Betaling detaljer
                gfx.DrawString($"Betalings ID: {betaling.BetalingsId}", font, XBrushes.Black, new XRect(40, yPoint, page.Width, 0), XStringFormats.TopLeft);
                yPoint += 20;
                gfx.DrawString($"Betalingsmetode: {betaling.BetalingsMetode}", font, XBrushes.Black, new XRect(40, yPoint, page.Width, 0), XStringFormats.TopLeft);
                yPoint += 20;
                gfx.DrawString($"Betalingsdato: {betaling.BetalingsDato?.ToString("dd-MM-yyyy")}", font, XBrushes.Black, new XRect(40, yPoint, page.Width, 0), XStringFormats.TopLeft);
                yPoint += 40;

                // Kundeoplysninger
                gfx.DrawString($"Kunde: {kunde.Navn}", font, XBrushes.Black, new XRect(40, yPoint, page.Width, 0), XStringFormats.TopLeft);
                yPoint += 20;
                gfx.DrawString($"Adresse: {kunde.Adresse}", font, XBrushes.Black, new XRect(40, yPoint, page.Width, 0), XStringFormats.TopLeft);
                yPoint += 20;
                gfx.DrawString($"Telefon: {kunde.Telefonnummer}", font, XBrushes.Black, new XRect(40, yPoint, page.Width, 0), XStringFormats.TopLeft);
                yPoint += 40;

                // Ordre detaljer
                gfx.DrawString($"Ordre ID: {ordre.OrdreId}", font, XBrushes.Black, new XRect(40, yPoint, page.Width, 0), XStringFormats.TopLeft);
                yPoint += 20;
                gfx.DrawString($"Dato: {ordre.Dato?.ToString("dd-MM-yyyy")}", font, XBrushes.Black, new XRect(40, yPoint, page.Width, 0), XStringFormats.TopLeft);
                yPoint += 20;
                gfx.DrawString($"Total Pris: {ordre.TotalPris?.ToString("F2")} kr.", font, XBrushes.Black, new XRect(40, yPoint, page.Width, 0), XStringFormats.TopLeft);
                yPoint += 40;

                // Lejeaftale detaljer (hvis nogen)
                if (ordre.LejeAftale != null)
                {
                    var lejeAftale = ordre.LejeAftale;
                    gfx.DrawString("Lejeaftale:", font, XBrushes.Black, new XRect(40, yPoint, page.Width, 0), XStringFormats.TopLeft);
                    yPoint += 20;
                    gfx.DrawString($"Startdato: {lejeAftale.StartDato?.ToString("dd-MM-yyyy")}", font, XBrushes.Black, new XRect(40, yPoint, page.Width, 0), XStringFormats.TopLeft);
                    yPoint += 20;
                    gfx.DrawString($"Slutdato: {lejeAftale.SlutDato?.ToString("dd-MM-yyyy")}", font, XBrushes.Black, new XRect(40, yPoint, page.Width, 0), XStringFormats.TopLeft);
                    yPoint += 40;
                }

                // Ydelser
                if (ordre.OrdreYdelse != null && ordre.OrdreYdelse.Any())
                {
                    gfx.DrawString("Ydelser:", font, XBrushes.Black, new XRect(40, yPoint, page.Width, 0), XStringFormats.TopLeft);
                    yPoint += 20;
                    foreach (var ydelse in ordre.OrdreYdelse)
                    {
                        gfx.DrawString($"- {ydelse.Ydelse?.Navn}: {ydelse.BeregnetPris.ToString("F2")} kr.", font, XBrushes.Black, new XRect(60, yPoint, page.Width, 0), XStringFormats.TopLeft);
                        yPoint += 20;
                    }
                }

                // Gem som PDF
                pdf.Save(ms);
                return ms.ToArray();
            }
        }


    }
}
