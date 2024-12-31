using Microsoft.EntityFrameworkCore.Migrations; // Importerer biblioteket for at understøtte database-migrationer.

#nullable disable // Deaktiverer nullable-analyse for at sikre kompatibilitet med tidligere kode.

namespace ScooterLandProjectOpg.Server.Migrations // Definerer namespace for denne migration i projektet.
{
    /// <inheritdoc />
    public partial class MakeLejeIdNullable : Migration // Definerer en migration til at gøre "LejeId" nullable i tabellen "LejeScootere".
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // Metoden der anvender ændringerne til databasen.
        {
            migrationBuilder.DropForeignKey( // Fjerner den eksisterende fremmednøgle-relation mellem "LejeScootere" og "LejeAftaler".
                name: "FK_LejeScootere_LejeAftaler_LejeId", // Navnet på fremmednøglen, der fjernes.
                table: "LejeScootere"); // Tabellen hvor fremmednøglen er defineret.

            migrationBuilder.AlterColumn<int>( // Ændrer "LejeId"-kolonnen i "LejeScootere" til at tillade null-værdier.
                name: "LejeId", // Navnet på kolonnen, der ændres.
                table: "LejeScootere", // Tabellen hvor kolonnen er defineret.
                type: "int", // Datatypen for kolonnen.
                nullable: true, // Tillader null-værdier i kolonnen.
                oldClrType: typeof(int), // Tidligere datatype for kolonnen.
                oldType: "int"); // Tidligere typebeskrivelse.

            migrationBuilder.AddForeignKey( // Tilføjer en ny fremmednøgle-relation med opdaterede egenskaber.
                name: "FK_LejeScootere_LejeAftaler_LejeId", // Navnet på den nye fremmednøgle.
                table: "LejeScootere", // Tabellen hvor fremmednøglen er defineret.
                column: "LejeId", // Kolonnen i "LejeScootere", der refererer til "LejeAftaler".
                principalTable: "LejeAftaler", // Tabellen der refereres til.
                principalColumn: "LejeId"); // Den primære nøgle i "LejeAftaler", som fremmednøglen peger på.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // Metoden der fortryder ændringerne i databasen.
        {
            migrationBuilder.DropForeignKey( // Fjerner den opdaterede fremmednøgle-relation.
                name: "FK_LejeScootere_LejeAftaler_LejeId", // Navnet på fremmednøglen, der fjernes.
                table: "LejeScootere"); // Tabellen hvor fremmednøglen er defineret.

            migrationBuilder.AlterColumn<int>( // Ændrer "LejeId"-kolonnen tilbage til at være ikke-null.
                name: "LejeId", // Navnet på kolonnen, der ændres.
                table: "LejeScootere", // Tabellen hvor kolonnen er defineret.
                type: "int", // Datatypen for kolonnen.
                nullable: false, // Tillader ikke null-værdier.
                defaultValue: 0, // Standardværdi for kolonnen.
                oldClrType: typeof(int), // Tidligere datatype for kolonnen.
                oldType: "int", // Tidligere typebeskrivelse.
                oldNullable: true); // Indikator for at kolonnen tidligere tillod null-værdier.

            migrationBuilder.AddForeignKey( // Genskaber den oprindelige fremmednøgle-relation.
                name: "FK_LejeScootere_LejeAftaler_LejeId", // Navnet på fremmednøglen.
                table: "LejeScootere", // Tabellen hvor fremmednøglen er defineret.
                column: "LejeId", // Kolonnen i "LejeScootere", der refererer til "LejeAftaler".
                principalTable: "LejeAftaler", // Tabellen der refereres til.
                principalColumn: "LejeId", // Den primære nøgle i "LejeAftaler", som fremmednøglen peger på.
                onDelete: ReferentialAction.Cascade); // Angiver, at relaterede rækker skal slettes ved sletning i "LejeAftaler".
        }
    }
}


// Denne migration ændrer kolonnen LejeId i tabellen LejeScootere, så den tillader null-værdier. Dette muliggør, at en scooter ikke nødvendigvis skal være tilknyttet en lejeaftale.
// I Up-metoden fjernes den eksisterende fremmednøgle, og kolonnen LejeId ændres til at være nullable. Derefter oprettes en ny fremmednøgle-relation mellem LejeScootere og LejeAftaler, hvor null-værdier accepteres.
// Dette sikrer fleksibilitet i relationen mellem de to tabeller.

// I Down-metoden rulles ændringerne tilbage ved at gøre LejeId ikke-null igen og genskabe den oprindelige fremmednøgle-relation med en Cascade-sletningspolitik.