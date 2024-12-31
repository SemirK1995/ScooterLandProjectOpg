using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class DeleteKunde : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Fjerner den eksisterende fremmednøgle fra "OrdreProdukter" til "Ordrer", hvor sletningsadfærden ikke tidligere var defineret som Cascade.
            migrationBuilder.DropForeignKey(
                name: "FK_OrdreProdukter_Ordrer_OrdreId",
                table: "OrdreProdukter");

            // Tilføjer en ny fremmednøgle fra "OrdreProdukter" til "Ordrer", hvor sletningsadfærden nu er defineret som Cascade.
            migrationBuilder.AddForeignKey(
                name: "FK_OrdreProdukter_Ordrer_OrdreId",
                table: "OrdreProdukter",
                column: "OrdreId",
                principalTable: "Ordrer",
                principalColumn: "OrdreId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Fjerner fremmednøglen, der blev oprettet i Up-metoden, hvor sletningsadfærden er Cascade.
            migrationBuilder.DropForeignKey(
                name: "FK_OrdreProdukter_Ordrer_OrdreId",
                table: "OrdreProdukter");

            // Genskaber den oprindelige fremmednøgle fra "OrdreProdukter" til "Ordrer", hvor sletningsadfærden ikke er Cascade.
            migrationBuilder.AddForeignKey(
                name: "FK_OrdreProdukter_Ordrer_OrdreId",
                table: "OrdreProdukter",
                column: "OrdreId",
                principalTable: "Ordrer",
                principalColumn: "OrdreId");
        }
    }
}


// Denne migration ændrer sletningsadfærden for fremmednøglen mellem "OrdreProdukter" og "Ordrer".
// I Up-metoden fjernes den eksisterende fremmednøgle og tilføjes igen med Cascade som sletningsadfærd.
// Dette betyder, at hvis en "Ordre" slettes, slettes alle relaterede "OrdreProdukter" automatisk.
// I Down-metoden fjernes denne ændring og gendanner den oprindelige fremmednøgle uden Cascade-adfærd.
// Dette sikrer, at migrationen kan rulles tilbage til den tidligere tilstand.