using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Server.Context;
using ScooterLandProjectOpg.Server.Interfaces;
using ScooterLandProjectOpg.Server.PDFServices;
using ScooterLandProjectOpg.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddRazorPages();

builder.Services.AddScoped<IKundeRepository, KundeService>();
builder.Services.AddScoped<IBetalingRepository, BetalingsService>();
builder.Services.AddScoped<IKundeScooterRepository, KundeScooterService>();
builder.Services.AddScoped<ILejeAftaleRepository, LejeAftaleService>();
builder.Services.AddScoped<ILejeScooterRepository, LejeScooterService>();
builder.Services.AddScoped<IMekanikerRepository, MekanikerService>();
builder.Services.AddScoped<IOrdreRepository, OrdreService>();
builder.Services.AddScoped<IOrdreYdelseRepository, OrdreYdelseService>();
builder.Services.AddScoped<IYdelseRepository, YdelseService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<FakturaService>();




// Hent connection string fra appsettings.json
builder.Services.AddDbContext<ScooterLandContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
