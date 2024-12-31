using Microsoft.EntityFrameworkCore.Migrations; // Importerer biblioteket for at understøtte database-migrationer.

#nullable disable // Deaktiverer nullable-analyse for at sikre kompatibilitet med tidligere kode.

namespace ScooterLandProjectOpg.Server.Migrations // Definerer namespace for migrationer i projektet.
{
    /// <inheritdoc />
    public partial class lejeaftaleLagtSpørgsmål : Migration // Definerer en migration med navnet "lejeaftaleLagtSpørgsmål".
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // Denne metode anvender ændringer til databasen.
        {
            // Ingen ændringer specificeret i denne migration. Her kunne nye kolonner, tabeller eller ændringer være defineret.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // Denne metode fortryder ændringerne fra metoden "Up".
        {
            // Ingen fortrydelser specificeret, da "Up"-metoden ikke implementerer nogen ændringer.
        }
    }
}


// Denne migration er en placeholder uden nogen implementerede ændringer i metoderne Up eller Down.
// Den er ofte brugt som udgangspunkt for senere at tilføje ændringer eller bruges til at nulstille tilstanden i migrationssystemet.