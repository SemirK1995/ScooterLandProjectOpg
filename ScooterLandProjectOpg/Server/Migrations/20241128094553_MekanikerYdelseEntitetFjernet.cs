using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class MekanikerYdelseEntitetFjernet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MekanikerYdelser");

            migrationBuilder.AddColumn<int>(
                name: "MekanikerId",
                table: "OrdreYdelser",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdreYdelser_MekanikerId",
                table: "OrdreYdelser",
                column: "MekanikerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdreYdelser_Mekanikere_MekanikerId",
                table: "OrdreYdelser",
                column: "MekanikerId",
                principalTable: "Mekanikere",
                principalColumn: "MekanikerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdreYdelser_Mekanikere_MekanikerId",
                table: "OrdreYdelser");

            migrationBuilder.DropIndex(
                name: "IX_OrdreYdelser_MekanikerId",
                table: "OrdreYdelser");

            migrationBuilder.DropColumn(
                name: "MekanikerId",
                table: "OrdreYdelser");

            migrationBuilder.CreateTable(
                name: "MekanikerYdelser",
                columns: table => new
                {
                    MekanikerYdelseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MekanikerId = table.Column<int>(type: "int", nullable: false),
                    YdelseId = table.Column<int>(type: "int", nullable: false),
                    SlutDato = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDato = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MekanikerYdelser", x => x.MekanikerYdelseId);
                    table.ForeignKey(
                        name: "FK_MekanikerYdelser_Mekanikere_MekanikerId",
                        column: x => x.MekanikerId,
                        principalTable: "Mekanikere",
                        principalColumn: "MekanikerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MekanikerYdelser_Ydelser_YdelseId",
                        column: x => x.YdelseId,
                        principalTable: "Ydelser",
                        principalColumn: "YdelseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MekanikerYdelser_MekanikerId",
                table: "MekanikerYdelser",
                column: "MekanikerId");

            migrationBuilder.CreateIndex(
                name: "IX_MekanikerYdelser_YdelseId",
                table: "MekanikerYdelser",
                column: "YdelseId");
        }
    }
}
