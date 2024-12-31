using System; // Importerer grundlæggende funktionalitet fra .NET, herunder DateTime-typen.
using Microsoft.EntityFrameworkCore.Migrations; // Importerer funktionalitet til Entity Framework Core-migrationer.

#nullable disable // Deaktiverer nullable-analyse for at sikre kompatibilitet med ældre kode.

namespace ScooterLandProjectOpg.Server.Migrations // Definerer namespace for migrationen, så den organiseres korrekt i projektet.
{
    /// <inheritdoc />
    public partial class tilfoejetstartogsluttilordreydelse : Migration // Definerer en migration, der tilføjer start- og slutdato til "OrdreYdelser".
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // Denne metode specificerer ændringer, der skal anvendes på databasen.
        {
            migrationBuilder.AddColumn<DateTime>( // Tilføjer en ny kolonne af typen "DateTime" til tabellen "OrdreYdelser".
                name: "SlutDato", // Navnet på den nye kolonne.
                table: "OrdreYdelser", // Tabellen, hvor kolonnen tilføjes.
                type: "datetime2", // Datatypen for kolonnen i databasen.
                nullable: true); // Angiver, at denne kolonne kan indeholde null-værdier.

            migrationBuilder.AddColumn<DateTime>( // Tilføjer en anden kolonne af typen "DateTime" til tabellen "OrdreYdelser".
                name: "StartDato", // Navnet på den nye kolonne.
                table: "OrdreYdelser", // Tabellen, hvor kolonnen tilføjes.
                type: "datetime2", // Datatypen for kolonnen i databasen.
                nullable: true); // Angiver, at denne kolonne kan indeholde null-værdier.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // Denne metode fortryder ændringerne, der er specificeret i "Up"-metoden.
        {
            migrationBuilder.DropColumn( // Fjerner kolonnen "SlutDato" fra tabellen "OrdreYdelser".
                name: "SlutDato", // Navnet på kolonnen, der skal fjernes.
                table: "OrdreYdelser"); // Tabellen, hvorfra kolonnen fjernes.

            migrationBuilder.DropColumn( // Fjerner kolonnen "StartDato" fra tabellen "OrdreYdelser".
                name: "StartDato", // Navnet på kolonnen, der skal fjernes.
                table: "OrdreYdelser"); // Tabellen, hvorfra kolonnen fjernes.
        }
    }
}


// Denne migration tilføjer to kolonner, "StartDato" og "SlutDato", af typen DateTime til tabellen "OrdreYdelser".
// Disse kolonner gør det muligt at gemme start- og slutdatoer for en ordreydelse.
// "Down"-metoden fjerner begge kolonner, hvis migrationen rulles tilbage, så databasen vender tilbage til sin tidligere tilstand.