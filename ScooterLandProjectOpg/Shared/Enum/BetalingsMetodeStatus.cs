using System; // Importerer namespace for grundlæggende funktionalitet som dato og tid.
using System.Collections.Generic; // Importerer namespace for generiske samlinger.
using System.Linq; // Importerer namespace for LINQ-funktionalitet.
using System.Text; // Importerer namespace for tekstmanipulation.
using System.Threading.Tasks; // Importerer namespace for asynkrone operationer.

// Enum (enumeration) er en speciel datatype i C#, der bruges til at definere en samling af navngivne værdier, som repræsenterer en liste af relaterede konstanter.
// Det gør koden mere læsbar og nemmere at vedligeholde ved at bruge navne i stedet for "magiske tal".

namespace ScooterLandProjectOpg.Shared.Enum // Definerer namespace, som organiserer klasser, enums osv.
{
    // Definerer en enumeration for forskellige betalingsmetoder.
    public enum BetalingsMetodeStatus 
    {
        Mobilepay = 1, // Repræsenterer MobilePay med en værdi på 1.
        KreditKort = 2, // Repræsenterer betaling med kreditkort med en værdi på 2.
        Kontanter = 3, // Repræsenterer kontant betaling med en værdi på 3.
        Bankoverførsel = 4 // Repræsenterer betaling via bankoverførsel med en værdi på 4.
    }
}