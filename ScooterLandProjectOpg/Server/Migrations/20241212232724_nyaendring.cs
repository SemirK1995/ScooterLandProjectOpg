using Microsoft.EntityFrameworkCore.Migrations;

// Angiver namespace for migrations i ScooterLand-projektet og sikrer kompatibilitet med databaseændringer.
#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class nyaendring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Tilføjer en ny nullable kolonne "OrdreId" af typen int til tabellen "LejeAftaler".
            migrationBuilder.AddColumn<int>(
                name: "OrdreId",
                table: "LejeAftaler",
                type: "int",
                nullable: true);

            // Opretter en indeks på den nye kolonne "OrdreId" i "LejeAftaler"-tabellen for at optimere opslag.
            migrationBuilder.CreateIndex(
                name: "IX_LejeAftaler_OrdreId",
                table: "LejeAftaler",
                column: "OrdreId");

            // Tilføjer en fremmednøgle til "LejeAftaler"-tabellen, der refererer til "Ordrer"-tabellen med "OrdreId" som nøglen.
            migrationBuilder.AddForeignKey(
                name: "FK_LejeAftaler_Ordrer_OrdreId",
                table: "LejeAftaler",
                column: "OrdreId",
                principalTable: "Ordrer",
                principalColumn: "OrdreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Fjerner den fremmednøgle, der refererer til "Ordrer"-tabellen, hvis migrationen rulles tilbage.
            migrationBuilder.DropForeignKey(
                name: "FK_LejeAftaler_Ordrer_OrdreId",
                table: "LejeAftaler");

            // Fjerner det indeks, der blev oprettet på kolonnen "OrdreId", hvis migrationen rulles tilbage.
            migrationBuilder.DropIndex(
                name: "IX_LejeAftaler_OrdreId",
                table: "LejeAftaler");

            // Fjerner kolonnen "OrdreId" fra "LejeAftaler"-tabellen, hvis migrationen rulles tilbage.
            migrationBuilder.DropColumn(
                name: "OrdreId",
                table: "LejeAftaler");
        }
    }
}


// Denne migration introducerer en ny kolonne OrdreId i tabellen LejeAftaler, som kan bruges til at oprette en relation mellem en lejeaftale og en ordre.
// I Up-metoden tilføjes kolonnen, der indeholder fremmednøglen, og der oprettes en indeks for at forbedre ydelsen ved opslag.
// Fremmednøglen sikrer referentiel integritet mellem LejeAftaler og Ordrer.
// I Down-metoden fjernes fremmednøglen, indekset og kolonnen for at rulle ændringerne tilbage.