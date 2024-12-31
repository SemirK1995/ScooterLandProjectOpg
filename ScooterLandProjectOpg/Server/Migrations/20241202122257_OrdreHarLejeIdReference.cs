using Microsoft.EntityFrameworkCore.Migrations; // Importerer funktioner til databaseændringer via Entity Framework Core migrations.

#nullable disable // Deaktiverer nullable-analyse for denne fil, hvilket gør det kompatibelt med ældre kode.

namespace ScooterLandProjectOpg.Server.Migrations // Namespace til at organisere migrationsfiler i projektet.
{
    /// <inheritdoc />
    public partial class OrdreHarLejeIdReference : Migration // Migration for at tilføje en LejeId-reference til Ordrer-tabellen.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // Metoden til at definere, hvad der sker, når denne migration anvendes.
        {
            // Tilføjer en ny kolonne "LejeId" til "Ordrer"-tabellen som en nullable int.
            migrationBuilder.AddColumn<int>(
                name: "LejeId", // Navnet på den nye kolonne.
                table: "Ordrer", // Tabellen, hvor kolonnen tilføjes.
                type: "int", // Datatypen for kolonnen.
                nullable: true); // Angiver, at kolonnen kan være null.

            // Opretter en indeks for "LejeId"-kolonnen i "Ordrer"-tabellen for at optimere søgninger.
            migrationBuilder.CreateIndex(
                name: "IX_Ordrer_LejeId", // Navnet på indekset.
                table: "Ordrer", // Tabellen, hvor indekset oprettes.
                column: "LejeId"); // Den kolonne, indekset gælder for.

            // Tilføjer en fremmednøgle-relation mellem "Ordrer"-tabellen og "LejeAftaler"-tabellen via "LejeId".
            migrationBuilder.AddForeignKey(
                name: "FK_Ordrer_LejeAftaler_LejeId", // Navnet på fremmednøglen.
                table: "Ordrer", // Tabellen, der har fremmednøglen.
                column: "LejeId", // Kolonnen, der fungerer som fremmednøgle.
                principalTable: "LejeAftaler", // Tabellen, fremmednøglen peger på.
                principalColumn: "LejeId", // Den kolonne i måltabellen, der matches.
                onDelete: ReferentialAction.Restrict); // Angiver, at relationen ikke kræver sletning af relaterede poster.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // Metoden til at definere, hvad der sker, når denne migration rulles tilbage.
        {
            // Fjerner fremmednøgle-relationen mellem "Ordrer" og "LejeAftaler" via "LejeId".
            migrationBuilder.DropForeignKey(
                name: "FK_Ordrer_LejeAftaler_LejeId", // Navnet på fremmednøglen.
                table: "Ordrer"); // Tabellen, der har fremmednøglen.

            // Fjerner indekset på "LejeId"-kolonnen i "Ordrer"-tabellen.
            migrationBuilder.DropIndex(
                name: "IX_Ordrer_LejeId", // Navnet på indekset.
                table: "Ordrer"); // Tabellen, hvor indekset findes.

            // Sletter "LejeId"-kolonnen fra "Ordrer"-tabellen.
            migrationBuilder.DropColumn(
                name: "LejeId", // Navnet på kolonnen, der fjernes.
                table: "Ordrer"); // Tabellen, hvor kolonnen fjernes.
        }
    }
}


// Denne migrationsklasse tilføjer en ny kolonne kaldet "LejeId" til "Ordrer"-tabellen og opretter en fremmednøgle-relation mellem "Ordrer"-tabellen og "LejeAftaler"-tabellen.
// I Up-metoden tilføjes kolonnen "LejeId" som en nullable int, der kan henvise til en post i "LejeAftaler".
// Derudover oprettes der et indeks for "LejeId"-kolonnen for at optimere forespørgsler, der bruger denne kolonne.
// Fremmednøglen sikrer referentiel integritet mellem "Ordrer" og "LejeAftaler", med en "Restrict"-indstilling, der forhindrer sletning af relaterede poster i "LejeAftaler".

// I Down-metoden rulles ændringerne tilbage.
// Fremmednøglen fjernes først, derefter indekset på "LejeId", og til sidst slettes selve "LejeId"-kolonnen fra "Ordrer".
// Dette gendanner databasen til dens tidligere tilstand, før migrationen blev anvendt.
// Migrationen implementerer dermed både oprettelsen og fjernelsen af denne relation og sikrer en reversibel proces.