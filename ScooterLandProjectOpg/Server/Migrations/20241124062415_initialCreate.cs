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
            migrationBuilder.CreateTable(
                name: "Kunder",
                columns: table => new
                {
                    KundeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Navn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefonnummer = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScooterMaerke = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunder", x => x.KundeId);
                });

            migrationBuilder.CreateTable(
                name: "Mekanikere",
                columns: table => new
                {
                    MekanikerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Navn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefonnummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Speciale = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mekanikere", x => x.MekanikerId);
                });

            migrationBuilder.CreateTable(
                name: "Ydelser",
                columns: table => new
                {
                    YdelseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Navn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StandardPris = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ydelser", x => x.YdelseId);
                });

            migrationBuilder.CreateTable(
                name: "KunderScootere",
                columns: table => new
                {
                    ScooterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KundeId = table.Column<int>(type: "int", nullable: false),
                    Maerke = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KunderScootere", x => x.ScooterId);
                    table.ForeignKey(
                        name: "FK_KunderScootere_Kunder_KundeId",
                        column: x => x.KundeId,
                        principalTable: "Kunder",
                        principalColumn: "KundeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LejeAftaler",
                columns: table => new
                {
                    LejeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KundeId = table.Column<int>(type: "int", nullable: false),
                    StartDato = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SlutDato = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DagligLeje = table.Column<double>(type: "float", nullable: true),
                    Forsikring = table.Column<bool>(type: "bit", nullable: true),
                    KilometerPris = table.Column<double>(type: "float", nullable: true),
                    Selvrisiko = table.Column<double>(type: "float", nullable: true),
                    KortKilometer = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LejeAftaler", x => x.LejeId);
                    table.ForeignKey(
                        name: "FK_LejeAftaler_Kunder_KundeId",
                        column: x => x.KundeId,
                        principalTable: "Kunder",
                        principalColumn: "KundeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ordrer",
                columns: table => new
                {
                    OrdreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KundeId = table.Column<int>(type: "int", nullable: false),
                    Dato = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalPris = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordrer", x => x.OrdreId);
                    table.ForeignKey(
                        name: "FK_Ordrer_Kunder_KundeId",
                        column: x => x.KundeId,
                        principalTable: "Kunder",
                        principalColumn: "KundeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MekanikerYdelser",
                columns: table => new
                {
                    MekanikerYdelseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MekanikerId = table.Column<int>(type: "int", nullable: false),
                    YdelseId = table.Column<int>(type: "int", nullable: false),
                    StartDato = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SlutDato = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MekanikerYdelser", x => x.MekanikerYdelseId);
                    table.ForeignKey(
                        name: "FK_MekanikerYdelser_Mekanikere_MekanikerId",
                        column: x => x.MekanikerId,
                        principalTable: "Mekanikere",
                        principalColumn: "MekanikerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MekanikerYdelser_Ydelser_YdelseId",
                        column: x => x.YdelseId,
                        principalTable: "Ydelser",
                        principalColumn: "YdelseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LejeScootere",
                columns: table => new
                {
                    LejeScooterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LejeId = table.Column<int>(type: "int", nullable: false),
                    ScooterModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScooterMaerke = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDato = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SlutDato = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LejeScootere", x => x.LejeScooterId);
                    table.ForeignKey(
                        name: "FK_LejeScootere_LejeAftaler_LejeId",
                        column: x => x.LejeId,
                        principalTable: "LejeAftaler",
                        principalColumn: "LejeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Betalinger",
                columns: table => new
                {
                    BetalingsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdreId = table.Column<int>(type: "int", nullable: false),
                    BetalingsDato = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Beløb = table.Column<double>(type: "float", nullable: true),
                    BetalingsMetode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Betalinger", x => x.BetalingsId);
                    table.ForeignKey(
                        name: "FK_Betalinger_Ordrer_OrdreId",
                        column: x => x.OrdreId,
                        principalTable: "Ordrer",
                        principalColumn: "OrdreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdreYdelser",
                columns: table => new
                {
                    OrdreYdelseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdreId = table.Column<int>(type: "int", nullable: false),
                    YdelseId = table.Column<int>(type: "int", nullable: false),
                    Timer = table.Column<double>(type: "float", nullable: true),
                    Dato = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AftaltPris = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdreYdelser", x => x.OrdreYdelseId);
                    table.ForeignKey(
                        name: "FK_OrdreYdelser_Ordrer_OrdreId",
                        column: x => x.OrdreId,
                        principalTable: "Ordrer",
                        principalColumn: "OrdreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdreYdelser_Ydelser_YdelseId",
                        column: x => x.YdelseId,
                        principalTable: "Ydelser",
                        principalColumn: "YdelseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Betalinger_OrdreId",
                table: "Betalinger",
                column: "OrdreId");

            migrationBuilder.CreateIndex(
                name: "IX_KunderScootere_KundeId",
                table: "KunderScootere",
                column: "KundeId");

            migrationBuilder.CreateIndex(
                name: "IX_LejeAftaler_KundeId",
                table: "LejeAftaler",
                column: "KundeId");

            migrationBuilder.CreateIndex(
                name: "IX_LejeScootere_LejeId",
                table: "LejeScootere",
                column: "LejeId");

            migrationBuilder.CreateIndex(
                name: "IX_MekanikerYdelser_MekanikerId",
                table: "MekanikerYdelser",
                column: "MekanikerId");

            migrationBuilder.CreateIndex(
                name: "IX_MekanikerYdelser_YdelseId",
                table: "MekanikerYdelser",
                column: "YdelseId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordrer_KundeId",
                table: "Ordrer",
                column: "KundeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdreYdelser_OrdreId",
                table: "OrdreYdelser",
                column: "OrdreId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdreYdelser_YdelseId",
                table: "OrdreYdelser",
                column: "YdelseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Betalinger");

            migrationBuilder.DropTable(
                name: "KunderScootere");

            migrationBuilder.DropTable(
                name: "LejeScootere");

            migrationBuilder.DropTable(
                name: "MekanikerYdelser");

            migrationBuilder.DropTable(
                name: "OrdreYdelser");

            migrationBuilder.DropTable(
                name: "LejeAftaler");

            migrationBuilder.DropTable(
                name: "Mekanikere");

            migrationBuilder.DropTable(
                name: "Ordrer");

            migrationBuilder.DropTable(
                name: "Ydelser");

            migrationBuilder.DropTable(
                name: "Kunder");
        }
    }
}
