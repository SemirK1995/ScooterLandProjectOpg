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
            migrationBuilder.DropForeignKey(
                name: "FK_OrdreProdukter_Ordrer_OrdreId",
                table: "OrdreProdukter");

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
            migrationBuilder.DropForeignKey(
                name: "FK_OrdreProdukter_Ordrer_OrdreId",
                table: "OrdreProdukter");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdreProdukter_Ordrer_OrdreId",
                table: "OrdreProdukter",
                column: "OrdreId",
                principalTable: "Ordrer",
                principalColumn: "OrdreId");
        }
    }
}
