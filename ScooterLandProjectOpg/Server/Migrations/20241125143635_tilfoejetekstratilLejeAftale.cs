using System; // Importerer System-namespace for at kunne bruge DateTime-typen.
using Microsoft.EntityFrameworkCore.Migrations; // Importerer funktionalitet til at håndtere database-migrationer.

#nullable disable // Deaktiverer nullable-analyse for at sikre kompatibilitet med eksisterende kode.

namespace ScooterLandProjectOpg.Server.Migrations // Definerer det namespace, som migrationen tilhører, for projektstruktur.
{
    /// <inheritdoc />
    public partial class tilfoejetekstratilLejeAftale : Migration // Definerer en migration, der opdaterer strukturen i tabellen "LejeAftaler".
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // Metode, der beskriver ændringerne, der skal anvendes på databasen.
        {
            migrationBuilder.AlterColumn<DateTime>( // Ændrer kolonnen "StartDato" i tabellen "LejeAftaler".
                name: "StartDato", // Navnet på kolonnen, der ændres.
                table: "LejeAftaler", // Tabellen, hvor ændringen finder sted.
                type: "datetime2", // Sætter datatypen til datetime2 i SQL.
                nullable: false, // Angiver, at kolonnen ikke længere kan være null.
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), // Sætter en standardværdi.
                oldClrType: typeof(DateTime), // Tidligere datatype i C#.
                oldType: "datetime2", // Tidligere datatype i SQL.
                oldNullable: true); // Indikerer, at kolonnen tidligere kunne være null.

            migrationBuilder.AlterColumn<DateTime>( // Ændrer kolonnen "SlutDato" i tabellen "LejeAftaler".
                name: "SlutDato", 
                table: "LejeAftaler", 
                type: "datetime2", 
                nullable: false, 
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 
                oldClrType: typeof(DateTime), 
                oldType: "datetime2", 
                oldNullable: true);

            migrationBuilder.AlterColumn<double>( // Ændrer kolonnen "Selvrisiko" i tabellen "LejeAftaler".
                name: "Selvrisiko", 
                table: "LejeAftaler", 
                type: "float", // Angiver, at datatypen er float i SQL.
                nullable: false, 
                defaultValue: 0.0, // Sætter standardværdien til 0.0.
                oldClrType: typeof(double), 
                oldType: "float", 
                oldNullable: true);

            migrationBuilder.AlterColumn<double>( // Ændrer kolonnen "KilometerPris" i tabellen "LejeAftaler".
                name: "KilometerPris", 
                table: "LejeAftaler", 
                type: "float", 
                nullable: false, 
                defaultValue: 0.0, 
                oldClrType: typeof(double), 
                oldType: "float", 
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>( // Ændrer kolonnen "Forsikring" i tabellen "LejeAftaler".
                name: "Forsikring", 
                table: "LejeAftaler", 
                type: "bit", // Sætter datatypen til bit for boolean-værdier i SQL.
                nullable: false, 
                defaultValue: false, // Angiver standardværdien som false.
                oldClrType: typeof(bool), 
                oldType: "bit", 
                oldNullable: true);

            migrationBuilder.AlterColumn<double>( // Ændrer kolonnen "DagligLeje" i tabellen "LejeAftaler".
                name: "DagligLeje", 
                table: "LejeAftaler", 
                type: "float", 
                nullable: false, 
                defaultValue: 0.0, 
                oldClrType: typeof(double), 
                oldType: "float", 
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // Metode, der ruller ændringerne tilbage, hvis migrationen fortrydes.
        {
            migrationBuilder.AlterColumn<DateTime>( // Gendanner kolonnen "StartDato" til dens tidligere tilstand.
                name: "StartDato", 
                table: "LejeAftaler", 
                type: "datetime2", 
                nullable: true, 
                oldClrType: typeof(DateTime), 
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>( // Gendanner kolonnen "SlutDato" til dens tidligere tilstand.
                name: "SlutDato", 
                table: "LejeAftaler", 
                type: "datetime2", 
                nullable: true, 
                oldClrType: typeof(DateTime), 
                oldType: "datetime2");

            migrationBuilder.AlterColumn<double>( // Gendanner kolonnen "Selvrisiko" til dens tidligere tilstand.
                name: "Selvrisiko", 
                table: "LejeAftaler", 
                type: "float", 
                nullable: true, 
                oldClrType: typeof(double), 
                oldType: "float");

            migrationBuilder.AlterColumn<double>( // Gendanner kolonnen "KilometerPris" til dens tidligere tilstand.
                name: "KilometerPris", 
                table: "LejeAftaler", 
                type: "float", 
                nullable: true, 
                oldClrType: typeof(double), 
                oldType: "float");

            migrationBuilder.AlterColumn<bool>( // Gendanner kolonnen "Forsikring" til dens tidligere tilstand.
                name: "Forsikring", 
                table: "LejeAftaler", 
                type: "bit", 
                nullable: true, 
                oldClrType: typeof(bool), 
                oldType: "bit");

            migrationBuilder.AlterColumn<double>( // Gendanner kolonnen "DagligLeje" til dens tidligere tilstand.
                name: "DagligLeje", 
                table: "LejeAftaler", 
                type: "float", 
                nullable: true, 
                oldClrType: typeof(double), 
                oldType: "float");
        }
    }
}


// Denne migration foretager ændringer i tabellen LejeAftaler ved at ændre flere kolonner for at sikre, at de ikke tillader null-værdier og har standardværdier.
// I Up-metoden ændres kolonnerne StartDato, SlutDato, Selvrisiko, KilometerPris, Forsikring og DagligLeje til at være påkrævede
// felter med passende standardværdier, hvilket forbedrer dataintegriteten og sikrer, at disse felter altid har en gyldig værdi.
// F.eks. sættes StartDato og SlutDato til at have en standardværdi på en udefineret dato, mens Selvrisiko, KilometerPris og DagligLeje får standardværdien 0.0, og Forsikring får standardværdien false.

// I Down-metoden rulles disse ændringer tilbage ved at gendanne kolonnerne til deres oprindelige tilstand, hvor de tillader null-værdier og ikke har standardværdier.
// Dette genskaber den tidligere fleksibilitet, men uden de strenge krav til dataintegritet, som blev introduceret i Up-metoden.