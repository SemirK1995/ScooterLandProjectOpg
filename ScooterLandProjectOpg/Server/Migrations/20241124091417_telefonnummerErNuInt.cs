using Microsoft.EntityFrameworkCore.Migrations; // Importerer namespace for at kunne arbejde med migrations og databaseændringer.

#nullable disable // Deaktiverer nullable-analyse for at sikre kompatibilitet med ældre kode.

namespace ScooterLandProjectOpg.Server.Migrations // Definerer namespace for migrationen, hvilket strukturerer det som en del af projektet.
{
    /// <inheritdoc />
    public partial class telefonnummerErNuInt : Migration // Angiver, at denne migration ændrer databasen og har en specifik ændring.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // Metode til at definere ændringer, der skal anvendes på databasen, når migrationen køres.
        {
            migrationBuilder.AlterColumn<int>( // Ændrer kolonnens datatype i tabellen.
                name: "Telefonnummer", // Navnet på kolonnen, der ændres.
                table: "Mekanikere", // Tabellen, hvor kolonnen findes.
                type: "int", // Ny datatype for kolonnen.
                nullable: true, // Angiver, at værdien kan være null.
                oldClrType: typeof(string), // Tidligere datatype for kolonnen.
                oldType: "nvarchar(max)", // Tidligere SQL-datatype for kolonnen.
                oldNullable: true); // Tidligere nullable-indstilling for kolonnen.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // Metode til at rulle ændringerne tilbage, hvis migrationen fortrydes.
        {
            migrationBuilder.AlterColumn<string>( // Gendanner kolonnens datatype til den tidligere værdi.
                name: "Telefonnummer", // Navnet på kolonnen, der ændres tilbage.
                table: "Mekanikere", // Tabellen, hvor kolonnen findes.
                type: "nvarchar(max)", // Den oprindelige SQL-datatype for kolonnen.
                nullable: true, // Angiver, at værdien kan være null.
                oldClrType: typeof(int), // Den aktuelle datatype for kolonnen, der ændres tilbage.
                oldType: "int", // Den aktuelle SQL-datatype for kolonnen.
                oldNullable: true); // Den aktuelle nullable-indstilling for kolonnen.
        }
    }
}


// Denne migration ændrer datatypen for kolonnen Telefonnummer i tabellen Mekanikere fra en streng (string) til et heltal (int).
// Denne ændring gør det muligt at gemme telefonnumre som numeriske værdier frem for tekst.
// I Up-metoden anvendes ændringen, så telefonnummeret gemmes som en integer, og kolonnen tillader fortsat null-værdier.

// I Down-metoden rulles ændringen tilbage, så kolonnen igen gemmer telefonnummeret som tekst (string) med samme nullable-indstilling som før.
// Dette giver mulighed for at gendanne den oprindelige databasekonfiguration, hvis migrationen fortrydes.