namespace KinoVerwaltungAPI.Data
{
    using Microsoft.EntityFrameworkCore;
    using KinoVerwaltungAPI.Models;
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSets reprentiert tabellen in der datenbank.
        public DbSet<Kino> Kinos { get; set; }
        public DbSet<Adresse> Adressen { get; set; }
        public DbSet<Saal> Saele { get; set; }
        public DbSet<Reihe> Reihen { get; set; }
        public DbSet<Sitz> Sitze { get; set; }
        public DbSet<Film> Filme { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Sprache> Sprachen { get; set; }
        public DbSet<Vorführung> Vorführungen { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Benutzer> Benutzer { get; set; }
        public DbSet<Rolle> Rollen { get; set; }
        public DbSet<Mitgliederstatus> Mitgliederstatus { get; set; }
        public DbSet<Mitgliederkarte> Mitgliederkarten { get; set; }
        public DbSet<Zahlungsmethode> Zahlungsmethoden { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Kinosäle Konfiguration
            modelBuilder.Entity<Kino>()
                .HasMany(k => k.Saele)
                .WithOne(s => s.Kino)
                .HasForeignKey(s => s.KinoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Saal>()
                .HasMany(s => s.Reihen)
                .WithOne(r => r.Saal)
                .HasForeignKey(r => r.SaalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reihe>()
                .HasMany(r => r.Sitze)
                .WithOne(s => s.Reihe)
                .HasForeignKey(s => s.ReiheId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Vorführungen Konfiguration
            modelBuilder.Entity<Vorführung>()
                .HasMany(v => v.Tickets)
                .WithOne(t => t.Vorführung)
                .HasForeignKey(t => t.VorführungId)
                .OnDelete(DeleteBehavior.Restrict); // Vermeiden von Kaskadierungskonflikten
            #endregion

            #region Mitgliederkarte und Mitgliederstatus Beziehung
            modelBuilder.Entity<Mitgliederkarte>()
                .HasOne(mk => mk.Mitgliederstatus)
                .WithMany()
                .HasForeignKey(mk => mk.MitgliederstatusId)
                .OnDelete(DeleteBehavior.Restrict); // Vermeiden von Kaskadierungskonflikten
            #endregion

            #region Ticket und Zahlungsmethode Konfiguration
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Benutzer)
                .WithMany(b => b.Tickets)
                .HasForeignKey(t => t.BenutzerId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade, wenn geschäftlich sinnvoll

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Sitz)
                .WithMany()
                .HasForeignKey(t => t.SitzId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Zahlungsmethode)
                .WithMany(zm => zm.Tickets)
                .HasForeignKey(t => t.ZahlungsmethodeId)
                .OnDelete(DeleteBehavior.Restrict); // Vermeiden von Kaskadierungskonflikten
            #endregion

            #region Benutzer und Mitgliederkarte Beziehung
            modelBuilder.Entity<Benutzer>()
                .HasOne(b => b.Mitgliederkarte)
                .WithOne(mk => mk.Benutzer)
                .HasForeignKey<Mitgliederkarte>(mk => mk.BenutzerId)
                .OnDelete(DeleteBehavior.Cascade); // Löschen der Mitgliederkarte, wenn der Benutzer gelöscht wird
            #endregion
        }
    }
}
