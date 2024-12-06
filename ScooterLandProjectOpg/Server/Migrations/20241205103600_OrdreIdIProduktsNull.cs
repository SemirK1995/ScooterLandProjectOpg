using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class OrdreIdIProduktsNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produkter_Ordrer_OrdreId",
                table: "Produkter");

            migrationBuilder.AlterColumn<int>(
                name: "OrdreId",
                table: "Produkter",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Produkter_Ordrer_OrdreId",
                table: "Produkter",
                column: "OrdreId",
                principalTable: "Ordrer",
                principalColumn: "OrdreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produkter_Ordrer_OrdreId",
                table: "Produkter");

            migrationBuilder.AlterColumn<int>(
                name: "OrdreId",
                table: "Produkter",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Produkter_Ordrer_OrdreId",
                table: "Produkter",
                column: "OrdreId",
                principalTable: "Ordrer",
                principalColumn: "OrdreId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
