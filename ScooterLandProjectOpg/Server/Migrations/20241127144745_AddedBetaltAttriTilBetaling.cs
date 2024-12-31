using Microsoft.EntityFrameworkCore.Migrations; // Importerer biblioteket til håndtering af database-migrationer.

#nullable disable // Deaktiverer nullable-analyse for at sikre kompatibilitet med eksisterende kode.

namespace ScooterLandProjectOpg.Server.Migrations // Definerer namespace for migrationer i projektet.
{
    /// <inheritdoc />
    public partial class AddedBetaltAttriTilBetaling : Migration // Definerer en migration med navnet "AddedBetaltAttriTilBetaling".
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // Implementerer ændringer, der skal anvendes på databasen.
        {
            migrationBuilder.AddColumn<bool>( // Tilføjer en ny kolonne til tabellen "Betalinger".
                name: "Betalt", // Navnet på den nye kolonne.
                table: "Betalinger", // Tabellen, hvor kolonnen skal tilføjes.
                type: "bit", // Datatypen for kolonnen, her en boolean-værdi.
                nullable: false, // Angiver, at kolonnen ikke kan indeholde null-værdier.
                defaultValue: false); // Sætter standardværdien for kolonnen til false.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // Fortryder ændringerne, der er implementeret i "Up".
        {
            migrationBuilder.DropColumn( // Fjerner den tilføjede kolonne fra tabellen "Betalinger".
                name: "Betalt", // Navnet på kolonnen, der skal fjernes.
                table: "Betalinger"); // Tabellen, hvorfra kolonnen skal fjernes.
        }
    }
}


// Denne migration tilføjer en ny boolean-kolonne kaldet Betalt til tabellen Betalinger med en standardværdi på false i metoden Up.
// Metoden Down fjerner denne kolonne for at fortryde ændringen, hvis det kræves. Dette sikrer, at ændringen er reversibel.