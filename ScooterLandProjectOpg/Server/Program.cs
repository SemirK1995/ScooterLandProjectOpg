using Microsoft.AspNetCore.ResponseCompression; // Importerer namespaces til komprimering af HTTP-svar.
using Microsoft.EntityFrameworkCore; // Importerer Entity Framework Core til databaseoperationer.
using ScooterLandProjectOpg.Server.Context; // Importerer databasekonteksten for projektet.
using ScooterLandProjectOpg.Server.Interfaces; // Importerer interfaces for dependency injection.
using ScooterLandProjectOpg.Server.PDFServices; // Importerer PDF-relaterede tjenester.
using ScooterLandProjectOpg.Server.Repositories;
using ScooterLandProjectOpg.Server.Repositories.Interfaces;
using ScooterLandProjectOpg.Server.Services;
using ScooterLandProjectOpg.Server.Services.Interfaces; // Importerer implementeringer af repositories og services.

// Dependency Injection (DI) er en programmeringsteknik, der gør en klasse uafhængig af sine afhængigheder.
// Det betyder, at en klasse ikke selv opretter eller styrer sine nødvendige objekter (afhængigheder), men i stedet får dem leveret udefra, typisk via en container.
// Dette gør koden mere fleksibel, testbar og lettere at vedligeholde.
// En "afhængighed" kan f.eks. være en tjeneste eller et objekt, som klassen har brug for at fungere.


var builder = WebApplication.CreateBuilder(args); // Opretter en ny webapplikation med de leverede argumenter.

// Tilføjer controller- og view-tjenester til DI-containeren og konfigurerer JSON-indstillinger.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; // Undgår reference loops i JSON-serialisering.
});

builder.Services.AddRazorPages(); // Tilføjer Razor Pages-tjenester til DI-containeren.

// Tilføjer scoped services for alle repositories, som bruges til DI.
builder.Services.AddScoped<IKundeRepository, KundeService>(); // Kunde repository.
builder.Services.AddScoped<IBetalingRepository, BetalingsService>(); // Betaling repository.
builder.Services.AddScoped<IKundeScooterRepository, KundeScooterService>(); // KundeScooter repository.
builder.Services.AddScoped<ILejeAftaleRepository, LejeAftaleService>(); // LejeAftale repository.
builder.Services.AddScoped<ILejeScooterRepository, LejeScooterService>(); // LejeScooter repository.
builder.Services.AddScoped<IMekanikerRepository, MekanikerService>(); // Mekaniker repository.
builder.Services.AddScoped<IOrdreService, OrdreService>(); // Ordre repository.
builder.Services.AddScoped<IOrdreYdelseRepository, OrdreYdelseService>(); // OrdreYdelse repository.
builder.Services.AddScoped<IYdelseRepository, YdelseService>(); // Ydelse repository.
builder.Services.AddScoped<IProduktRepository, ProduktService>(); // Produkt repository.
builder.Services.AddScoped<IOrdreRepository, OrdreRepository>(); // Produkt repository.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // Generisk repository for alle modeller.

// Tilføjer FakturaService som en transient service, da det er en separat tjeneste til generering af fakturaer.
builder.Services.AddTransient<FakturaService>();

// Konfigurerer databasen ved hjælp af en connection string fra appsettings.json.
builder.Services.AddDbContext<ScooterLandContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // Bruger connection string til SQL Server.
});

var app = builder.Build(); // Bygger webapplikationen.

// Konfigurerer middleware og HTTP request pipeline.
if (app.Environment.IsDevelopment()) // Tjekker, om miljøet er "Development".
{
    app.UseWebAssemblyDebugging(); // Aktiverer debugging for WebAssembly i udviklingsmiljøet.
}
else
{
    app.UseExceptionHandler("/Error"); // Bruger en fejlside i produktionsmiljøet.
    app.UseHsts(); // Aktiverer HTTP Strict Transport Security (HSTS) med en standardindstilling på 30 dage.
}

app.UseHttpsRedirection(); // Omdirigerer alle HTTP-anmodninger til HTTPS.

app.UseBlazorFrameworkFiles(); // Aktiverer Blazor-framework-filer til klientdelen af applikationen.
app.UseStaticFiles(); // Gør statiske filer som CSS og JS tilgængelige.

app.UseRouting(); // Aktiverer routing-middleware til behandling af URL-forespørgsler.

app.MapRazorPages(); // Tilknytter Razor Pages til request pipeline.
app.MapControllers(); // Tilknytter API-controllere til request pipeline.

app.MapFallbackToFile("index.html"); // Fallback, der indlæser index.html for ikke-matchede anmodninger.

app.Run(); // Starter applikationen og begynder at lytte efter anmodninger.