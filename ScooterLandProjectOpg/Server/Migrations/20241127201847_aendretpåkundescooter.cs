using Microsoft.EntityFrameworkCore.Migrations; // Importerer nødvendigt bibliotek til database-migrationer.

#nullable disable // Deaktiverer nullable-analyse for at sikre kompatibilitet med eksisterende kode.

namespace ScooterLandProjectOpg.Server.Migrations // Definerer namespace for migrationen i projektet.
{
    /// <inheritdoc />
    public partial class aendretpåkundescooter : Migration // Opretter en migration med navnet "aendretpåkundescooter".
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // Implementerer ændringer i databasen.
        {
            migrationBuilder.DropColumn( // Fjerner en eksisterende kolonne fra tabellen "Kunder".
                name: "ScooterMaerke", // Navnet på kolonnen, der skal fjernes.
                table: "Kunder"); // Tabellen, hvorfra kolonnen fjernes.

            migrationBuilder.AddColumn<int>( // Tilføjer en ny kolonne til tabellen "KunderScootere".
                name: "ProduktionsAar", // Navnet på den nye kolonne.
                table: "KunderScootere", // Tabellen, hvor kolonnen tilføjes.
                type: "int", // Datatypen for den nye kolonne, her et heltal.
                nullable: true); // Angiver, at kolonnen kan indeholde null-værdier.

            migrationBuilder.AddColumn<string>( // Tilføjer en ny kolonne til tabellen "KunderScootere".
                name: "RegistreringsNummer", // Navnet på den nye kolonne.
                table: "KunderScootere", // Tabellen, hvor kolonnen tilføjes.
                type: "nvarchar(max)", // Datatypen for den nye kolonne, her en streng med ubegrænset længde.
                nullable: true); // Angiver, at kolonnen kan indeholde null-værdier.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // Fortryder ændringerne fra "Up"-metoden.
        {
            migrationBuilder.DropColumn( // Fjerner kolonnen "ProduktionsAar" fra tabellen "KunderScootere".
                name: "ProduktionsAar",
                table: "KunderScootere");

            migrationBuilder.DropColumn( // Fjerner kolonnen "RegistreringsNummer" fra tabellen "KunderScootere".
                name: "RegistreringsNummer",
                table: "KunderScootere");

            migrationBuilder.AddColumn<string>( // Genskaber den tidligere slettede kolonne i tabellen "Kunder".
                name: "ScooterMaerke", // Navnet på kolonnen, der genskabes.
                table: "Kunder", // Tabellen, hvor kolonnen genskabes.
                type: "nvarchar(max)", // Datatypen for kolonnen, her en streng med ubegrænset længde.
                nullable: true); // Angiver, at kolonnen kan indeholde null-værdier.
        }
    }
}


// Denne migration ændrer databasen ved at fjerne kolonnen ScooterMaerke fra tabellen Kunder og tilføjer to nye kolonner, ProduktionsAar og RegistreringsNummer, til tabellen KunderScootere.
// Metoden Down fortryder disse ændringer, hvis det bliver nødvendigt, ved at genskabe den oprindelige struktur.