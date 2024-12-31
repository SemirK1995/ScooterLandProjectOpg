using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class cascadedeleteforkunde : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Fjerner den eksisterende fremmednøgle-relation mellem "LejeScootere" og "LejeAftaler" på kolonnen "LejeId".
            migrationBuilder.DropForeignKey(
                name: "FK_LejeScootere_LejeAftaler_LejeId",
                table: "LejeScootere");

            // Ændrer kolonnen "RegistreringsNummer" i tabellen "KunderScootere" til at være ikke-null (obligatorisk)
            // med en standardværdi af en tom streng, og opdaterer datatypen til `nvarchar(max)`.
            migrationBuilder.AlterColumn<string>(
                name: "RegistreringsNummer",
                table: "KunderScootere",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            // Ændrer kolonnen "ProduktionsAar" i tabellen "KunderScootere" til at være ikke-null (obligatorisk)
            // med en standardværdi af 0. Den gamle version tillod null-værdier.
            migrationBuilder.AlterColumn<int>(
                name: "ProduktionsAar",
                table: "KunderScootere",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            // Ændrer kolonnen "Navn" i tabellen "Kunder" til at være ikke-null (obligatorisk)
            // og begrænser længden til maksimalt 100 tegn. Den tidligere version havde ingen længdebegrænsning.
            migrationBuilder.AlterColumn<string>(
                name: "Navn",
                table: "Kunder",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            // Ændrer kolonnen "Adresse" i tabellen "Kunder" til at være ikke-null (obligatorisk)
            // og begrænser længden til maksimalt 200 tegn. Den tidligere version havde ingen længdebegrænsning.
            migrationBuilder.AlterColumn<string>(
                name: "Adresse",
                table: "Kunder",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            // Tilføjer fremmednøglen igen mellem "LejeScootere" og "LejeAftaler" på kolonnen "LejeId".
            // Hvis en "LejeAftale" slettes, vil de tilhørende "LejeScootere" også blive slettet.
            migrationBuilder.AddForeignKey(
                name: "FK_LejeScootere_LejeAftaler_LejeId",
                table: "LejeScootere",
                column: "LejeId",
                principalTable: "LejeAftaler",
                principalColumn: "LejeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Fjerner den eksisterende fremmednøgle-relation mellem "LejeScootere" og "LejeAftaler" på kolonnen "LejeId".
            migrationBuilder.DropForeignKey(
                name: "FK_LejeScootere_LejeAftaler_LejeId",
                table: "LejeScootere");

            // Ændrer kolonnen "RegistreringsNummer" i tabellen "KunderScootere" tilbage til at tillade null-værdier.
            migrationBuilder.AlterColumn<string>(
                name: "RegistreringsNummer",
                table: "KunderScootere",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            // Ændrer kolonnen "ProduktionsAar" i tabellen "KunderScootere" tilbage til at tillade null-værdier.
            migrationBuilder.AlterColumn<int>(
                name: "ProduktionsAar",
                table: "KunderScootere",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            // Ændrer kolonnen "Navn" i tabellen "Kunder" tilbage til en string uden længdebegrænsning.
            migrationBuilder.AlterColumn<string>(
                name: "Navn",
                table: "Kunder",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            // Ændrer kolonnen "Adresse" i tabellen "Kunder" tilbage til en string uden længdebegrænsning.
            migrationBuilder.AlterColumn<string>(
                name: "Adresse",
                table: "Kunder",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            // Tilføjer fremmednøglen igen mellem "LejeScootere" og "LejeAftaler" på kolonnen "LejeId",
            // men med en restriktion, der forhindrer sletning af relaterede rækker.
            migrationBuilder.AddForeignKey(
                name: "FK_LejeScootere_LejeAftaler_LejeId",
                table: "LejeScootere",
                column: "LejeId",
                principalTable: "LejeAftaler",
                principalColumn: "LejeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}