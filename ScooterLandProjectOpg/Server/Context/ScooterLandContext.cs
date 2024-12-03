using Microsoft.EntityFrameworkCore;
using ScooterLandProjectOpg.Shared.Models;

namespace ScooterLandProjectOpg.Server.Context
{
	public class ScooterLandContext : DbContext
	{
		public ScooterLandContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<Kunde> Kunder { get; set; }
		public DbSet<Betaling> Betalinger { get; set; }
		public DbSet<KundeScooter> KunderScootere { get; set; }
		public DbSet<LejeAftale> LejeAftaler { get; set; }
		public DbSet<LejeScooter> LejeScootere { get; set; }
		public DbSet<Mekaniker> Mekanikere { get; set; }
		public DbSet<Ordre> Ordrer { get; set; }
		public DbSet<OrdreYdelse> OrdreYdelser { get; set; }
		public DbSet<Ydelse> Ydelser { get; set; }


        // Konfiguration af relationer mellem modeller
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definer relationen mellem LejeScooter og LejeAftale
            modelBuilder.Entity<LejeScooter>()
                .HasOne(ls => ls.LejeAftale) // LejeScooter har en LejeAftale
                .WithMany(la => la.LejeScooter) // LejeAftale kan have mange LejeScooter
                .HasForeignKey(ls => ls.LejeId) // Foreign Key er LejeId i LejeScooter
                .OnDelete(DeleteBehavior.Restrict); // Restriktiv sletning, så scootere ikke slettes automatisk

			// Definer relationen mellem Ordre og LejeAftale
			modelBuilder.Entity<Ordre>()
				.HasOne(o => o.LejeAftale) // Ordre har en LejeAftale
				.WithMany() // Hvis LejeAftale kan have flere ordrer, tilføj WithMany(la => la.Ordrer)
				.HasForeignKey(o => o.LejeId) // Foreign Key er LejeId i Ordre
				.OnDelete(DeleteBehavior.Restrict); // Eller Cascade, afhængigt af dine krav
		}

    }
}
