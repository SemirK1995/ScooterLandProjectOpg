using Microsoft.EntityFrameworkCore.Migrations; // Importerer funktionalitet til at oprette og håndtere migrations i Entity Framework Core.

#nullable disable // Deaktiverer nullable-analyse for at sikre kompatibilitet med ældre kode.

namespace ScooterLandProjectOpg.Server.Migrations // Namespace for migrationen, der organiserer den inden for projektet.
{
    /// <inheritdoc />
    public partial class AendretILejeAftaleModellen : Migration // Definerer en migration, der foretager ændringer i "LejeAftale"-modellen.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // Metode, der specificerer ændringer, der skal anvendes på databasen.
        {
            migrationBuilder.DropColumn( // Fjerner en eksisterende kolonne fra tabellen "LejeAftaler".
                name: "Forsikring", // Navnet på kolonnen, der fjernes.
                table: "LejeAftaler"); // Tabellen, hvorfra kolonnen fjernes.

            migrationBuilder.AddColumn<double>( // Tilføjer en ny kolonne af typen "double" til tabellen "LejeAftaler".
                name: "ForsikringsPris", // Navnet på den nye kolonne.
                table: "LejeAftaler", // Tabellen, hvor kolonnen tilføjes.
                type: "float", // Datatypen for kolonnen i databasen.
                nullable: false, // Angiver, at kolonnen ikke kan indeholde null-værdier.
                defaultValue: 0.0); // Standardværdien for kolonnen er 0.0.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // Metode, der fortryder ændringerne, hvis migrationen rulles tilbage.
        {
            migrationBuilder.DropColumn( // Fjerner den nye kolonne, der blev tilføjet i "Up"-metoden.
                name: "ForsikringsPris", // Navnet på kolonnen, der fjernes.
                table: "LejeAftaler"); // Tabellen, hvorfra kolonnen fjernes.

            migrationBuilder.AddColumn<bool>( // Tilføjer den tidligere fjernede kolonne tilbage til tabellen "LejeAftaler".
                name: "Forsikring", // Navnet på kolonnen, der tilføjes igen.
                table: "LejeAftaler", // Tabellen, hvor kolonnen tilføjes.
                type: "bit", // Datatypen for kolonnen i databasen.
                nullable: false, // Angiver, at kolonnen ikke kan indeholde null-værdier.
                defaultValue: false); // Standardværdien for kolonnen er "false".
        }
    }
}


// Denne migration ændrer modellen for "LejeAftale".
// Den fjerner den eksisterende kolonne "Forsikring", som er af typen bool, og tilføjer en ny kolonne "ForsikringsPris" af typen double.
// "Down"-metoden ruller ændringerne tilbage ved at fjerne "ForsikringsPris" og gendanne den oprindelige "Forsikring"-kolonne.
// Dette sikrer, at databasen kan gå frem og tilbage mellem forskellige versioner.