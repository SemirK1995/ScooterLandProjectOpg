using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Opretter tabellen "Kunder" med de definerede kolonner og primærnøgle.
            migrationBuilder.CreateTable(
                name: "Kunder",
                columns: table => new
                {
                    // Definerer kolonnen `KundeId` som en integer, der er primærnøgle og auto-inkrementeres.
                    KundeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    
                    // Definerer kolonnen `Navn` som en string, der ikke må være null.
                    Navn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   
                    // Definerer kolonnen `Adresse` som en string, der ikke må være null.
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   
                    // Definerer kolonnen `Telefonnummer` som en integer, der ikke må være null.
                    Telefonnummer = table.Column<int>(type: "int", nullable: false),
                   
                    // Definerer kolonnen `Email` som en string, der ikke må være null.
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   
                    // Definerer kolonnen `ScooterMaerke` som en string, der ikke må være null.
                    ScooterMaerke = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunder", x => x.KundeId); // Angiver `KundeId` som primærnøgle for tabellen "Kunder".
                });

            // Opretter tabellen "Mekanikere" med de definerede kolonner og primærnøgle.
            migrationBuilder.CreateTable(
                name: "Mekanikere",
                columns: table => new
                {
                    // Definerer kolonnen `MekanikerId` som en integer, der er primærnøgle og auto-inkrementeres.
                    MekanikerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    // Definerer kolonnen `Navn` som en string, der kan være null.
                    Navn = table.Column<string>(type: "nvarchar(max)", nullable: true),

                    // Definerer kolonnen `Telefonnummer` som en string, der kan være null.
                    Telefonnummer = table.Column<string>(type: "nvarchar(max)", nullable: true),

                    // Definerer kolonnen `Speciale` som en string, der kan være null.
                    Speciale = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mekanikere", x => x.MekanikerId); // Angiver `MekanikerId` som primærnøgle for tabellen "Mekanikere".
                });

            // Opretter tabellen "Ydelser" med de definerede kolonner og primærnøgle.
            migrationBuilder.CreateTable(
                name: "Ydelser",
                columns: table => new
                {
                    // Definerer kolonnen `YdelseId` som en integer, der er primærnøgle og auto-inkrementeres.
                    YdelseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    // Definerer kolonnen `Navn` som en string, der kan være null.
                    Navn = table.Column<string>(type: "nvarchar(max)", nullable: true),

                    // Definerer kolonnen `StandardPris` som en float, der kan være null.
                    StandardPris = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ydelser", x => x.YdelseId); // Angiver `YdelseId` som primærnøgle for tabellen "Ydelser".
                });

            // Opretter tabellen "KunderScootere" med de definerede kolonner og relationer.
            migrationBuilder.CreateTable(
                name: "KunderScootere",
                columns: table => new
                {
                    // Definerer kolonnen `ScooterId` som primærnøglen for tabellen og gør den auto-inkrementerende.
                    ScooterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    // Definerer kolonnen `KundeId` som en fremmednøgle, der ikke kan være null.
                    KundeId = table.Column<int>(type: "int", nullable: false),

                    // Definerer kolonnen `Maerke` som en string, der kan være null og repræsenterer scooterens mærke.
                    Maerke = table.Column<string>(type: "nvarchar(max)", nullable: true),

                    // Definerer kolonnen `Model` som en string, der kan være null og repræsenterer scooterens model.
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    // Angiver `ScooterId` som primærnøglen for tabellen.
                    table.PrimaryKey("PK_KunderScootere", x => x.ScooterId);

                    // Opretter en fremmednøgle til tabellen "Kunder" gennem `KundeId`.
                    table.ForeignKey(
                        name: "FK_KunderScootere_Kunder_KundeId", 
                        column: x => x.KundeId, // Fremmednøglekolonnen i denne tabel.
                        principalTable: "Kunder", // Refererer til tabellen "Kunder".
                        principalColumn: "KundeId", // Refererer til kolonnen "KundeId" i tabellen "Kunder".
                        onDelete: ReferentialAction.Cascade); // Sletter tilknyttede rækker, hvis kunden slettes.
                });

            // Opretter tabellen "LejeAftaler" med de definerede kolonner og relationer.
            migrationBuilder.CreateTable(
                name: "LejeAftaler",
                columns: table => new
                {
                    // Definerer kolonnen `LejeId` som primærnøglen for tabellen og gør den auto-inkrementerende.
                    LejeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    // Definerer kolonnen `KundeId` som en fremmednøgle, der ikke kan være null.
                    KundeId = table.Column<int>(type: "int", nullable: false),

                    // Definerer kolonnen `StartDato` som en nullable datetime, der repræsenterer startdatoen for lejeaftalen.
                    StartDato = table.Column<DateTime>(type: "datetime2", nullable: true),

                    // Definerer kolonnen `SlutDato` som en nullable datetime, der repræsenterer slutdatoen for lejeaftalen.
                    SlutDato = table.Column<DateTime>(type: "datetime2", nullable: true),

                    // Definerer kolonnen `DagligLeje` som en nullable float, der repræsenterer prisen per dag.
                    DagligLeje = table.Column<double>(type: "float", nullable: true),

                    // Definerer kolonnen `Forsikring` som en nullable boolean, der angiver, om forsikring er inkluderet.
                    Forsikring = table.Column<bool>(type: "bit", nullable: true),

                    // Definerer kolonnen `KilometerPris` som en nullable float, der repræsenterer prisen per kilometer.
                    KilometerPris = table.Column<double>(type: "float", nullable: true),

                    // Definerer kolonnen `Selvrisiko` som en nullable float, der repræsenterer selvrisikobeløbet.
                    Selvrisiko = table.Column<double>(type: "float", nullable: true),

                    // Definerer kolonnen `KortKilometer` som en nullable integer, der repræsenterer inkluderede kilometer.
                    KortKilometer = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    // Angiver `LejeId` som primærnøglen for tabellen.
                    table.PrimaryKey("PK_LejeAftaler", x => x.LejeId);

                    // Opretter en fremmednøgle til tabellen "Kunder" gennem `KundeId`.
                    table.ForeignKey(
                        name: "FK_LejeAftaler_Kunder_KundeId", 
                        column: x => x.KundeId, // Fremmednøglekolonnen i denne tabel.
                        principalTable: "Kunder", // Refererer til tabellen "Kunder".
                        principalColumn: "KundeId", // Refererer til kolonnen "KundeId" i tabellen "Kunder".
                        onDelete: ReferentialAction.Cascade); // Sletter tilknyttede rækker, hvis kunden slettes.
                });

            // Opretter tabellen "Ordrer" med de definerede kolonner og relationer.
            migrationBuilder.CreateTable(
                name: "Ordrer",
                columns: table => new
                {
                    // Definerer kolonnen `OrdreId` som primærnøglen for tabellen og gør den auto-inkrementerende.
                    OrdreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    // Definerer kolonnen `KundeId` som en fremmednøgle, der ikke kan være null.
                    KundeId = table.Column<int>(type: "int", nullable: false),

                    // Definerer kolonnen `Dato` som en nullable datetime, der repræsenterer datoen for ordren.
                    Dato = table.Column<DateTime>(type: "datetime2", nullable: true),

                    // Definerer kolonnen `TotalPris` som en nullable float, der repræsenterer den samlede pris for ordren.
                    TotalPris = table.Column<double>(type: "float", nullable: true),

                    // Definerer kolonnen `Status` som en nullable integer, der repræsenterer status for ordren.
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    // Angiver `OrdreId` som primærnøglen for tabellen.
                    table.PrimaryKey("PK_Ordrer", x => x.OrdreId);

                    // Opretter en fremmednøgle til tabellen "Kunder" gennem `KundeId`.
                    table.ForeignKey(
                        name: "FK_Ordrer_Kunder_KundeId",
                        column: x => x.KundeId, // Fremmednøglekolonnen i denne tabel.
                        principalTable: "Kunder", // Refererer til tabellen "Kunder".
                        principalColumn: "KundeId", // Refererer til kolonnen "KundeId" i tabellen "Kunder".
                        onDelete: ReferentialAction.Cascade); // Sletter tilknyttede rækker, hvis kunden slettes.
                });

            // Opretter tabellen "MekanikerYdelser" med de definerede kolonner og relationer.
            migrationBuilder.CreateTable(
                name: "MekanikerYdelser",
                columns: table => new
                {
                    // Definerer kolonnen `MekanikerYdelseId` som primærnøglen for tabellen og gør den auto-inkrementerende.
                    MekanikerYdelseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    // Definerer kolonnen `MekanikerId` som en fremmednøgle, der ikke kan være null.
                    MekanikerId = table.Column<int>(type: "int", nullable: false),

                    // Definerer kolonnen `YdelseId` som en fremmednøgle, der ikke kan være null.
                    YdelseId = table.Column<int>(type: "int", nullable: false),

                    // Definerer kolonnen `StartDato` som en nullable datetime, der repræsenterer startdatoen for ydelsen.
                    StartDato = table.Column<DateTime>(type: "datetime2", nullable: true),

                    // Definerer kolonnen `SlutDato` som en nullable datetime, der repræsenterer slutdatoen for ydelsen.
                    SlutDato = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    // Angiver `MekanikerYdelseId` som primærnøglen for tabellen.
                    table.PrimaryKey("PK_MekanikerYdelser", x => x.MekanikerYdelseId);

                    // Opretter en fremmednøgle til tabellen "Mekanikere" gennem `MekanikerId`.
                    table.ForeignKey(
                        name: "FK_MekanikerYdelser_Mekanikere_MekanikerId",
                        column: x => x.MekanikerId, // Fremmednøglekolonnen i denne tabel.
                        principalTable: "Mekanikere", // Refererer til tabellen "Mekanikere".
                        principalColumn: "MekanikerId", // Refererer til kolonnen "MekanikerId" i tabellen "Mekanikere".
                        onDelete: ReferentialAction.Cascade); // Sletter tilknyttede rækker, hvis mekanikeren slettes.

                    // Opretter en fremmednøgle til tabellen "Ydelser" gennem `YdelseId`.
                    table.ForeignKey(
                        name: "FK_MekanikerYdelser_Ydelser_YdelseId",
                        column: x => x.YdelseId, // Fremmednøglekolonnen i denne tabel.
                        principalTable: "Ydelser", // Refererer til tabellen "Ydelser".
                        principalColumn: "YdelseId", // Refererer til kolonnen "YdelseId" i tabellen "Ydelser".
                        onDelete: ReferentialAction.Cascade); // Sletter tilknyttede rækker, hvis ydelsen slettes.
                });

            // Opretter tabellen "LejeScootere" med de definerede kolonner og relationer.
            migrationBuilder.CreateTable(
                name: "LejeScootere",
                columns: table => new
                {
                    // Definerer kolonnen `LejeScooterId` som primærnøglen for tabellen og gør den auto-inkrementerende.
                    LejeScooterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    // Definerer kolonnen `LejeId` som en fremmednøgle, der ikke kan være null.
                    LejeId = table.Column<int>(type: "int", nullable: false),

                    // Definerer kolonnen `ScooterModel` som en nullable string, der repræsenterer scooterens model.
                    ScooterModel = table.Column<string>(type: "nvarchar(max)", nullable: true),

                    // Definerer kolonnen `ScooterMaerke` som en nullable string, der repræsenterer scooterens mærke.
                    ScooterMaerke = table.Column<string>(type: "nvarchar(max)", nullable: true),

                    // Definerer kolonnen `StartDato` som en nullable datetime, der repræsenterer startdatoen for lejeperioden.
                    StartDato = table.Column<DateTime>(type: "datetime2", nullable: true),

                    // Definerer kolonnen `SlutDato` som en nullable datetime, der repræsenterer slutdatoen for lejeperioden.
                    SlutDato = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    // Angiver `LejeScooterId` som primærnøglen for tabellen.
                    table.PrimaryKey("PK_LejeScootere", x => x.LejeScooterId);

                    // Opretter en fremmednøgle til tabellen "LejeAftaler" gennem `LejeId`.
                    table.ForeignKey(
                        name: "FK_LejeScootere_LejeAftaler_LejeId",
                        column: x => x.LejeId, // Fremmednøglekolonnen i denne tabel.
                        principalTable: "LejeAftaler", // Refererer til tabellen "LejeAftaler".
                        principalColumn: "LejeId", // Refererer til kolonnen "LejeId" i tabellen "LejeAftaler".
                        onDelete: ReferentialAction.Cascade); // Sletter tilknyttede rækker, hvis lejeaftalen slettes.
                });

            // Opretter tabellen "Betalinger" med de definerede kolonner og relationer.
            migrationBuilder.CreateTable(
                name: "Betalinger",
                columns: table => new
                {
                    // Definerer kolonnen `BetalingsId` som primærnøglen for tabellen og gør den auto-inkrementerende.
                    BetalingsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    // Definerer kolonnen `OrdreId` som en fremmednøgle, der ikke kan være null.
                    OrdreId = table.Column<int>(type: "int", nullable: false),

                    // Definerer kolonnen `BetalingsDato` som en nullable datetime, der repræsenterer betalingsdatoen.
                    BetalingsDato = table.Column<DateTime>(type: "datetime2", nullable: true),

                    // Definerer kolonnen `Beløb` som en nullable float, der repræsenterer det betalte beløb.
                    Beløb = table.Column<double>(type: "float", nullable: true),

                    // Definerer kolonnen `BetalingsMetode` som en integer, der repræsenterer betalingsmetoden.
                    BetalingsMetode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    // Angiver `BetalingsId` som primærnøglen for tabellen.
                    table.PrimaryKey("PK_Betalinger", x => x.BetalingsId);

                    // Opretter en fremmednøgle til tabellen "Ordrer" gennem `OrdreId`.
                    table.ForeignKey(
                        name: "FK_Betalinger_Ordrer_OrdreId",
                        column: x => x.OrdreId, // Fremmednøglekolonnen i denne tabel.
                        principalTable: "Ordrer", // Refererer til tabellen "Ordrer".
                        principalColumn: "OrdreId", // Refererer til kolonnen "OrdreId" i tabellen "Ordrer".
                        onDelete: ReferentialAction.Cascade); // Sletter tilknyttede rækker, hvis ordren slettes.
                });

            // Opretter tabellen "OrdreYdelser" med de definerede kolonner og relationer.
            migrationBuilder.CreateTable(
                name: "OrdreYdelser",
                columns: table => new
                {
                    // Definerer kolonnen `OrdreYdelseId` som primærnøglen for tabellen og gør den auto-inkrementerende.
                    OrdreYdelseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    // Definerer kolonnen `OrdreId` som en fremmednøgle, der ikke kan være null.
                    OrdreId = table.Column<int>(type: "int", nullable: false),

                    // Definerer kolonnen `YdelseId` som en fremmednøgle, der ikke kan være null.
                    YdelseId = table.Column<int>(type: "int", nullable: false),

                    // Definerer kolonnen `Timer` som en nullable float, der repræsenterer det antal timer, der er brugt på ydelsen.
                    Timer = table.Column<double>(type: "float", nullable: true),

                    // Definerer kolonnen `Dato` som en nullable datetime, der repræsenterer datoen for ydelsen.
                    Dato = table.Column<DateTime>(type: "datetime2", nullable: true),

                    // Definerer kolonnen `AftaltPris` som en nullable float, der repræsenterer den aftalte pris for ydelsen.
                    AftaltPris = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    // Angiver `OrdreYdelseId` som primærnøglen for tabellen.
                    table.PrimaryKey("PK_OrdreYdelser", x => x.OrdreYdelseId);

                    // Opretter en fremmednøgle til tabellen "Ordrer" gennem `OrdreId`.
                    table.ForeignKey(
                        name: "FK_OrdreYdelser_Ordrer_OrdreId",
                        column: x => x.OrdreId, // Fremmednøglekolonnen i denne tabel.
                        principalTable: "Ordrer", // Refererer til tabellen "Ordrer".
                        principalColumn: "OrdreId", // Refererer til kolonnen "OrdreId" i tabellen "Ordrer".
                        onDelete: ReferentialAction.Cascade); // Sletter tilknyttede rækker, hvis ordren slettes.

                    // Opretter en fremmednøgle til tabellen "Ydelser" gennem `YdelseId`.
                    table.ForeignKey(
                        name: "FK_OrdreYdelser_Ydelser_YdelseId",
                        column: x => x.YdelseId, // Fremmednøglekolonnen i denne tabel.
                        principalTable: "Ydelser", // Refererer til tabellen "Ydelser".
                        principalColumn: "YdelseId", // Refererer til kolonnen "YdelseId" i tabellen "Ydelser".
                        onDelete: ReferentialAction.Cascade); // Sletter tilknyttede rækker, hvis ydelsen slettes.
                });

            // Opretter et indeks på kolonnen `OrdreId` i tabellen "Betalinger" for at optimere forespørgsler, der filtrerer eller sorterer baseret på denne kolonne.
            migrationBuilder.CreateIndex(
                name: "IX_Betalinger_OrdreId",
                table: "Betalinger",
                column: "OrdreId");

            // Opretter et indeks på kolonnen `KundeId` i tabellen "KunderScootere" for at forbedre ydeevnen ved forespørgsler relateret til kundens scootere.
            migrationBuilder.CreateIndex(
                name: "IX_KunderScootere_KundeId",
                table: "KunderScootere",
                column: "KundeId");

            // Opretter et indeks på kolonnen `KundeId` i tabellen "LejeAftaler" for at optimere forespørgsler, der knytter en kunde til deres lejeaftaler.
            migrationBuilder.CreateIndex(
                name: "IX_LejeAftaler_KundeId",
                table: "LejeAftaler",
                column: "KundeId");

            // Opretter et indeks på kolonnen `LejeId` i tabellen "LejeScootere" for at forbedre ydeevnen ved forespørgsler, der relaterer lejescootere til deres lejeaftaler.
            migrationBuilder.CreateIndex(
                name: "IX_LejeScootere_LejeId",
                table: "LejeScootere",
                column: "LejeId");

            // Opretter et indeks på kolonnen `MekanikerId` i tabellen "MekanikerYdelser" for at optimere forespørgsler, der knytter mekanikere til deres ydelser.
            migrationBuilder.CreateIndex(
                name: "IX_MekanikerYdelser_MekanikerId",
                table: "MekanikerYdelser",
                column: "MekanikerId");

            // Opretter et indeks på kolonnen `YdelseId` i tabellen "MekanikerYdelser" for at forbedre ydeevnen ved forespørgsler relateret til ydelser, som mekanikere arbejder på.
            migrationBuilder.CreateIndex(
                name: "IX_MekanikerYdelser_YdelseId",
                table: "MekanikerYdelser",
                column: "YdelseId");

            // Opretter et indeks på kolonnen `KundeId` i tabellen "Ordrer" for at optimere forespørgsler, der knytter kunder til deres ordrer.
            migrationBuilder.CreateIndex(
                name: "IX_Ordrer_KundeId",
                table: "Ordrer",
                column: "KundeId");

            // Opretter et indeks på kolonnen `OrdreId` i tabellen "OrdreYdelser" for at forbedre ydeevnen ved forespørgsler, der relaterer ordreydelser til deres ordrer.
            migrationBuilder.CreateIndex(
                name: "IX_OrdreYdelser_OrdreId",
                table: "OrdreYdelser",
                column: "OrdreId");

            // Opretter et indeks på kolonnen `YdelseId` i tabellen "OrdreYdelser" for at optimere forespørgsler, der knytter ydelser til deres ordrer.
            migrationBuilder.CreateIndex(
                name: "IX_OrdreYdelser_YdelseId",
                table: "OrdreYdelser",
                column: "YdelseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Sletter tabellen "Betalinger" fra databasen, hvis denne migration rulles tilbage.
            migrationBuilder.DropTable(
                name: "Betalinger");

            // Sletter tabellen "KunderScootere" fra databasen, hvis denne migration rulles tilbage.
            migrationBuilder.DropTable(
                name: "KunderScootere");

            // Sletter tabellen "LejeScootere" fra databasen, hvis denne migration rulles tilbage.
            migrationBuilder.DropTable(
                name: "LejeScootere");

            // Sletter tabellen "MekanikerYdelser" fra databasen, hvis denne migration rulles tilbage.
            migrationBuilder.DropTable(
                name: "MekanikerYdelser");

            // Sletter tabellen "OrdreYdelser" fra databasen, hvis denne migration rulles tilbage.
            migrationBuilder.DropTable(
                name: "OrdreYdelser");

            // Sletter tabellen "LejeAftaler" fra databasen, hvis denne migration rulles tilbage.
            migrationBuilder.DropTable(
                name: "LejeAftaler");

            // Sletter tabellen "Mekanikere" fra databasen, hvis denne migration rulles tilbage.
            migrationBuilder.DropTable(
                name: "Mekanikere");

            // Sletter tabellen "Ordrer" fra databasen, hvis denne migration rulles tilbage.
            migrationBuilder.DropTable(
                name: "Ordrer");

            // Sletter tabellen "Ydelser" fra databasen, hvis denne migration rulles tilbage.
            migrationBuilder.DropTable(
                name: "Ydelser");

            // Sletter tabellen "Kunder" fra databasen, hvis denne migration rulles tilbage.
            migrationBuilder.DropTable(
                name: "Kunder");
        }
    }
}