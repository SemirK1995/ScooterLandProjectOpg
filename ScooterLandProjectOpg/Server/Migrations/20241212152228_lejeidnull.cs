using Microsoft.EntityFrameworkCore.Migrations;

// Namespace til migrationer for ScooterLand-projektet. Dette definerer, hvor migrationer er placeret i serverens kodebase.
#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class lejeidnull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Denne metode definerer ændringer, der skal anvendes på databasen, når migrationen køres.
            // I denne migration er metoden tom, hvilket betyder, at der ikke foretages nogen ændringer.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Denne metode definerer, hvordan ændringerne, der blev anvendt i `Up`, skal rulles tilbage.
            // I denne migration er metoden tom, hvilket betyder, at der ikke foretages nogen ændringer ved rollback.
        }
    }
}


// Denne migrationsklasse er defineret, men både Up og Down metoderne er tomme.
// Det betyder, at der ikke foretages nogen ændringer i databasen, hverken når migrationen anvendes eller rulles tilbage.
// Navnet lejeidnull antyder, at migrationen kunne være tiltænkt at ændre noget ved LejeId, såsom at gøre det nullable, men dette er ikke implementeret i koden.
// Det kan være en placeholder, hvor implementeringen mangler.