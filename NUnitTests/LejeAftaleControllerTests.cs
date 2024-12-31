using NUnit.Framework; // Importerer NUnit til testformål.
using Microsoft.AspNetCore.Mvc; // Bruges til at arbejde med ActionResult og controllers.
using Microsoft.EntityFrameworkCore; // Bruges til at konfigurere en in-memory database.
using ScooterLandProjectOpg.Server.Controllers; // Importerer den controller, der skal testes.
using ScooterLandProjectOpg.Server.Context; // Importerer konteksten for databasen.
using ScooterLandProjectOpg.Server.Services; // Importerer servicen til lejeaftaler.
using ScooterLandProjectOpg.Shared.Models; // Importerer modellerne til brug i tests.
using System.Linq; // Bruges til LINQ-forespørgsler.
using System.Threading.Tasks; // Understøtter asynkron programmering.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer grænseflader til repository-mønstre.

namespace ScooterLandProjectOpgNUnitTests // Navnerummet for tests.
{
    [TestFixture] // Marker klassen som en NUnit testklasse.
    public class LejeAftaleControllerTests // Testklasse for LejeAftaleController.
    {
        private LejeAftaleController _controller; // Controlleren der skal testes.
        private ScooterLandContext _context; // Databasekonteksten.

        [SetUp] // Kører før hver test.
        public void Setup()
        {
            // Opretter en in-memory database for test.
            var options = new DbContextOptionsBuilder<ScooterLandContext>()
                .UseInMemoryDatabase("TestDatabase") // Bruger en in-memory database.
                .Options;

            _context = new ScooterLandContext(options); // Initialiserer databasekonteksten.

            // Opretter LejeAftaleService med den in-memory database.
            var repository = new LejeAftaleService(_context); // Initialiserer servicen.
            _controller = new LejeAftaleController(repository); // Initialiserer controlleren.
        }

        [TearDown] // Kører efter hver test.
        public void TearDown()
        {
            // Rydder databasen mellem tests.
            _context.LejeAftaler.RemoveRange(_context.LejeAftaler);
            _context.SaveChanges();
        }

        [Test] // Marker denne metode som en test.
        public async Task AddLejeAftale_ShouldReturnCreatedResult()
        {
            // Arrange:
            // Forbereder en lejeaftale til testen.
            var lejeAftale = new LejeAftale
            {
                LejeId = 0, // Sætter en unik ID.
                StartDato = DateTime.Now, // Sætter startdatoen.
                SlutDato = DateTime.Now.AddDays(7), // Sætter slutdatoen.
                KundeId = 1, // Sætter kunde-ID.
                DagligLeje = 100, // Sætter daglig leje.
                ForsikringsPris = 50, // Sætter forsikringspris.
                KilometerPris = 0.53, // Sætter kilometerpris.
                Selvrisiko = 1000, // Sætter selvrisiko.
                KortKilometer = 100 // Sætter kort kilometer.
            };

            // Act:
            // Kalder controllerens metode til at tilføje lejeaftalen.
            ActionResult<LejeAftale> result = await _controller.Add(lejeAftale);

            // Assert:
            // Verificerer resultatet.
            var createdResult = result.Result as CreatedAtActionResult; // Henter det returnerede resultat.
            Assert.IsNotNull(createdResult); // Tjekker at resultatet ikke er null.
            Assert.AreEqual(201, createdResult?.StatusCode); // Tjekker at statuskoden er 201 (Created).

            var createdLejeAftale = createdResult?.Value as LejeAftale; // Henter den oprettede lejeaftale.
            Assert.IsNotNull(createdLejeAftale); // Tjekker at den oprettede lejeaftale ikke er null.
            Assert.AreEqual(lejeAftale.LejeId, createdLejeAftale?.LejeId); // Tjekker at LejeId matcher.
        }

        [Test] // Marker denne metode som en test.
        public async Task GetLejeAftaleById_ShouldReturnNotFound_WhenIdDoesNotExist()
        {
            // Act:
            // Kalder controllerens metode med en ikke-eksisterende ID.
            ActionResult<LejeAftale> result = await _controller.GetById(999);

            // Assert:
            // Verificerer at resultatet er NotFound.
            var notFoundResult = result.Result as NotFoundObjectResult; // Henter det returnerede resultat.
            Assert.IsNotNull(notFoundResult); // Tjekker at resultatet ikke er null.
            Assert.AreEqual(404, notFoundResult?.StatusCode); // Tjekker at statuskoden er 404 (Not Found).
        }
    }
}