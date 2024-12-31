using Microsoft.EntityFrameworkCore.Migrations; // Importerer namespace for at arbejde med database-migrationer.

#nullable disable // Deaktiverer nullable-analyse for at opretholde kompatibilitet med eksisterende kode.

namespace ScooterLandProjectOpg.Server.Migrations // Definerer det namespace, migrationen hører til, for at organisere projektet korrekt.
{
    /// <inheritdoc />
    public partial class AendretLejeScooter : Migration // Definerer en migration, der ændrer strukturen i tabellen "LejeScootere".
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // Beskriver de ændringer, der skal anvendes på databasen, når migrationen køres.
        {
            migrationBuilder.AddColumn<bool>( // Tilføjer en ny kolonne af typen bool til tabellen "LejeScootere".
                name: "ErTilgængelig", // Navnet på den nye kolonne.
                table: "LejeScootere", // Tabellen, hvor kolonnen tilføjes.
                type: "bit", // Datatypen for kolonnen i databasen.
                nullable: false, // Angiver, at kolonnen ikke kan have null-værdier.
                defaultValue: false); // Standardværdien for kolonnen sættes til false.

            migrationBuilder.AddColumn<string>( // Tilføjer en ny kolonne af typen string til tabellen "LejeScootere".
                name: "RegistreringsNummer", // Navnet på den nye kolonne.
                table: "LejeScootere", // Tabellen, hvor kolonnen tilføjes.
                type: "nvarchar(max)", // Datatypen for kolonnen i databasen.
                nullable: true); // Angiver, at kolonnen kan have null-værdier.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // Beskriver de ændringer, der skal fortrydes, hvis migrationen rulles tilbage.
        {
            migrationBuilder.DropColumn( // Fjerner kolonnen "ErTilgængelig" fra tabellen "LejeScootere".
                name: "ErTilgængelig", // Navnet på kolonnen, der fjernes.
                table: "LejeScootere"); // Tabellen, hvor kolonnen fjernes.

            migrationBuilder.DropColumn( // Fjerner kolonnen "RegistreringsNummer" fra tabellen "LejeScootere".
                name: "RegistreringsNummer", // Navnet på kolonnen, der fjernes.
                table: "LejeScootere"); // Tabellen, hvor kolonnen fjernes.
        }
    }
}


// Denne migration tilføjer to nye kolonner til tabellen LejeScootere: ErTilgængelig og RegistreringsNummer.
// Kolonnen ErTilgængelig er en bool, der bruges til at indikere, om en scooter er tilgængelig, med en standardværdi sat til false.
// Kolonnen RegistreringsNummer er en string, der kan indeholde registreringsnummeret for scooteren, og den tillader null-værdier.

// I Up-metoden tilføjes disse kolonner til databasen for at udvide funktionaliteten af LejeScootere-tabellen.

// I Down-metoden fjernes begge kolonner for at rulle ændringerne tilbage, hvis migrationen skal fortrydes.
// Dette gendanner tabellen til dens tidligere struktur.