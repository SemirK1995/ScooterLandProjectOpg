using Microsoft.EntityFrameworkCore.Migrations;

// Namespace for migrationer i ScooterLand-projektet. Dette angiver, hvor migrationer placeres i projektet.
#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class tilføjetstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Tilføjer en ny kolonne "Status" til "LejeAftaler"-tabellen. 
            // Kolonnen har typen "int" og kan indeholde null-værdier.
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LejeAftaler",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Fjerner "Status"-kolonnen fra "LejeAftaler"-tabellen, hvis migrationen rulles tilbage.
            migrationBuilder.DropColumn(
                name: "Status",
                table: "LejeAftaler");
        }
    }
}


// Denne migration introducerer en ny kolonne Status i tabellen LejeAftaler.
// Kolonnen er af typen int og er nullable, hvilket betyder, at den kan have værdien null.
// I Up-metoden tilføjes kolonnen til tabellen, mens den fjernes i Down-metoden, hvis migrationen skal rulles tilbage.
// Dette giver mulighed for at registrere statusinformation på lejeaftaler, f.eks. om en aftale er aktiv, afsluttet osv.