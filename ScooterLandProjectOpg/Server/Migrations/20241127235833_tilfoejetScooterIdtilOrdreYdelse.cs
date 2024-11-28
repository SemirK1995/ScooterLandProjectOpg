using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class tilfoejetScooterIdtilOrdreYdelse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScooterId",
                table: "OrdreYdelser",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdreYdelser_ScooterId",
                table: "OrdreYdelser",
                column: "ScooterId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdreYdelser_KunderScootere_ScooterId",
                table: "OrdreYdelser",
                column: "ScooterId",
                principalTable: "KunderScootere",
                principalColumn: "ScooterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdreYdelser_KunderScootere_ScooterId",
                table: "OrdreYdelser");

            migrationBuilder.DropIndex(
                name: "IX_OrdreYdelser_ScooterId",
                table: "OrdreYdelser");

            migrationBuilder.DropColumn(
                name: "ScooterId",
                table: "OrdreYdelser");
        }
    }
}
