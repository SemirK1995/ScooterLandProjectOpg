using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Shared.Models;
using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Server.Services;

namespace ScooterLandProjectOpgNUnitTests.NUnitTests
{
    [TestFixture]
    public class KundeServiceTests
    {
        private ScooterLandContext _context;
        private KundeService _repository;

        [SetUp]
        public void Setup()
        {
            // Brug en in-memory database til tests
            var options = new DbContextOptionsBuilder<ScooterLandContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ScooterLandContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _repository = new KundeService(_context);
        }


        [Test]
        public async Task GetAllWithOrdersAsync_ShouldReturnKunderWithOrders()
        {
            // Arrange
            var kunde = new Kunde
            {
                KundeId = 1,
                Navn = "Test Kunde",
                Adresse = "Testvej 123",
                Email = "test@test.dk",
                Telefonnummer = 12345678, // Tilføj et telefonnummer
                Ordre = new List<Ordre>
        {
            new Ordre { OrdreId = 1, Dato = DateTime.Now, TotalPris = 100.0 },
            new Ordre { OrdreId = 2, Dato = DateTime.Now, TotalPris = 150.0 }
        }
            };

            _context.Kunder.Add(kunde);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllWithOrdersAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1)); // Der skal være én kunde
            Assert.That(result.First().Ordre.Count, Is.EqualTo(2)); // Kunden har to ordrer
        }



        [Test]
        public async Task GetKundeWithScootersAsync_ShouldReturnKundeWithScooters()
        {
            // Arrange
            var kunde = new Kunde
            {
                KundeId = 1,
                Navn = "Test Kunde",
                Adresse = "Testvej 123",
                Email = "test@test.dk",
                Telefonnummer = 12345678,
                KundeScooter = new List<KundeScooter>
        {
            new KundeScooter { ScooterId = 1, Maerke = "Scooter 1", Model = "Model A" },
            new KundeScooter { ScooterId = 2, Maerke = "Scooter 2", Model = "Model B" }
        }
            };

            _context.Kunder.Add(kunde);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetKundeWithScootersAsync(1);

            // Assert
            Assert.That(result, Is.Not.Null); // Kunden skal findes
            Assert.That(result.KundeScooter.Count, Is.EqualTo(2)); // Kunden skal have to scootere
            Assert.That(result.KundeScooter[0].Maerke, Is.EqualTo("Scooter 1")); // Første scooter-mærke
            Assert.That(result.KundeScooter[0].Model, Is.EqualTo("Model A")); // Første scooter-model
            Assert.That(result.KundeScooter[1].Maerke, Is.EqualTo("Scooter 2")); // Anden scooter-mærke
            Assert.That(result.KundeScooter[1].Model, Is.EqualTo("Model B")); // Anden scooter-model
        }

    }
}
