using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class AendretLejeScooter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ErTilgængelig",
                table: "LejeScootere",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RegistreringsNummer",
                table: "LejeScootere",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErTilgængelig",
                table: "LejeScootere");

            migrationBuilder.DropColumn(
                name: "RegistreringsNummer",
                table: "LejeScootere");
        }
    }
}
