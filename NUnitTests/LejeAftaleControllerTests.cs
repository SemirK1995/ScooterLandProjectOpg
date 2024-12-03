using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Server.Controllers;
using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Server.Services; // Brug det eksisterende repository
using ScooterLandProjectOpg.Shared.Models;
using System.Linq;
using System.Threading.Tasks;
using ScooterLandProjectOpg.Server.Interfaces;

namespace ScooterLandProjectOpgNUnitTests
{
    [TestFixture]
    public class LejeAftaleControllerTests
    {
        private LejeAftaleController _controller;
        private ScooterLandContext _context;

        [SetUp]
        public void Setup()
        {
            // Opretter en in-memory database
            var options = new DbContextOptionsBuilder<ScooterLandContext>()
                .UseInMemoryDatabase("TestDatabase") // Brug en in-memory database
                .Options;

            _context = new ScooterLandContext(options);

            // Opretter LejeAftaleRepository med den in-memory database
            var repository = new LejeAftaleService(_context); // Bruger det eksisterende repository
            _controller = new LejeAftaleController(repository); // Bruger controlleren som den er
        }

        [TearDown]
        public void TearDown()
        {
            // Rydder databasen mellem testene
            _context.LejeAftaler.RemoveRange(_context.LejeAftaler);
            _context.SaveChanges();
        }

        [Test]
        public async Task AddLejeAftale_ShouldReturnCreatedResult()
        {
            // Arrange
            var lejeAftale = new LejeAftale
            {
                LejeId = 0, // Brug en unik ID
                StartDato = DateTime.Now, // Sørg for at StartDato er sat
                SlutDato = DateTime.Now.AddDays(7), // Sørg for at SlutDato er sat
                KundeId = 1, // Sørg for at KundeId er sat
                DagligLeje = 100, // Sørg for at DagligLeje er sat
                ForsikringsPris = 50, // Sørg for at ForsikringsPris er sat
                KilometerPris = 0.53, // Sørg for at KilometerPris er sat
                Selvrisiko = 1000, // Sørg for at Selvrisiko er sat
                KortKilometer = 100, // Sørg for at KortKilometer er sat (valgfrit)
            };

            // Act
            ActionResult<LejeAftale> result = await _controller.Add(lejeAftale); // Returnerer ActionResult<LejeAftale>

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult); // Resultatet skal være ikke-null
            Assert.AreEqual(201, createdResult?.StatusCode); // Statuskoden skal være 201 (Created)

            var createdLejeAftale = createdResult?.Value as LejeAftale;
            Assert.IsNotNull(createdLejeAftale); // Tjek at LejeAftale ikke er null
            Assert.AreEqual(lejeAftale.LejeId, createdLejeAftale?.LejeId); // Verificer at LejeId er korrekt
        }

       

        [Test]
        public async Task GetLejeAftaleById_ShouldReturnNotFound_WhenIdDoesNotExist()
        {
            // Act
            ActionResult<LejeAftale> result = await _controller.GetById(999); // Kald med en ID, der ikke findes

            // Assert
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult); // Det skal ikke være null
            Assert.AreEqual(404, notFoundResult?.StatusCode); // Statuskoden skal være 404 (Not Found)
        }
    }





}
