using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class OrdreHarLejeIdReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			// Tilføj LejeId-kolonne til Ordrer-tabellen
			migrationBuilder.AddColumn<int>(
				name: "LejeId",
				table: "Ordrer",
				type: "int",
				nullable: true);

			migrationBuilder.CreateIndex(
                name: "IX_Ordrer_LejeId",
                table: "Ordrer",
                column: "LejeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordrer_LejeAftaler_LejeId",
                table: "Ordrer",
                column: "LejeId",
                principalTable: "LejeAftaler",
                principalColumn: "LejeId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordrer_LejeAftaler_LejeId",
                table: "Ordrer");

            migrationBuilder.DropIndex(
                name: "IX_Ordrer_LejeId",
                table: "Ordrer");

			// Fjern LejeId-kolonnen
			migrationBuilder.DropColumn(
				name: "LejeId",
				table: "Ordrer");




		}
    }
}
