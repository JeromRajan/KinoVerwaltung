namespace KinoVerwaltungAPI.Data
{
    using Microsoft.EntityFrameworkCore;
    using KinoVerwaltungAPI.Models;
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //DBSets for entities
        public DbSet<Kino> Kinos { get; set; }
        public DbSet<Saal> Saele { get; set; }
        public DbSet<Film> Filme { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Benutzer> Benutzer { get; set; }
        public DbSet<Adresse> Adressen { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rolle> Rollen { get; set; }
        public DbSet<Reihe> Reihen { get; set; }
        public DbSet<Sitz> Sitz { get; set; }
        public DbSet<Vorfuehrung> Vorfuehrungen { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and keys
            modelBuilder.Entity<Kino>()
                    .HasMany(k => k.Saele)
                    .WithOne()
                    .HasForeignKey(s => s.KinoId);

            modelBuilder.Entity<Saal>()
                .HasMany(s => s.Reihen)
                .WithOne(r => r.Saal)
                .HasForeignKey(r => r.SaalId);

            modelBuilder.Entity<Reihe>()
                .HasMany(r => r.Sitze)
                .WithOne(s => s.Reihe)
                .HasForeignKey(s => s.ReiheId);

            modelBuilder.Entity<Vorfuehrung>()
                .HasMany(v => v.Tickets)
                .WithOne(t => t.Vorfuehrung)
                .HasForeignKey(t => t.VorfuehrungId);

            // Add other configurations
        }
    }
}
