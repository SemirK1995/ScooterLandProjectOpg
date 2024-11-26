using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class lejescootertilpasse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LejeScootere_LejeAftaler_LejeId",
                table: "LejeScootere");

            migrationBuilder.AddForeignKey(
                name: "FK_LejeScootere_LejeAftaler_LejeId",
                table: "LejeScootere",
                column: "LejeId",
                principalTable: "LejeAftaler",
                principalColumn: "LejeId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LejeScootere_LejeAftaler_LejeId",
                table: "LejeScootere");

            migrationBuilder.AddForeignKey(
                name: "FK_LejeScootere_LejeAftaler_LejeId",
                table: "LejeScootere",
                column: "LejeId",
                principalTable: "LejeAftaler",
                principalColumn: "LejeId");
        }
    }
}
