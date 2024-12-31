using Microsoft.EntityFrameworkCore; // Importerer Entity Framework Core til databaser.
using ScooterLandProjectOpg.Shared.Models; // Importerer modeller til brug i testene.
using ScooterLandProjectOpg.Server.Context; // Importerer databasekonteksten.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer interfaces til services.
using ScooterLandProjectOpg.Server.Services; // Importerer implementationer af services.

namespace ScooterLandProjectOpgNUnitTests.NUnitTests // Namespace til testklassen.
{
    [TestFixture] // Marker klassen som en NUnit testklasse.
    public class KundeServiceTests // Testklasse for KundeService.
    {
        private ScooterLandContext _context; // Databasekonteksten for tests.
        private KundeService _repository; // KundeService til test.

        [SetUp] // Kører før hver test for at initialisere miljøet.
        public void Setup()
        {
            // Opretter en in-memory database til testformål.
            var options = new DbContextOptionsBuilder<ScooterLandContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase") // Navn på testdatabasen.
                .Options;

            _context = new ScooterLandContext(options); // Initialiserer databasekonteksten.
            _context.Database.EnsureDeleted(); // Sletter databasen, hvis den findes.
            _context.Database.EnsureCreated(); // Opretter en ny database.

            _repository = new KundeService(_context); // Initialiserer KundeService.
        }

        [Test] // Marker denne metode som en test.
        public async Task GetAllWithOrdersAsync_ShouldReturnKunderWithOrders()
        {
            // Arrange: Tilføjer en kunde med ordrer til databasen.
            var kunde = new Kunde
            {
                KundeId = 1,
                Navn = "Test Kunde", // Kundens navn.
                Adresse = "Testvej 123", // Kundens adresse.
                Email = "test@test.dk", // Kundens e-mail.
                Telefonnummer = 12345678, // Kundens telefonnummer.
                Ordre = new List<Ordre> // Tilføjer to ordrer til kunden.
                {
                    new Ordre { OrdreId = 1, Dato = DateTime.Now, TotalPris = 100.0 },
                    new Ordre { OrdreId = 2, Dato = DateTime.Now, TotalPris = 150.0 }
                }
            };

            _context.Kunder.Add(kunde); // Tilføjer kunden til databasen.
            await _context.SaveChangesAsync(); // Gemmer ændringerne.

            // Act:
            // Henter alle kunder med deres ordrer.
            var result = await _repository.GetAllWithOrdersAsync();

            // Assert:
            // Verificerer resultatet.
            Assert.That(result.Count(), Is.EqualTo(1)); // Der skal være én kunde.
            Assert.That(result.First().Ordre.Count, Is.EqualTo(2)); // Kunden har to ordrer.
        }

        [Test] // Marker denne metode som en test.
        public async Task GetKundeWithScootersAsync_ShouldReturnKundeWithScooters()
        {
            // Arrange:
            // Tilføjer en kunde med scootere til databasen.
            var kunde = new Kunde
            {
                KundeId = 1,
                Navn = "Test Kunde", // Kundens navn.
                Adresse = "Testvej 123", // Kundens adresse.
                Email = "test@test.dk", // Kundens e-mail.
                Telefonnummer = 12345678, // Kundens telefonnummer.
                KundeScooter = new List<KundeScooter> // Tilføjer to scootere til kunden.
                {
                    new KundeScooter { ScooterId = 1, Maerke = "Scooter 1", Model = "Model A" },
                    new KundeScooter { ScooterId = 2, Maerke = "Scooter 2", Model = "Model B" }
                }
            };

            _context.Kunder.Add(kunde); // Tilføjer kunden til databasen.
            await _context.SaveChangesAsync(); // Gemmer ændringerne.

            // Act:
            // Henter kunden med scootere baseret på ID.
            var result = await _repository.GetKundeWithScootersAsync(1);

            // Assert:
            // Verificerer resultatet.
            Assert.That(result, Is.Not.Null); // Kunden skal findes.
            Assert.That(result.KundeScooter.Count, Is.EqualTo(2)); // Kunden skal have to scootere.
            Assert.That(result.KundeScooter[0].Maerke, Is.EqualTo("Scooter 1")); // Verificerer mærke for første scooter.
            Assert.That(result.KundeScooter[0].Model, Is.EqualTo("Model A")); // Verificerer model for første scooter.
            Assert.That(result.KundeScooter[1].Maerke, Is.EqualTo("Scooter 2")); // Verificerer mærke for anden scooter.
            Assert.That(result.KundeScooter[1].Model, Is.EqualTo("Model B")); // Verificerer model for anden scooter.
        }
    }
}