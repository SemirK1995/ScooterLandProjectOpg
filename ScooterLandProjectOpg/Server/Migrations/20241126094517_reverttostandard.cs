using Microsoft.EntityFrameworkCore.Migrations; // Importerer namespace for at arbejde med database-migrationer.

#nullable disable // Deaktiverer nullable-analyse for at opretholde kompatibilitet med tidligere kode.

namespace ScooterLandProjectOpg.Server.Migrations // Definerer det namespace, som migrationen hører til, for at strukturere projektet korrekt.
{
    /// <inheritdoc />
    public partial class reverttostandard : Migration // Definerer en migration, der ændrer strukturen af databasen.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // Metode, der beskriver ændringer, som skal anvendes på databasen.
        {
            migrationBuilder.AlterColumn<string>( // Ændrer datatypen for en kolonne i tabellen.
                name: "ScooterModel", // Navnet på kolonnen, der ændres.
                table: "LejeScootere", // Tabellen, hvor kolonnen findes.
                type: "nvarchar(max)", // Angiver den nye datatype som en variabel længde tekst.
                nullable: false, // Angiver, at kolonnen ikke længere kan indeholde null-værdier.
                defaultValue: "", // Angiver en standardværdi for kolonnen.
                oldClrType: typeof(string), // Den tidligere datatype for kolonnen.
                oldType: "nvarchar(max)", // Den tidligere SQL-datatype for kolonnen.
                oldNullable: true); // Angiver, at kolonnen tidligere kunne være null.

            migrationBuilder.AlterColumn<string>( // Ændrer datatypen for en anden kolonne i tabellen.
                name: "ScooterMaerke", // Navnet på kolonnen, der ændres.
                table: "LejeScootere", // Tabellen, hvor kolonnen findes.
                type: "nvarchar(max)", // Angiver den nye datatype som en variabel længde tekst.
                nullable: false, // Angiver, at kolonnen ikke længere kan indeholde null-værdier.
                defaultValue: "", // Angiver en standardværdi for kolonnen.
                oldClrType: typeof(string), // Den tidligere datatype for kolonnen.
                oldType: "nvarchar(max)", // Den tidligere SQL-datatype for kolonnen.
                oldNullable: true); // Angiver, at kolonnen tidligere kunne være null.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // Metode, der beskriver, hvordan ændringerne kan fortrydes.
        {
            migrationBuilder.AlterColumn<string>( // Gendanner datatypen for kolonnen til dens tidligere tilstand.
                name: "ScooterModel", // Navnet på kolonnen, der gendannes.
                table: "LejeScootere", // Tabellen, hvor kolonnen findes.
                type: "nvarchar(max)", // Angiver den tidligere SQL-datatype som en variabel længde tekst.
                nullable: true, // Angiver, at kolonnen igen kan indeholde null-værdier.
                oldClrType: typeof(string), // Den nuværende datatype for kolonnen, der ændres.
                oldType: "nvarchar(max)"); // Den nuværende SQL-datatype for kolonnen.

            migrationBuilder.AlterColumn<string>( // Gendanner datatypen for en anden kolonne til dens tidligere tilstand.
                name: "ScooterMaerke", // Navnet på kolonnen, der gendannes.
                table: "LejeScootere", // Tabellen, hvor kolonnen findes.
                type: "nvarchar(max)", // Angiver den tidligere SQL-datatype som en variabel længde tekst.
                nullable: true, // Angiver, at kolonnen igen kan indeholde null-værdier.
                oldClrType: typeof(string), // Den nuværende datatype for kolonnen, der ændres.
                oldType: "nvarchar(max)"); // Den nuværende SQL-datatype for kolonnen.
        }
    }
}


// Denne migration ændrer kravene til to kolonner i tabellen LejeScootere, nemlig ScooterModel og ScooterMaerke.
// I Up-metoden opdateres kolonnerne, så de ikke længere kan indeholde null-værdier.
// Standardværdien for begge kolonner sættes til en tom streng (""), og dette sikrer, at der altid er data tilgængeligt i disse felter.

// I Down-metoden fortrydes ændringerne.
// Kolonnerne ændres tilbage til deres tidligere tilstand, hvor de igen kan indeholde null-værdier, og standardværdien fjernes.
// Disse ændringer giver fleksibilitet til at håndtere scenarier, hvor data i disse felter ikke nødvendigvis er påkrævet.