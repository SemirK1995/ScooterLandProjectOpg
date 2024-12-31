using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    /// <inheritdoc />
    public partial class fjernetafsluttet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Denne metode repræsenterer ændringer, der skal foretages på databasen, når migrationen anvendes.
            // I denne migration er metoden tom, hvilket betyder, at der ikke foretages ændringer i databasen.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Denne metode repræsenterer ændringer, der skal foretages på databasen for at rulle migrationen tilbage.
            // I denne migration er metoden også tom, hvilket betyder, at der ikke foretages ændringer, når migrationen rulles tilbage.
        }
    }
}


// Denne migrationsklasse er defineret, men både Up og Down metoderne er tomme, hvilket betyder, at der ikke foretages nogen ændringer i databasen, hverken når migrationen anvendes eller rulles tilbage.
// Navnet på migrationen, fjernetafsluttet, antyder, at den muligvis var tiltænkt at fjerne noget, sandsynligvis en kolonne eller et felt relateret til en "afsluttet"-status eller lignende,
// men dette er ikke blevet implementeret i koden. Det kan være en placeholder-migration, der endnu ikke er blevet udfyldt.