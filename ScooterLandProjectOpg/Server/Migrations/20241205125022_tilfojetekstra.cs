using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class tilfojetekstra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produkter_Ordrer_OrdreId",
                table: "Produkter");

            migrationBuilder.DropIndex(
                name: "IX_Produkter_OrdreId",
                table: "Produkter");

            migrationBuilder.DropColumn(
                name: "OrdreId",
                table: "Produkter");

            migrationBuilder.CreateTable(
                name: "OrdreProdukter",
                columns: table => new
                {
                    OrdreProduktId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdreId = table.Column<int>(type: "int", nullable: true),
                    ProduktId = table.Column<int>(type: "int", nullable: true),
                    Antal = table.Column<int>(type: "int", nullable: false),
                    Pris = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdreProdukter", x => x.OrdreProduktId);
                    table.ForeignKey(
                        name: "FK_OrdreProdukter_Ordrer_OrdreId",
                        column: x => x.OrdreId,
                        principalTable: "Ordrer",
                        principalColumn: "OrdreId");
                    table.ForeignKey(
                        name: "FK_OrdreProdukter_Produkter_ProduktId",
                        column: x => x.ProduktId,
                        principalTable: "Produkter",
                        principalColumn: "ProduktId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdreProdukter_OrdreId",
                table: "OrdreProdukter",
                column: "OrdreId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdreProdukter_ProduktId",
                table: "OrdreProdukter",
                column: "ProduktId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdreProdukter");

            migrationBuilder.AddColumn<int>(
                name: "OrdreId",
                table: "Produkter",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produkter_OrdreId",
                table: "Produkter",
                column: "OrdreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produkter_Ordrer_OrdreId",
                table: "Produkter",
                column: "OrdreId",
                principalTable: "Ordrer",
                principalColumn: "OrdreId");
        }
    }
}
