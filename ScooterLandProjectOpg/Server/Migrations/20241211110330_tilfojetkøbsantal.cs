using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class tilfojetkøbsantal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Ændrer navnet på kolonnen "Antal" i "Produkter"-tabellen til "LagerAntal" for bedre at reflektere dens funktion som lagerbeholdning.
            migrationBuilder.RenameColumn(
                name: "Antal",
                table: "Produkter",
                newName: "LagerAntal");

            // Tilføjer en ny kolonne "KøbsAntal" til "Produkter"-tabellen. Denne kolonne holder styr på antallet af produkter, der købes.
            migrationBuilder.AddColumn<int>(
                name: "KøbsAntal",
                table: "Produkter",
                type: "int",
                nullable: false,
                defaultValue: 0); // Standardværdien for denne kolonne er sat til 0.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Fjerner kolonnen "KøbsAntal" fra "Produkter"-tabellen for at rulle ændringen tilbage.
            migrationBuilder.DropColumn(
                name: "KøbsAntal",
                table: "Produkter");

            // Ændrer navnet på kolonnen "LagerAntal" tilbage til det oprindelige navn "Antal" for at gendanne den tidligere tilstand.
            migrationBuilder.RenameColumn(
                name: "LagerAntal",
                table: "Produkter",
                newName: "Antal");
        }
    }
}


// Denne migration opdaterer "Produkter"-tabellen ved at omdøbe kolonnen "Antal" til "LagerAntal" for at afspejle dens rolle i lagerstyring.
// Desuden tilføjes en ny kolonne "KøbsAntal", der repræsenterer antallet af produkter, der købes.
// Hvis migrationen rulles tilbage, fjernes "KøbsAntal"-kolonnen, og kolonnenavnet ændres tilbage fra "LagerAntal" til "Antal".
// Dette sikrer fleksibilitet og kontrol over ændringerne i databasen.