using Microsoft.EntityFrameworkCore; // Importerer Entity Framework Core til databaseinteraktioner.
using Microsoft.EntityFrameworkCore.Metadata.Internal; // Importerer interne metadata for EF Core.
using PdfSharpCore.Drawing; // Importerer tegneværktøjer til at generere PDF'er.
using PdfSharpCore.Pdf; // Importerer PDF-funktionalitet fra PdfSharpCore.
using ScooterLandProjectOpg.Server.Context; // Importerer ScooterLand datakontext til databasen.
using ScooterLandProjectOpg.Shared.Models; // Importerer delte modeller fra projektet.
using SixLabors.Fonts; // Importerer SixLabors til fontstyring.
using System.Reflection.Metadata; // Importerer metadatahåndtering.

namespace ScooterLandProjectOpg.Server.PDFServices // Definerer namespace for PDF-relaterede tjenester, der bruges i projektet.
{
    // En serviceklasse til generering af fakturaer i PDF-format.
    public class FakturaService
    {
        private readonly ScooterLandContext _context; // Kontekst, der giver adgang til databasen via EF Core.

        public FakturaService(ScooterLandContext context) // Constructor, der initialiserer databaskonteksten for denne service.
        {
            _context = context;
        }

        public async Task<byte[]> GenererFakturaPdfAsync(int betalingsId) // Metode til at generere faktura som PDF baseret på betalingsID.
        {
            // Henter betaling med relaterede data som ordre, kunde, produkter og ydelser.
            var betaling = await _context.Betalinger
                .Include(b => b.Ordre) // Inkluderer ordren relateret til betalingen.
                    .ThenInclude(o => o.LejeAftale) // Inkluderer lejeaftalen relateret til ordren.
                        .ThenInclude(la => la.LejeScooter) // Inkluderer scootere relateret til lejeaftalen.
                .Include(b => b.Ordre)
                    .ThenInclude(o => o.OrdreYdelse) // Inkluderer ydelser relateret til ordren.
                        .ThenInclude(oy => oy.Scooter) // Inkluderer scooteren relateret til ydelsen.
                .Include(b => b.Ordre)
                    .ThenInclude(o => o.OrdreYdelse)
                        .ThenInclude(oy => oy.Ydelse) // Inkluderer ydelsesdetaljer.
                .Include(b => b.Ordre)
                    .ThenInclude(o => o.OrdreYdelse)
                        .ThenInclude(oy => oy.Mekaniker) // Inkluderer mekanikeren relateret til ydelsen.
                .Include(b => b.Ordre)
                    .ThenInclude(o => o.Kunde) // Inkluderer kunden relateret til ordren.
                .Include(b => b.Ordre)
                    .ThenInclude(o => o.OrdreProdukter) // Inkluderer produkter relateret til ordren.
                        .ThenInclude(op => op.Produkt) // Inkluderer produktdetaljer.
                .FirstOrDefaultAsync(b => b.BetalingsId == betalingsId); // Finder den første betaling med det angivne betalingsID.

            // Kontrollerer, om betalingen eksisterer.
            if (betaling == null)
            {
                // Kaster undtagelse, hvis betalingen ikke findes.
                throw new KeyNotFoundException($"Betaling med ID {betalingsId} blev ikke fundet.");
            }
             

            // Henter ordren fra betalingen.
            var ordre = betaling.Ordre;
            // Henter kunden relateret til ordren.
            var kunde = ordre.Kunde;

            // Opretter en MemoryStream til PDF-data.
            using (var ms = new MemoryStream())
            {
                PdfDocument pdf = new PdfDocument(); // Opretter en ny PDF-dokument.
                PdfPage page = pdf.AddPage(); // Tilføjer en side til dokumentet.
                XGraphics gfx = XGraphics.FromPdfPage(page); // Opretter en tegnekontekst til siden.

                // Definerer skrifttyper og farver, der skal bruges i PDF'en.
                var titleFont = new XFont("Arial", 16, XFontStyle.Bold);
                var headerFont = new XFont("Arial", 12, XFontStyle.Bold);
                var normalFont = new XFont("Arial", 10);
                var smallFont = new XFont("Arial", 8);
                var grayBrush = new XSolidBrush(XColor.FromArgb(150, 150, 150));
                var darkBrush = XBrushes.Black;
                var lightGrayBrush = new XSolidBrush(XColor.FromArgb(240, 240, 240));

                int yPoint = 40; // Startpunkt for y-koordinaten, hvor tekst begynder på siden.

                // Tegner fakturaens header.
                // Skriver overskriften "Scooterland Faktura" med stor skrift.
                gfx.DrawString("Scooterland Faktura", titleFont, darkBrush, new XRect(40, yPoint, page.Width - 80, 0), XStringFormats.TopLeft);
                // Tilføjer datoen for fakturagenerering i øverste højre hjørne.
                gfx.DrawString($"Dato: {DateTime.Now:dd-MM-yyyy}", normalFont, darkBrush, new XRect(0, yPoint, page.Width - 40, 0), XStringFormats.TopRight);
                // Flytter y-koordinaten ned for næste sektion.
                yPoint += 30;


                // Tegner en linje som separator.
                gfx.DrawLine(XPens.LightGray, 40, yPoint, page.Width - 40, yPoint);
                // Flytter y-koordinaten ned for næste sektion.
                yPoint += 10;


                // Tegner betalingsdetaljer.
                // Skriver overskriften "Betaling Detaljer" på PDF'en. Teksten er formateret med en header-skrifttype og sort farve.
                // Den placeres øverst til venstre på siden, med en XRect, der definerer placeringen og bredden af tekstområdet.
                gfx.DrawString("Betaling Detaljer", headerFont, darkBrush, new XRect(40, yPoint, page.Width - 80, 0), XStringFormats.TopLeft);
                // Flytter y-koordinaten 30 enheder ned for at gøre plads til yderligere indhold nedenfor.
                yPoint += 30;
                // Skriver betalings-ID'et, som kommer fra betalingens data, med en normal skrifttype og sort farve.
                // Den placeres på x-position 40 og den aktuelle y-position.
                gfx.DrawString($"Betalings ID: {betaling.BetalingsId}", normalFont, darkBrush, 40, yPoint);
                // Flytter y-koordinaten 15 enheder ned for at tilføje næste tekstlinje.
                yPoint += 15;
                // Skriver betalingsmetoden fra betalingen. Teksten er også placeret i venstre kolonne, 40 enheder fra venstre kant.
                gfx.DrawString($"Betalingsmetode: {betaling.BetalingsMetode}", normalFont, darkBrush, 40, yPoint);
                // Flytter y-koordinaten yderligere 15 enheder ned.
                yPoint += 15;
                // Skriver betalingsdatoen i formatet dag-måned-år. Datoen hentes fra betalingens data og vises på samme måde som de tidligere linjer.
                gfx.DrawString($"Betalingsdato: {betaling.BetalingsDato:dd-MM-yyyy}", normalFont, darkBrush, 40, yPoint);
                // Tilføjer et større mellemrum på 30 enheder til at adskille betalingssektionen fra den næste sektion.
                yPoint += 30;


                // Tegner kundeoplysninger
                // Skriver overskriften "Kundeoplysninger" på PDF'en. Teksten er formateret med en header-skrifttype og sort farve.
                // Den placeres øverst til venstre på siden, med en XRect, der definerer placeringen og bredden af tekstområdet.
                gfx.DrawString("Kundeoplysninger", headerFont, darkBrush, new XRect(40, yPoint, page.Width - 80, 0), XStringFormats.TopLeft);
                // Flytter y-koordinaten 30 enheder ned for at gøre plads til yderligere indhold nedenfor.
                yPoint += 30;
                // Skriver kunde-ID'et, som kommer fra kundens data, med en normal skrifttype og sort farve.
                // Den placeres på x-position 40 og den aktuelle y-position.
                gfx.DrawString($"KundeId: {kunde.KundeId}", normalFont, darkBrush, 40, yPoint);
                // Flytter y-koordinaten 15 enheder ned for at tilføje næste tekstlinje.
                yPoint += 15;
                // Skriver kundens navn, som hentes fra kundens data. Teksten vises også i venstre kolonne, 40 enheder fra venstre kant.
                gfx.DrawString($"Kunde Navn: {kunde.Navn}", normalFont, darkBrush, 40, yPoint);
                // Flytter y-koordinaten yderligere 15 enheder ned.
                yPoint += 15;
                // Skriver kundens adresse fra dataene. Den placeres med samme formatering og placering som tidligere linjer.
                gfx.DrawString($"Adresse: {kunde.Adresse}", normalFont, darkBrush, 40, yPoint);
                // Flytter y-koordinaten yderligere 15 enheder ned.
                yPoint += 15;
                // Skriver kundens telefonnummer fra dataene, med samme placering og formatering som tidligere linjer.
                gfx.DrawString($"Telefon: {kunde.Telefonnummer}", normalFont, darkBrush, 40, yPoint);
                // Tilføjer et større mellemrum på 30 enheder for at adskille kundeoplysninger fra næste sektion.
                yPoint += 30;

                // Tilføjer en vandret, lys grå linje for at visuelt adskille sektionerne på PDF'en.
                // Linjen starter 40 enheder fra venstre kant og strækker sig til næsten hele sidens bredde.
                gfx.DrawLine(XPens.LightGray, 40, yPoint, page.Width - 40, yPoint);
                // Flytter y-koordinaten yderligere 20 enheder ned for at skabe afstand til næste sektion.
                yPoint += 20;


                // Tegner ordre detaljer
                // Skriver overskriften "Ordre Detaljer" på PDF'en. Teksten er formateret med en header-skrifttype og sort farve.
                // Den placeres øverst til venstre på siden, med en XRect, der definerer placeringen og bredden af tekstområdet.
                gfx.DrawString("Ordre Detaljer", headerFont, darkBrush, new XRect(40, yPoint, page.Width - 80, 0), XStringFormats.TopLeft);
                // Flytter y-koordinaten 30 enheder ned for at gøre plads til yderligere indhold nedenfor.
                yPoint += 30;
                // Skriver ordre-ID'et, som hentes fra ordredatabasen. Teksten vises i venstre kolonne, 40 enheder fra venstre kant.
                gfx.DrawString($"Ordre ID: {ordre.OrdreId}", normalFont, darkBrush, 40, yPoint);
                // Flytter y-koordinaten yderligere 15 enheder ned.
                yPoint += 15;
                // Skriver ordredatoen i formatet dag-måned-år. Datoen er hentet fra ordredatabasen og vises på samme måde som tidligere linjer.
                gfx.DrawString($"Ordre Dato: {ordre.Dato?.ToString("dd-MM-yyyy")}", normalFont, darkBrush, 40, yPoint);
                // Tilføjer et større mellemrum på 30 enheder for at adskille ordredetaljerne fra næste sektion.
                yPoint += 30;


                // Tegner lejeaftale
                // Kontrollerer om ordren har en tilknyttet lejeaftale. Hvis lejeaftalen ikke er null, skrives detaljerne til PDF'en.
                if (ordre.LejeAftale != null)
                {
                    // Skriver overskriften "Lejeaftale Detaljer" på PDF'en. Teksten er formateret med en header-skrifttype og sort farve.
                    // Den placeres øverst til venstre på siden, med en XRect, der definerer placeringen og bredden af tekstområdet.
                    gfx.DrawString("Lejeaftale Detaljer", headerFont, darkBrush, new XRect(40, yPoint, page.Width - 80, 0), XStringFormats.TopLeft);
                    // Flytter y-koordinaten 30 enheder ned for at gøre plads til yderligere indhold nedenfor.
                    yPoint += 30;
                    // Skriver lejeaftalens ID, som hentes fra lejeaftaledataene. Teksten vises i venstre kolonne, 40 enheder fra venstre kant.
                    gfx.DrawString($"LejeId: {ordre.LejeAftale.LejeId}", normalFont, darkBrush, 40, yPoint);
                    // Flytter y-koordinaten yderligere 15 enheder ned.
                    yPoint += 15;
                    // Skriver lejeaftalens startdato i formatet dag-måned-år. Startdatoen hentes fra lejeaftaledataene.
                    gfx.DrawString($"Startdato: {ordre.LejeAftale.StartDato:dd-MM-yyyy}", normalFont, darkBrush, 40, yPoint);
                    // Flytter y-koordinaten yderligere 15 enheder ned.
                    yPoint += 15;
                    // Skriver lejeaftalens slutdato i formatet dag-måned-år. Slutdatoen hentes fra lejeaftaledataene.
                    gfx.DrawString($"Slutdato: {ordre.LejeAftale.SlutDato:dd-MM-yyyy}", normalFont, darkBrush, 40, yPoint);
                    // Flytter y-koordinaten yderligere 15 enheder ned.
                    yPoint += 15;
                    // Skriver prisen for daglig leje af en scooter, som hentes fra lejeaftaledataene. Beløbet vises i kroner.
                    gfx.DrawString($"Daglig leje for scooter: {ordre.LejeAftale.DagligLeje} kr.", normalFont, darkBrush, 40, yPoint);
                    // Flytter y-koordinaten yderligere 15 enheder ned.
                    yPoint += 15;
                    // Skriver prisen per kilometer, som hentes fra lejeaftaledataene. Beløbet vises i kroner.
                    gfx.DrawString($"Kilometer Pris: {ordre.LejeAftale.KilometerPris} kr.", normalFont, darkBrush, 40, yPoint);
                    // Flytter y-koordinaten yderligere 15 enheder ned.
                    yPoint += 15;
                    // Skriver antallet af kørte kilometer, som hentes fra lejeaftaledataene.
                    gfx.DrawString($"Antal kørte kilometer: {ordre.LejeAftale.KortKilometer}", normalFont, darkBrush, 40, yPoint);
                    // Flytter y-koordinaten yderligere 15 enheder ned.
                    yPoint += 15;
                    // Skriver selvrisikobeløbet for lejeaftalen, som hentes fra lejeaftaledataene. Beløbet vises i kroner.
                    gfx.DrawString($"Selvrisiko: {ordre.LejeAftale.Selvrisiko} kr.", normalFont, darkBrush, 40, yPoint);
                    // Tilføjer et større mellemrum på 30 enheder for at adskille lejeaftaledetaljerne fra næste sektion.
                    yPoint += 30;
                }

                // Tegner ydelser
                // Kontrollerer om ordren indeholder nogen produkter. Hvis listen over ordreprodukter ikke er tom, skrives detaljerne til PDF'en.
                if (ordre.OrdreProdukter?.Any() == true)
                {
                    // Skriver overskriften "Produkter" på PDF'en. Teksten er formateret med en header-skrifttype og sort farve.
                    // Den placeres øverst til venstre på siden, med en XRect, der definerer placeringen og bredden af tekstområdet.
                    gfx.DrawString("Produkter", headerFont, darkBrush, new XRect(40, yPoint, page.Width - 80, 0), XStringFormats.TopLeft);
                    // Flytter y-koordinaten 30 enheder ned for at gøre plads til produktlisten.
                    yPoint += 30;
                    // Itererer gennem hver produktpost i ordreprodukterne for at skrive produktdetaljerne til PDF'en.
                    foreach (var produkt in ordre.OrdreProdukter)
                    {
                        // Skriver produkt-ID'et, produktnavnet og prisen for det aktuelle produkt. Prisen formateres med to decimaler og vises i kroner.
                        // Teksten placeres 40 enheder fra venstre kant og på den aktuelle y-position.
                        gfx.DrawString($"- Produkt ID {produkt.ProduktId}: {produkt.Produkt.ProduktNavn}, Pris: {produkt.Pris.ToString("F2")} kr.", normalFont, darkBrush, 40, yPoint);
                        // Flytter y-koordinaten 15 enheder ned for at gøre plads til næste produkt.
                        yPoint += 15;
                    }
                    // Tilføjer et mindre mellemrum på 10 enheder for at adskille produktsektionen fra eventuel efterfølgende tekst.
                    yPoint += 10;
                }


                // Tegner produkter
                // Kontrollerer om ordren indeholder nogen ydelser. Hvis listen over ordreydelser ikke er tom, skrives detaljerne til PDF'en.
                if (ordre.OrdreYdelse?.Any() == true)
                {
                    // Skriver overskriften "Ydelser" på PDF'en. Teksten er formateret med en header-skrifttype og sort farve.
                    // Den placeres øverst til venstre på siden, med en XRect, der definerer placeringen og bredden af tekstområdet.
                    gfx.DrawString("Ydelser", headerFont, darkBrush, new XRect(40, yPoint, page.Width - 80, 0), XStringFormats.TopLeft);
                    // Flytter y-koordinaten 30 enheder ned for at gøre plads til efterfølgende indhold.
                    yPoint += 30;

                    // Tegner en separatorlinje under overskriften for at adskille sektionen visuelt.
                    gfx.DrawLine(XPens.LightGray, 40, yPoint, page.Width - 40, yPoint);
                    // Flytter y-koordinaten yderligere 20 enheder ned for at skabe afstand til den næste sektion.
                    yPoint += 20;

                    // Gemmer PDF-dokumentet til en MemoryStream for at forberede det til returnering som en bytearray.
                    pdf.Save(ms);

                    // Returnerer PDF-dokumentet som en bytearray, som kan sendes tilbage til klienten eller bruges videre.
                    return ms.ToArray();
                }
                else
                {
                    // Hvis listen over ordreydelser er tom, kaster koden en InvalidDataException.
                    // Dette angiver, at ordren ikke indeholder nogen ydelser, hvilket er en fejltilstand.
                    throw new InvalidDataException("Ordren har ikke nogle ydelser. Kaster exception.");
                }
            }
        }
    }
}