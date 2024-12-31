using Microsoft.EntityFrameworkCore.Migrations; // Importerer EF Core Migrations, der bruges til at oprette og håndtere databaseændringer.

#nullable disable // Deaktiverer nullable-analyse for at sikre kompatibilitet med eksisterende kode.

namespace ScooterLandProjectOpg.Server.Migrations // Definerer namespace, som matcher projektets struktur og holder migrationer organiseret.
{
    /// <inheritdoc />
    public partial class tilfoejetScooterIdtilOrdreYdelse : Migration // Definerer en ny migration med navnet "tilfoejetScooterIdtilOrdreYdelse".
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // Indeholder de ændringer, der skal anvendes på databasen, når migrationen køres.
        {
            migrationBuilder.AddColumn<int>( // Tilføjer en ny kolonne med navnet "ScooterId" til tabellen "OrdreYdelser".
                name: "ScooterId", // Navnet på den nye kolonne.
                table: "OrdreYdelser", // Tabellen, som kolonnen skal tilføjes til.
                type: "int", // Datatypen for kolonnen.
                nullable: true); // Angiver, at kolonnen er nullable, hvilket betyder, at den kan indeholde null-værdier.

            migrationBuilder.CreateIndex( // Opretter en indeks på kolonnen "ScooterId" i tabellen "OrdreYdelser".
                name: "IX_OrdreYdelser_ScooterId", // Navnet på indekset.
                table: "OrdreYdelser", // Tabellen, som indekset tilføjes til.
                column: "ScooterId"); // Kolonnen, som indekset skal indeholde.

            migrationBuilder.AddForeignKey( // Tilføjer en fremmednøgle-relation mellem "OrdreYdelser" og "KunderScootere".
                name: "FK_OrdreYdelser_KunderScootere_ScooterId", // Navnet på fremmednøgle-relationen.
                table: "OrdreYdelser", // Tabellen, som fremmednøglen tilføjes til.
                column: "ScooterId", // Kolonnen, der fungerer som fremmednøgle.
                principalTable: "KunderScootere", // Tabellen, som fremmednøglen refererer til.
                principalColumn: "ScooterId"); // Primærnøglen i den refererede tabel.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // Indeholder logikken til at fortryde ændringerne fra "Up"-metoden.
        {
            migrationBuilder.DropForeignKey( // Fjerner fremmednøgle-relationen mellem "OrdreYdelser" og "KunderScootere".
                name: "FK_OrdreYdelser_KunderScootere_ScooterId", // Navnet på den fremmednøgle-relation, der skal fjernes.
                table: "OrdreYdelser"); // Tabellen, som fremmednøglen fjernes fra.

            migrationBuilder.DropIndex( // Fjerner det indeks, der tidligere blev oprettet på kolonnen "ScooterId".
                name: "IX_OrdreYdelser_ScooterId", // Navnet på indekset.
                table: "OrdreYdelser"); // Tabellen, som indekset fjernes fra.

            migrationBuilder.DropColumn( // Fjerner kolonnen "ScooterId" fra tabellen "OrdreYdelser".
                name: "ScooterId", // Navnet på kolonnen, der skal fjernes.
                table: "OrdreYdelser"); // Tabellen, som kolonnen fjernes fra.
        }
    }
}


// Denne migration tilføjer en ny kolonne "ScooterId" til tabellen "OrdreYdelser" for at etablere en relation til tabellen "KunderScootere".
// Den opretter også et indeks og en fremmednøgle for at sikre referentiel integritet.
// "Down"-metoden fortryder alle disse ændringer, hvis migrationen skal rulles tilbage.