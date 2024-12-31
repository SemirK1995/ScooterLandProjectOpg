using Microsoft.EntityFrameworkCore.Migrations; // Importerer funktioner til håndtering af databaseændringer ved hjælp af migrations i Entity Framework Core.

#nullable disable // Deaktiverer nullable-analyse, hvilket gør det muligt at arbejde med ældre kode uden nullable-konflikter.

namespace ScooterLandProjectOpg.Server.Migrations // Definerer det namespace, der organiserer migrationsfilerne i projektet.
{
    /// <inheritdoc />
    public partial class AddedLejeIdTilOrdre : Migration // Definerer en migrationsklasse, som sandsynligvis skulle tilføje "LejeId" til "Ordre"-tabellen.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // Denne metode bruges til at specificere ændringer, der skal foretages på databasen, når migrationen anvendes.
        {
            // Der er ingen implementerede ændringer, hvilket betyder, at denne migration ikke indeholder nogen operationer at udføre.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // Denne metode specificerer ændringer for at rulle migrationen tilbage, hvis det er nødvendigt.
        {
            // Der er ingen implementerede operationer til at fortryde ændringer, hvilket betyder, at migrationen ikke efterlader nogen effekter at fortryde.
        }
    }
}


// Denne migrationsklasse er oprettet, men både Up og Down metoderne er tomme, hvilket betyder, at der ikke foretages nogen ændringer på databasen.
// Navnet på migrationen antyder, at den skulle tilføje en "LejeId"-kolonne til "Ordre"-tabellen, men dette er ikke blevet implementeret.
// Det kan være, at dette er en placeholder-migration, hvor de faktiske ændringer endnu ikke er blevet defineret.