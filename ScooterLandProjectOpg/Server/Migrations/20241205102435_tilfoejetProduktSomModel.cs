using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class tilfoejetProduktSomModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Opretter en ny tabel kaldet "Produkter" i databasen
            migrationBuilder.CreateTable(
                name: "Produkter",
                columns: table => new
                {
                    ProduktId = table.Column<int>(type: "int", nullable: false) // Opretter en kolonne til produktets ID og definerer den som primærnøgle med auto-increment
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdreId = table.Column<int>(type: "int", nullable: false), // Tilføjer en kolonne til at lagre en reference til en ordre via dens ID
                    ProduktNavn = table.Column<string>(type: "nvarchar(max)", nullable: true), // Tilføjer en kolonne til at lagre navnet på produktet
                    Pris = table.Column<double>(type: "float", nullable: true), // Tilføjer en kolonne til at lagre prisen på produktet
                    Antal = table.Column<int>(type: "int", nullable: true) // Tilføjer en kolonne til at lagre antallet af produktet
                },
                constraints: table =>
                {
                    // Definerer primærnøglen for tabellen som "ProduktId"
                    table.PrimaryKey("PK_Produkter", x => x.ProduktId);

                    // Opretter en fremmednøgle-relation mellem "Produkter" og "Ordrer"-tabellen baseret på "OrdreId"
                    table.ForeignKey(
                        name: "FK_Produkter_Ordrer_OrdreId",
                        column: x => x.OrdreId,
                        principalTable: "Ordrer",
                        principalColumn: "OrdreId",
                        onDelete: ReferentialAction.Cascade);
                });

            // Opretter et indeks for "OrdreId"-kolonnen i "Produkter"-tabellen for at optimere forespørgsler
            migrationBuilder.CreateIndex(
                name: "IX_Produkter_OrdreId",
                table: "Produkter",
                column: "OrdreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Sletter "Produkter"-tabellen fra databasen
            migrationBuilder.DropTable(
                name: "Produkter");
        }
    }
}


// Denne migration opretter en ny tabel, "Produkter", som indeholder information om produkter, der er tilknyttet en ordre via "OrdreId".
// Tabellen inkluderer også felter til at gemme produktnavn, pris og antal.
// Migrationen sikrer referentiel integritet mellem "Produkter" og "Ordrer" ved at definere en fremmednøgle.
// I Down-metoden fjernes tabellen igen, hvis migrationen rulles tilbage.