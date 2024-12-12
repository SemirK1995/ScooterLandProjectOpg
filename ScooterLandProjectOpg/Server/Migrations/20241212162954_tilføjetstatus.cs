using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class tilføjetstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LejeAftaler",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "LejeAftaler");
        }
    }
}
