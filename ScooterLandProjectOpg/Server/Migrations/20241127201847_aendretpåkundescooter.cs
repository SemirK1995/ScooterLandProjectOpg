using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class aendretpåkundescooter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScooterMaerke",
                table: "Kunder");

            migrationBuilder.AddColumn<int>(
                name: "ProduktionsAar",
                table: "KunderScootere",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistreringsNummer",
                table: "KunderScootere",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProduktionsAar",
                table: "KunderScootere");

            migrationBuilder.DropColumn(
                name: "RegistreringsNummer",
                table: "KunderScootere");

            migrationBuilder.AddColumn<string>(
                name: "ScooterMaerke",
                table: "Kunder",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
