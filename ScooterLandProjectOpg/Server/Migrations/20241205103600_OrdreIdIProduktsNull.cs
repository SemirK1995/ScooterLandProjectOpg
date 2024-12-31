using Microsoft.EntityFrameworkCore.Migrations; // Importerer nødvendige namespaces for at understøtte database-migrationer.

#nullable disable // Deaktiverer nullable-analyse for at sikre kompatibilitet med eksisterende kode.

namespace ScooterLandProjectOpg.Server.Migrations // Definerer namespace for at organisere migrationen som en del af projektet.
{
    /// <inheritdoc />
    public partial class OrdreIdIProduktsNull : Migration // Definerer en migration, der gør "OrdreId" nullable i tabellen "Produkter".
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // Implementerer de ændringer, der skal anvendes på databasen, når migrationen køres.
        {
            migrationBuilder.DropForeignKey( // Fjerner den eksisterende fremmednøgle-relation mellem "Produkter" og "Ordrer".
                name: "FK_Produkter_Ordrer_OrdreId", // Navnet på den fremmednøgle, der fjernes.
                table: "Produkter"); // Tabellen, hvor fremmednøglen findes.

            migrationBuilder.AlterColumn<int>( // Ændrer "OrdreId"-kolonnen i "Produkter" til at tillade null-værdier.
                name: "OrdreId", // Navnet på kolonnen, der ændres.
                table: "Produkter", // Tabellen, hvor kolonnen findes.
                type: "int", // Bevarer kolonnens datatype som int.
                nullable: true, // Tillader nu null-værdier i kolonnen.
                oldClrType: typeof(int), // Den tidligere datatype for kolonnen.
                oldType: "int"); // Den tidligere typebeskrivelse for kolonnen.

            migrationBuilder.AddForeignKey( // Tilføjer en ny fremmednøgle-relation med opdaterede egenskaber.
                name: "FK_Produkter_Ordrer_OrdreId", // Navnet på den nye fremmednøgle.
                table: "Produkter", // Tabellen, hvor fremmednøglen er defineret.
                column: "OrdreId", // Kolonnen i "Produkter", der refererer til "Ordrer".
                principalTable: "Ordrer", // Tabellen, der refereres til.
                principalColumn: "OrdreId"); // Den primære nøgle i "Ordrer", som fremmednøglen peger på.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // Implementerer de ændringer, der fortryder migrationen, hvis den rulles tilbage.
        {
            migrationBuilder.DropForeignKey( // Fjerner den opdaterede fremmednøgle-relation.
                name: "FK_Produkter_Ordrer_OrdreId", // Navnet på fremmednøglen, der fjernes.
                table: "Produkter"); // Tabellen, hvor fremmednøglen findes.

            migrationBuilder.AlterColumn<int>( // Ændrer "OrdreId"-kolonnen tilbage til at være ikke-null.
                name: "OrdreId", // Navnet på kolonnen, der ændres tilbage.
                table: "Produkter", // Tabellen, hvor kolonnen findes.
                type: "int", // Bevarer kolonnens datatype som int.
                nullable: false, // Gør det ikke længere muligt for kolonnen at indeholde null-værdier.
                defaultValue: 0, // Sætter en standardværdi for kolonnen.
                oldClrType: typeof(int), // Den aktuelle datatype for kolonnen.
                oldType: "int", // Den aktuelle typebeskrivelse for kolonnen.
                oldNullable: true); // Indikator for, at kolonnen tidligere kunne være null.

            migrationBuilder.AddForeignKey( // Genskaber den oprindelige fremmednøgle-relation.
                name: "FK_Produkter_Ordrer_OrdreId", // Navnet på fremmednøglen.
                table: "Produkter", // Tabellen, hvor fremmednøglen er defineret.
                column: "OrdreId", // Kolonnen i "Produkter", der refererer til "Ordrer".
                principalTable: "Ordrer", // Tabellen, der refereres til.
                principalColumn: "OrdreId", // Den primære nøgle i "Ordrer", som fremmednøglen peger på.
                onDelete: ReferentialAction.Cascade); // Definerer, at relaterede rækker skal slettes, hvis referencen slettes i "Ordrer".
        }
    }
}


// Denne migration ændrer kolonnen OrdreId i tabellen Produkter fra at være obligatorisk til at tillade null-værdier, hvilket gør det muligt for produkter at eksistere uden nødvendigvis at være knyttet til en specifik ordre.

// I Up-metoden fjernes den eksisterende fremmednøgle, der krævede en ikke-null værdi i OrdreId.
// Derefter ændres kolonnen OrdreId til at være nullable. Til sidst oprettes en ny fremmednøgle, som tillader null-værdier i OrdreId, men stadig sikrer referentiel integritet, hvis en værdi er angivet.

// I Down-metoden rulles ændringerne tilbage.
// Den nullable-fremmednøgle fjernes, og kolonnen OrdreId ændres tilbage til at være ikke-null.
// En ny fremmednøgle oprettes, som kræver en værdi og sikrer, at sletninger i tabellen Ordrer resulterer i sletninger af relaterede rækker i Produkter (via Cascade).