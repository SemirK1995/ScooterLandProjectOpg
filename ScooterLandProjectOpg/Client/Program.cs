using Microsoft.AspNetCore.Components.Web; // Importerer namespaces til underst�ttelse af Blazor-komponenter og browserinteraktioner.
using Microsoft.AspNetCore.Components.WebAssembly.Hosting; // Importerer namespaces til ops�tning og konfiguration af en Blazor WebAssembly-applikation.
using ScooterLandProjectOpg.Client; // Importerer navnerummet for klientdelen af dit projekt, hvor App-komponenten er defineret.

// Initialiserer en standard WebAssembly-v�rt, som konfigurerer Blazor-applikationen med standardindstillinger, s�som milj� og konfiguration.
var builder = WebAssemblyHostBuilder.CreateDefault(args); 

// Tilf�jer den prim�re komponent, `App`, til DOM-elementet med ID'et `#app`. Dette binder hele Blazor-applikationen til det specificerede DOM-element i index.html.
builder.RootComponents.Add<App>("#app"); 

// Tilf�jer `HeadOutlet`-komponenten til DOM-elementet placeret efter `<head>` i index.html. Dette bruges til dynamisk opdatering af metadata i dokumentets `<head>`-sektion, f.eks. titler og beskrivelser.
builder.RootComponents.Add<HeadOutlet>("head::after"); 

// Registrerer en `HttpClient` som en scoped service, hvilket betyder, at en ny instans oprettes for hver bruger-session.
// `BaseAddress` s�ttes til applikationens rod-URL, hvilket muligg�r HTTP-anmodninger til API'er eller andre ressourcer.
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }); 

// Bygger og starter Blazor WebAssembly-applikationen asynkront. Dette initialiserer applikationen og k�rer den i browseren.
await builder.Build().RunAsync(); 