using System; // Inkluderer standardfunktioner som datatyper og systemværktøjer.
using System.Collections.Generic; // Giver funktionalitet til generiske samlinger.
using System.Linq; // Tilbyder LINQ-funktionalitet for forespørgsler over samlinger.
using System.Text; // Giver klasser til at arbejde med tekst og tekststrenge.
using System.Threading.Tasks; // Tilbyder funktioner til asynkron programmering.

// Enum (enumeration) er en speciel datatype i C#, der bruges til at definere en samling af navngivne værdier, som repræsenterer en liste af relaterede konstanter.
// Det gør koden mere læsbar og nemmere at vedligeholde ved at bruge navne i stedet for "magiske tal".

namespace ScooterLandProjectOpg.Shared.Enum // Definerer et namespace for at organisere koden i projektet.
{
    // Definerer en enumeration med navnet LejeAftaleStatus.
    public enum LejeAftaleStatus 
    {
        Aktiv, // Repræsenterer en status, hvor lejeaftalen er aktiv.
        Afsluttet // Repræsenterer en status, hvor lejeaftalen er afsluttet.
    }
}