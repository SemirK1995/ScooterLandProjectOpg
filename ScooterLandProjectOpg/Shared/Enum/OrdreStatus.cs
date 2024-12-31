using System; // Inkluderer grundlæggende funktionalitet som datatyper og systemværktøjer.
using System.Collections.Generic; // Giver funktionalitet til generiske samlinger.
using System.Linq; // Tilbyder LINQ-funktionalitet for forespørgsler over samlinger.
using System.Text; // Giver klasser til tekstmanipulation.
using System.Threading.Tasks; // Understøtter asynkron programmering og opgaver.

// Enum (enumeration) er en speciel datatype i C#, der bruges til at definere en samling af navngivne værdier, som repræsenterer en liste af relaterede konstanter.
// Det gør koden mere læsbar og nemmere at vedligeholde ved at bruge navne i stedet for "magiske tal".

namespace ScooterLandProjectOpg.Shared.Enum // Definerer et namespace til organisering af koden i projektet.
{
    // Definerer en enumeration med navnet OrdreStatus.
    public enum OrdreStatus 
    {
        Oprettet, // Repræsenterer status, hvor ordren er oprettet.
        Behandles, // Repræsenterer status, hvor ordren er under behandling.
        Annulleret, // Repræsenterer status, hvor ordren er annulleret.
        Betalt // Repræsenterer status, hvor ordren er betalt.
    }
}