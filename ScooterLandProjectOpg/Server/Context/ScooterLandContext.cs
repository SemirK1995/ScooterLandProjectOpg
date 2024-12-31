using Microsoft.EntityFrameworkCore; // Importerer EntityFrameworkCore-navneområdet, så vi kan bruge DbContext og relaterede klasser.
using ScooterLandProjectOpg.Shared.Models; // Importerer de modelklasser, som Context-klassen skal bruge.

namespace ScooterLandProjectOpg.Server.Context // Angiver, at klassen nedenfor ligger i ScooterLandProjectOpg.Server.Context-namespace.
{
    public class ScooterLandContext : DbContext // Definerer en kontekstklasse, som nedarver fra Microsofts DbContext, og repræsenterer databasen.
    {
        public ScooterLandContext(DbContextOptions options) : base(options) // Constructor der modtager DbContextOptions, som konfigurerer denne kontekst (f.eks. database-connection).
        {
            // Kalder base-klassens constructor med disse options.
        }

        public DbSet<Kunde> Kunder { get; set; } // En tabel (DbSet) kaldet 'Kunder', der indeholder Kunde-objekter.
        public DbSet<Betaling> Betalinger { get; set; } // Tabel for Betaling-objekter.
        public DbSet<KundeScooter> KunderScootere { get; set; } // Tabel for KundeScooter-objekter.
        public DbSet<LejeAftale> LejeAftaler { get; set; } // Tabel for LejeAftale-objekter.
        public DbSet<LejeScooter> LejeScootere { get; set; } // Tabel for LejeScooter-objekter.
        public DbSet<Mekaniker> Mekanikere { get; set; } // Tabel for Mekaniker-objekter.
        public DbSet<Ordre> Ordrer { get; set; } // Tabel for Ordre-objekter.
        public DbSet<OrdreYdelse> OrdreYdelser { get; set; } // Tabel for OrdreYdelse-objekter.
        public DbSet<Ydelse> Ydelser { get; set; } // Tabel for Ydelse-objekter.
        public DbSet<Produkt> Produkter { get; set; } // Tabel for Produkt-objekter.
        public DbSet<OrdreProdukt> OrdreProdukter { get; set; } // Tabel for OrdreProdukt-objekter.


        // Konfiguration af relationer mellem modeller
        protected override void OnModelCreating(ModelBuilder modelBuilder) // Metode hvor man kan konfigurere tabeller, relationer og constraints ud over standardkonfigurationen.
        {
            base.OnModelCreating(modelBuilder); // Kalder den grundlæggende implementering i DbContext.

            // Definer relationen mellem LejeScooter og LejeAftale
            modelBuilder.Entity<LejeScooter>()
                .HasOne(ls => ls.LejeAftale) // LejeScooter har en LejeAftale-ejendom.
                .WithMany(la => la.LejeScooter) // LejeAftale kan have mange LejeScooter.
                .HasForeignKey(ls => ls.LejeId) // Foreign Key i LejeScooter er LejeId.
                .OnDelete(DeleteBehavior.Restrict); // Slet ikke LejeScooter automatisk, hvis LejeAftale slettes.

            // Definer relationen mellem Ordre og LejeAftale
            modelBuilder.Entity<Ordre>()
                .HasOne(o => o.LejeAftale) // Ordre har en LejeAftale-ejendom.
                .WithMany() // Hvis LejeAftale kan have flere ordrer, kan man specificere WithMany(la => la.Ordrer).
                .HasForeignKey(o => o.LejeId) // Foreign Key i Ordre er LejeId.
                .OnDelete(DeleteBehavior.Restrict); // Forhindrer automatisk sletning (eller vælg Cascade, hvis man ønsker det).

            // Konfigurer relation mellem Kunde og LejeAftaler med Cascade Delete
            modelBuilder.Entity<Kunde>()
                .HasMany(k => k.LejeAftale) // En kunde kan have mange LejeAftaler.
                .WithOne(la => la.Kunde) // Hver LejeAftale har én Kunde.
                .HasForeignKey(la => la.KundeId) // Foreign Key er KundeId i LejeAftale.
                .OnDelete(DeleteBehavior.Cascade); // Slet alle LejeAftaler, hvis Kunden slettes.

            // Konfigurer relation mellem LejeAftale og LejeScooter med Cascade Delete
            modelBuilder.Entity<LejeAftale>()
                .HasMany(la => la.LejeScooter) // En LejeAftale kan have mange LejeScooter.
                .WithOne(ls => ls.LejeAftale) // Hver LejeScooter har én LejeAftale.
                .HasForeignKey(ls => ls.LejeId) // Foreign Key er LejeId i LejeScooter.
                .OnDelete(DeleteBehavior.Cascade); // Slet alle LejeScootere, hvis LejeAftalen slettes.

            modelBuilder.Entity<OrdreProdukt>()
                .HasOne(op => op.Ordre) // OrdreProdukt har en Ordre-ejendom.
                .WithMany(o => o.OrdreProdukter) // En Ordre kan have mange OrdreProdukt.
                .HasForeignKey(op => op.OrdreId) // Foreign Key i OrdreProdukt er OrdreId.
                .OnDelete(DeleteBehavior.Cascade); // Sletter OrdreProdukter, hvis Ordre slettes.

            modelBuilder.Entity<OrdreYdelse>()
                .HasOne(oy => oy.Ordre) // OrdreYdelse har en Ordre-ejendom.
                .WithMany(o => o.OrdreYdelse) // En Ordre kan have mange OrdreYdelse.
                .HasForeignKey(oy => oy.OrdreId) // Foreign Key i OrdreYdelse er OrdreId.
                .OnDelete(DeleteBehavior.Cascade); // Sletter OrdreYdelser, hvis Ordre slettes.
        }
    }
}