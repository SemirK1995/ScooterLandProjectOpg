using Microsoft.EntityFrameworkCore.Migrations; // Importerer biblioteket, der bruges til at arbejde med migrationer og databaseændringer.

#nullable disable // Deaktiverer nullable-analyse for at sikre kompatibilitet med eksisterende kode, hvor nullable-annotations ikke er brugt.

namespace ScooterLandProjectOpg.Server.Migrations // Definerer namespace for at strukturere migrationen som en del af projektet.
{
    /// <inheritdoc />
    public partial class tilfojetekstra : Migration // Definerer en ny migration, der ændrer strukturen i databasen.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // Beskriver ændringer, der skal anvendes på databasen, når migrationen køres.
        {
            migrationBuilder.DropForeignKey( // Fjerner den eksisterende fremmednøgle-relation mellem "Produkter" og "Ordrer".
                name: "FK_Produkter_Ordrer_OrdreId", // Navnet på den fremmednøgle, der skal fjernes.
                table: "Produkter"); // Tabellen, hvor fremmednøglen er defineret.

            migrationBuilder.DropIndex( // Fjerner indekset, der er knyttet til kolonnen "OrdreId" i tabellen "Produkter".
                name: "IX_Produkter_OrdreId", // Navnet på det indeks, der fjernes.
                table: "Produkter"); // Tabellen, hvor indekset findes.

            migrationBuilder.DropColumn( // Fjerner kolonnen "OrdreId" fra tabellen "Produkter".
                name: "OrdreId", // Navnet på kolonnen, der fjernes.
                table: "Produkter"); // Tabellen, hvor kolonnen findes.

            migrationBuilder.CreateTable( // Opretter en ny tabel kaldet "OrdreProdukter".
                name: "OrdreProdukter", // Navnet på den nye tabel.
                columns: table => new // Definerer kolonnerne i tabellen.
                {
                    OrdreProduktId = table.Column<int>(type: "int", nullable: false) // Opretter en kolonne som primærnøgle med auto-increment.
                        .Annotation("SqlServer:Identity", "1, 1"), // Angiver, at værdien auto-incrementeres startende fra 1.
                    OrdreId = table.Column<int>(type: "int", nullable: true), // Tilføjer en fremmednøglekolonne til "Ordrer", der kan være null.
                    ProduktId = table.Column<int>(type: "int", nullable: true), // Tilføjer en fremmednøglekolonne til "Produkter", der kan være null.
                    Antal = table.Column<int>(type: "int", nullable: false), // Tilføjer en kolonne til at angive antallet af produkter, som ikke kan være null.
                    Pris = table.Column<double>(type: "float", nullable: false) // Tilføjer en kolonne til prisen på produktet, som ikke kan være null.
                },
                constraints: table => // Definerer primærnøgle og fremmednøglerelationer.
                {
                    table.PrimaryKey("PK_OrdreProdukter", x => x.OrdreProduktId); // Angiver "OrdreProduktId" som primærnøgle.
                    table.ForeignKey( // Tilføjer en fremmednøgle, der refererer til "Ordrer".
                        name: "FK_OrdreProdukter_Ordrer_OrdreId", // Navnet på fremmednøglen.
                        column: x => x.OrdreId, // Kolonnen i "OrdreProdukter", der refererer til "Ordrer".
                        principalTable: "Ordrer", // Tabellen, der refereres til.
                        principalColumn: "OrdreId"); // Den primære nøgle i "Ordrer", som fremmednøglen peger på.
                    table.ForeignKey( // Tilføjer en fremmednøgle, der refererer til "Produkter".
                        name: "FK_OrdreProdukter_Produkter_ProduktId", // Navnet på fremmednøglen.
                        column: x => x.ProduktId, // Kolonnen i "OrdreProdukter", der refererer til "Produkter".
                        principalTable: "Produkter", // Tabellen, der refereres til.
                        principalColumn: "ProduktId"); // Den primære nøgle i "Produkter", som fremmednøglen peger på.
                });

            migrationBuilder.CreateIndex( // Opretter et indeks på kolonnen "OrdreId" i tabellen "OrdreProdukter".
                name: "IX_OrdreProdukter_OrdreId", // Navnet på det nye indeks.
                table: "OrdreProdukter", // Tabellen, hvor indekset oprettes.
                column: "OrdreId"); // Kolonnen, der indekseres.

            migrationBuilder.CreateIndex( // Opretter et indeks på kolonnen "ProduktId" i tabellen "OrdreProdukter".
                name: "IX_OrdreProdukter_ProduktId", // Navnet på det nye indeks.
                table: "OrdreProdukter", // Tabellen, hvor indekset oprettes.
                column: "ProduktId"); // Kolonnen, der indekseres.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // Beskriver, hvordan ændringerne kan fortrydes, hvis migrationen rulles tilbage.
        {
            migrationBuilder.DropTable( // Fjerner tabellen "OrdreProdukter".
                name: "OrdreProdukter"); // Navnet på tabellen, der fjernes.

            migrationBuilder.AddColumn<int>( // Gendanner kolonnen "OrdreId" i tabellen "Produkter".
                name: "OrdreId", // Navnet på kolonnen, der genskabes.
                table: "Produkter", // Tabellen, hvor kolonnen tilføjes.
                type: "int", // Datatypen for kolonnen.
                nullable: true); // Angiver, at kolonnen kan være null.

            migrationBuilder.CreateIndex( // Gendanner indekset på kolonnen "OrdreId" i tabellen "Produkter".
                name: "IX_Produkter_OrdreId", // Navnet på det genskabte indeks.
                table: "Produkter", // Tabellen, hvor indekset genskabes.
                column: "OrdreId"); // Kolonnen, der indekseres.

            migrationBuilder.AddForeignKey( // Gendanner fremmednøgle-relationen mellem "Produkter" og "Ordrer".
                name: "FK_Produkter_Ordrer_OrdreId", // Navnet på fremmednøglen.
                table: "Produkter", // Tabellen, hvor fremmednøglen genskabes.
                column: "OrdreId", // Kolonnen i "Produkter", der refererer til "Ordrer".
                principalTable: "Ordrer", // Tabellen, der refereres til.
                principalColumn: "OrdreId"); // Den primære nøgle i "Ordrer", som fremmednøglen peger på.
        }
    }
}


// Denne migration omstrukturerer relationen mellem ordrer og produkter ved at fjerne kolonnen OrdreId fra tabellen Produkter
// og i stedet oprette en ny tabel OrdreProdukter, som etablerer en mange-til-mange relation mellem ordrer og produkter.

// I Up-metoden fjernes den eksisterende fremmednøgle og indekset på kolonnen OrdreId i tabellen Produkter, og kolonnen slettes.
// Derefter oprettes tabellen OrdreProdukter, som inkluderer kolonner til en ny primærnøgle (OrdreProduktId), fremmednøgler til OrdreId og ProduktId, samt yderligere oplysninger som Antal og Pris.
// Fremmednøgler og indekser sikrer referentiel integritet og forbedrer ydelsen ved opslag i den nye tabel.

// I Down-metoden fjernes tabellen OrdreProdukter, og kolonnen OrdreId gendannes i tabellen Produkter.
// Fremmednøglen og indekset på OrdreId i Produkter genoprettes for at vende tilbage til den oprindelige relation mellem ordrer og produkter.