using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class MakeLejeIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LejeScootere_LejeAftaler_LejeId",
                table: "LejeScootere");

            migrationBuilder.AlterColumn<int>(
                name: "LejeId",
                table: "LejeScootere",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LejeScootere_LejeAftaler_LejeId",
                table: "LejeScootere",
                column: "LejeId",
                principalTable: "LejeAftaler",
                principalColumn: "LejeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LejeScootere_LejeAftaler_LejeId",
                table: "LejeScootere");

            migrationBuilder.AlterColumn<int>(
                name: "LejeId",
                table: "LejeScootere",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LejeScootere_LejeAftaler_LejeId",
                table: "LejeScootere",
                column: "LejeId",
                principalTable: "LejeAftaler",
                principalColumn: "LejeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
