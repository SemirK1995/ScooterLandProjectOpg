using Microsoft.EntityFrameworkCore.Migrations; // Importerer biblioteket til database-migrationer.

#nullable disable // Deaktiverer nullable-analyse for at sikre kompatibilitet med ældre kode.

namespace ScooterLandProjectOpg.Server.Migrations // Definerer namespace for migrationen, så den er organiseret inden for projektet.
{
    /// <inheritdoc />
    public partial class IngenkravpåScooterMaerke : Migration // Definerer en migration til at ændre krav på kolonner i tabellerne.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // Denne metode indeholder ændringerne, der anvendes på databasen, når migrationen køres.
        {
            migrationBuilder.AlterColumn<string>( // Ændrer kolonnen "ScooterMaerke" i tabellen "Kunder".
                name: "ScooterMaerke", // Navnet på kolonnen, der ændres.
                table: "Kunder", // Tabellen, hvor kolonnen findes.
                type: "nvarchar(max)", // Definerer kolonnens datatype som en ubegrænset streng i SQL.
                nullable: true, // Tillader, at kolonnen kan have null-værdier.
                oldClrType: typeof(string), // Den tidligere datatype for kolonnen.
                oldType: "nvarchar(max)"); // Den tidligere SQL-datatype for kolonnen.

            migrationBuilder.AlterColumn<int>( // Ændrer kolonnen "BetalingsMetode" i tabellen "Betalinger".
                name: "BetalingsMetode", // Navnet på kolonnen, der ændres.
                table: "Betalinger", // Tabellen, hvor kolonnen findes.
                type: "int", // Definerer kolonnens datatype som integer i SQL.
                nullable: true, // Tillader, at kolonnen kan have null-værdier.
                oldClrType: typeof(int), // Den tidligere datatype for kolonnen.
                oldType: "int"); // Den tidligere SQL-datatype for kolonnen.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // Denne metode ruller ændringerne tilbage, hvis migrationen fortrydes.
        {
            migrationBuilder.AlterColumn<string>( // Gendanner kolonnen "ScooterMaerke" til dens tidligere tilstand.
                name: "ScooterMaerke", // Navnet på kolonnen, der ændres tilbage.
                table: "Kunder", // Tabellen, hvor kolonnen findes.
                type: "nvarchar(max)", // Angiver den tidligere datatype som en streng uden længdebegrænsning.
                nullable: false, // Angiver, at kolonnen ikke længere tillader null-værdier.
                defaultValue: "", // Sætter en standardværdi for kolonnen.
                oldClrType: typeof(string), // Den aktuelle datatype for kolonnen.
                oldType: "nvarchar(max)", // Den aktuelle SQL-datatype for kolonnen.
                oldNullable: true); // Angiver, at kolonnen i den nuværende tilstand tillader null-værdier.

            migrationBuilder.AlterColumn<int>( // Gendanner kolonnen "BetalingsMetode" til dens tidligere tilstand.
                name: "BetalingsMetode", // Navnet på kolonnen, der ændres tilbage.
                table: "Betalinger", // Tabellen, hvor kolonnen findes.
                type: "int", // Angiver den tidligere datatype som integer i SQL.
                nullable: false, // Angiver, at kolonnen ikke længere tillader null-værdier.
                defaultValue: 0, // Sætter en standardværdi for kolonnen.
                oldClrType: typeof(int), // Den aktuelle datatype for kolonnen.
                oldType: "int", // Den aktuelle SQL-datatype for kolonnen.
                oldNullable: true); // Angiver, at kolonnen i den nuværende tilstand tillader null-værdier.
        }
    }
}


// Denne migration ændrer strukturen i to tabeller i databasen: Kunder og Betalinger.
// I tabellen Kunder ændres kolonnen ScooterMaerke til at tillade null-værdier, hvilket gør det valgfrit at angive et mærke.
// I tabellen Betalinger ændres kolonnen BetalingsMetode også til at tillade null-værdier, hvilket gør det muligt at have betalinger uden en specificeret betalingsmetode.

// I Up-metoden implementeres disse ændringer ved at ændre datatyperne og nullable-egenskaberne for de to kolonner.

// I Down-metoden rulles ændringerne tilbage ved at gendanne kolonnerne til deres tidligere tilstand, hvor ScooterMaerke og BetalingsMetode er påkrævet
// og ikke tillader null-værdier, med standardværdier angivet for at sikre dataintegritet.