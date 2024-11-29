using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class AendretILejeAftaleModellen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Forsikring",
                table: "LejeAftaler");

            migrationBuilder.AddColumn<double>(
                name: "ForsikringsPris",
                table: "LejeAftaler",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForsikringsPris",
                table: "LejeAftaler");

            migrationBuilder.AddColumn<bool>(
                name: "Forsikring",
                table: "LejeAftaler",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
