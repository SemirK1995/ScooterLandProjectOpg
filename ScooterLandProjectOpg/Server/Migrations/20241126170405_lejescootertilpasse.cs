using Microsoft.EntityFrameworkCore.Migrations; // Importerer biblioteket til håndtering af database-migrationer.

#nullable disable // Deaktiverer nullable-analyse for at sikre kompatibilitet med tidligere kode.

namespace ScooterLandProjectOpg.Server.Migrations // Definerer namespace for migrationer i projektet.
{
    /// <inheritdoc />
    public partial class lejescootertilpasse : Migration // Definerer en migration med navnet "lejescootertilpasse".
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // Denne metode implementerer ændringer til databasen.
        {
            migrationBuilder.DropForeignKey( // Fjerner en eksisterende fremmednøgle-relation fra tabellen "LejeScootere" til "LejeAftaler".
                name: "FK_LejeScootere_LejeAftaler_LejeId", // Navnet på den fremmednøgle-relation, der skal fjernes.
                table: "LejeScootere"); // Tabellen, som fremmednøglen er tilknyttet.

            migrationBuilder.AddForeignKey( // Tilføjer en ny fremmednøgle-relation fra "LejeScootere" til "LejeAftaler".
                name: "FK_LejeScootere_LejeAftaler_LejeId", // Navnet på den nye fremmednøgle-relation.
                table: "LejeScootere", // Tabellen, som fremmednøglen skal knyttes til.
                column: "LejeId", // Kolonnen i "LejeScootere", der fungerer som fremmednøgle.
                principalTable: "LejeAftaler", // Den relaterede tabel, som fremmednøglen peger på.
                principalColumn: "LejeId", // Kolonnen i "LejeAftaler", som fremmednøglen refererer til.
                onDelete: ReferentialAction.Restrict); // Ændrer sletteadfærden til "Restrict", så relaterede data ikke slettes automatisk.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // Denne metode fortryder ændringerne, der er implementeret i "Up".
        {
            migrationBuilder.DropForeignKey( // Fjerner fremmednøgle-relationen tilføjet i "Up".
                name: "FK_LejeScootere_LejeAftaler_LejeId", // Navnet på den fremmednøgle-relation, der skal fjernes.
                table: "LejeScootere"); // Tabellen, som fremmednøglen er knyttet til.

            migrationBuilder.AddForeignKey( // Tilføjer den oprindelige fremmednøgle-relation tilbage mellem "LejeScootere" og "LejeAftaler".
                name: "FK_LejeScootere_LejeAftaler_LejeId", // Navnet på den oprindelige fremmednøgle-relation.
                table: "LejeScootere", // Tabellen, som fremmednøglen skal knyttes til.
                column: "LejeId", // Kolonnen i "LejeScootere", der fungerer som fremmednøgle.
                principalTable: "LejeAftaler", // Den relaterede tabel, som fremmednøglen peger på.
                principalColumn: "LejeId"); // Kolonnen i "LejeAftaler", som fremmednøglen refererer til.
        }
    }
}


// Denne migration ændrer sletteadfærden for fremmednøglen mellem "LejeScootere" og "LejeAftaler".
// I metoden Up ændres relationen til at bruge ReferentialAction.Restrict, så relaterede data ikke slettes automatisk, mens metoden Down genopretter den oprindelige relation uden restriktioner.