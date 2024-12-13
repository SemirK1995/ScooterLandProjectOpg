using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class nyaendring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrdreId",
                table: "LejeAftaler",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LejeAftaler_OrdreId",
                table: "LejeAftaler",
                column: "OrdreId");

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
            migrationBuilder.DropForeignKey(
                name: "FK_LejeAftaler_Ordrer_OrdreId",
                table: "LejeAftaler");

            migrationBuilder.DropIndex(
                name: "IX_LejeAftaler_OrdreId",
                table: "LejeAftaler");

            migrationBuilder.DropColumn(
                name: "OrdreId",
                table: "LejeAftaler");
        }
    }
}
