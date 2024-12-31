using System; // Importerer systembiblioteket for at muliggøre brug af grundlæggende funktioner, som f.eks. datoer.
using Microsoft.EntityFrameworkCore.Migrations; // Importerer biblioteket for at kunne definere og anvende database-migrationer.

#nullable disable // Deaktiverer nullable-analyse for at sikre kompatibilitet med ældre kode.

namespace ScooterLandProjectOpg.Server.Migrations // Definerer namespace for migrationen, der organiserer det som en del af projektet.
{
    /// <inheritdoc />
    public partial class MekanikerYdelseEntitetFjernet : Migration // Definerer en migration, der fjerner entiteten "MekanikerYdelser" og tilføjer "MekanikerId" til "OrdreYdelser".
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // Metoden anvender ændringer på databasen, når migrationen køres.
        {
            migrationBuilder.DropTable( // Fjerner tabellen "MekanikerYdelser" fra databasen.
                name: "MekanikerYdelser"); // Angiver navnet på tabellen, der skal fjernes.

            migrationBuilder.AddColumn<int>( // Tilføjer en ny kolonne "MekanikerId" til tabellen "OrdreYdelser".
                name: "MekanikerId", // Navnet på den nye kolonne.
                table: "OrdreYdelser", // Tabellen, hvor kolonnen tilføjes.
                type: "int", // Datatypen for den nye kolonne.
                nullable: true); // Angiver, at kolonnen kan indeholde null-værdier.

            migrationBuilder.CreateIndex( // Opretter en indeks for den nye "MekanikerId"-kolonne.
                name: "IX_OrdreYdelser_MekanikerId", // Navnet på det nye indeks.
                table: "OrdreYdelser", // Tabellen, hvor indekset oprettes.
                column: "MekanikerId"); // Kolonnen, der indekseres.

            migrationBuilder.AddForeignKey( // Tilføjer en fremmednøgle-relation mellem "OrdreYdelser" og "Mekanikere".
                name: "FK_OrdreYdelser_Mekanikere_MekanikerId", // Navnet på fremmednøglen.
                table: "OrdreYdelser", // Tabellen, hvor fremmednøglen er defineret.
                column: "MekanikerId", // Kolonnen i "OrdreYdelser", der refererer til "Mekanikere".
                principalTable: "Mekanikere", // Tabellen, som fremmednøglen peger på.
                principalColumn: "MekanikerId"); // Den primære nøgle i "Mekanikere", der refereres til.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // Metoden ruller ændringerne tilbage, hvis migrationen fortrydes.
        {
            migrationBuilder.DropForeignKey( // Fjerner fremmednøgle-relationen mellem "OrdreYdelser" og "Mekanikere".
                name: "FK_OrdreYdelser_Mekanikere_MekanikerId", // Navnet på fremmednøglen.
                table: "OrdreYdelser"); // Tabellen, hvor fremmednøglen fjernes.

            migrationBuilder.DropIndex( // Fjerner indekset på "MekanikerId"-kolonnen i "OrdreYdelser".
                name: "IX_OrdreYdelser_MekanikerId", // Navnet på indekset, der fjernes.
                table: "OrdreYdelser"); // Tabellen, hvor indekset fjernes.

            migrationBuilder.DropColumn( // Fjerner "MekanikerId"-kolonnen fra "OrdreYdelser".
                name: "MekanikerId", // Navnet på kolonnen, der fjernes.
                table: "OrdreYdelser"); // Tabellen, hvor kolonnen fjernes.

            migrationBuilder.CreateTable( // Genskaber tabellen "MekanikerYdelser", som blev fjernet i Up-metoden.
                name: "MekanikerYdelser", // Navnet på tabellen, der oprettes igen.
                columns: table => new // Definerer kolonnerne i tabellen.
                {
                    MekanikerYdelseId = table.Column<int>(type: "int", nullable: false) // Opretter primærnøglen "MekanikerYdelseId".
                        .Annotation("SqlServer:Identity", "1, 1"), // Angiver, at kolonnen er en identity-kolonne, der autogenererer værdier.
                    MekanikerId = table.Column<int>(type: "int", nullable: false), // Opretter kolonnen "MekanikerId".
                    YdelseId = table.Column<int>(type: "int", nullable: false), // Opretter kolonnen "YdelseId".
                    SlutDato = table.Column<DateTime>(type: "datetime2", nullable: true), // Opretter kolonnen "SlutDato", som kan være null.
                    StartDato = table.Column<DateTime>(type: "datetime2", nullable: true) // Opretter kolonnen "StartDato", som kan være null.
                },
                constraints: table => // Definerer tabellens begrænsninger.
                {
                    table.PrimaryKey("PK_MekanikerYdelser", x => x.MekanikerYdelseId); // Angiver primærnøglen for tabellen.
                    table.ForeignKey( // Tilføjer en fremmednøgle-relation mellem "MekanikerYdelser" og "Mekanikere".
                        name: "FK_MekanikerYdelser_Mekanikere_MekanikerId", // Navnet på fremmednøglen.
                        column: x => x.MekanikerId, // Kolonnen i "MekanikerYdelser", der refererer til "Mekanikere".
                        principalTable: "Mekanikere", // Tabellen, som fremmednøglen peger på.
                        principalColumn: "MekanikerId", // Den primære nøgle i "Mekanikere", der refereres til.
                        onDelete: ReferentialAction.Cascade); // Angiver, at relaterede rækker skal slettes ved sletning i "Mekanikere".
                    table.ForeignKey( // Tilføjer en fremmednøgle-relation mellem "MekanikerYdelser" og "Ydelser".
                        name: "FK_MekanikerYdelser_Ydelser_YdelseId", // Navnet på fremmednøglen.
                        column: x => x.YdelseId, // Kolonnen i "MekanikerYdelser", der refererer til "Ydelser".
                        principalTable: "Ydelser", // Tabellen, som fremmednøglen peger på.
                        principalColumn: "YdelseId", // Den primære nøgle i "Ydelser", der refereres til.
                        onDelete: ReferentialAction.Cascade); // Angiver, at relaterede rækker skal slettes ved sletning i "Ydelser".
                });

            migrationBuilder.CreateIndex( // Genskaber indekset på "MekanikerId"-kolonnen i "MekanikerYdelser".
                name: "IX_MekanikerYdelser_MekanikerId", // Navnet på det nye indeks.
                table: "MekanikerYdelser", // Tabellen, hvor indekset oprettes.
                column: "MekanikerId"); // Kolonnen, der indekseres.

            migrationBuilder.CreateIndex( // Genskaber indekset på "YdelseId"-kolonnen i "MekanikerYdelser".
                name: "IX_MekanikerYdelser_YdelseId", // Navnet på det nye indeks.
                table: "MekanikerYdelser", // Tabellen, hvor indekset oprettes.
                column: "YdelseId"); // Kolonnen, der indekseres.
        }
    }
}


// Denne migration fjerner tabellen MekanikerYdelser fra databasen og introducerer i stedet en ny kolonne MekanikerId i tabellen OrdreYdelser.
// Denne kolonne kan bruges til at oprette en relation mellem en ordreydelse og en mekaniker.
// I Up-metoden fjernes tabellen MekanikerYdelser, den nye MekanikerId-kolonne tilføjes til OrdreYdelser, og et indeks oprettes for at forbedre ydelsen ved opslag.
// Derudover tilføjes en fremmednøgle, der sikrer referentiel integritet mellem OrdreYdelser og Mekanikere.

// I Down-metoden genskabes tabellen MekanikerYdelser, og alle relaterede fremmednøgler og indekser genskabes.
// Samtidig fjernes MekanikerId-kolonnen fra OrdreYdelser, og relaterede indekser og fremmednøgler fjernes, for at rulle ændringerne tilbage til deres oprindelige tilstand.