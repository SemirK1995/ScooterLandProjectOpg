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
		public DbSet<MekanikerYdelse> MekanikerYdelser { get; set; }
		public DbSet<Ordre> Ordrer { get; set; }
		public DbSet<OrdreYdelse> OrdreYdelser { get; set; }
		public DbSet<Ydelse> Ydelser { get; set; }
	}
}
