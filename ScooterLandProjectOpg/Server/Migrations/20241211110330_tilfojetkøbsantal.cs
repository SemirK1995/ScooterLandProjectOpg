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
            migrationBuilder.RenameColumn(
                name: "Antal",
                table: "Produkter",
                newName: "LagerAntal");

            migrationBuilder.AddColumn<int>(
                name: "KøbsAntal",
                table: "Produkter",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KøbsAntal",
                table: "Produkter");

            migrationBuilder.RenameColumn(
                name: "LagerAntal",
                table: "Produkter",
                newName: "Antal");
        }
    }
}
