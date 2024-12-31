using Microsoft.AspNetCore.Components.Web; // Importerer namespaces til understøttelse af Blazor-komponenter og browserinteraktioner.
using Microsoft.AspNetCore.Components.WebAssembly.Hosting; // Importerer namespaces til opsætning og konfiguration af en Blazor WebAssembly-applikation.
using ScooterLandProjectOpg.Client; // Importerer navnerummet for klientdelen af dit projekt, hvor App-komponenten er defineret.

// Initialiserer en standard WebAssembly-vært, som konfigurerer Blazor-applikationen med standardindstillinger, såsom miljø og konfiguration.
var builder = WebAssemblyHostBuilder.CreateDefault(args); 

// Tilføjer den primære komponent, `App`, til DOM-elementet med ID'et `#app`. Dette binder hele Blazor-applikationen til det specificerede DOM-element i index.html.
builder.RootComponents.Add<App>("#app"); 

// Tilføjer `HeadOutlet`-komponenten til DOM-elementet placeret efter `<head>` i index.html. Dette bruges til dynamisk opdatering af metadata i dokumentets `<head>`-sektion, f.eks. titler og beskrivelser.
builder.RootComponents.Add<HeadOutlet>("head::after"); 

// Registrerer en `HttpClient` som en scoped service, hvilket betyder, at en ny instans oprettes for hver bruger-session.
// `BaseAddress` sættes til applikationens rod-URL, hvilket muliggør HTTP-anmodninger til API'er eller andre ressourcer.
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }); 

// Bygger og starter Blazor WebAssembly-applikationen asynkront. Dette initialiserer applikationen og kører den i browseren.
await builder.Build().RunAsync(); 