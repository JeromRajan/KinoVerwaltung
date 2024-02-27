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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and keys
            //modelBuilder.Entity<Kino>()
            //            .HasMany(k => k.Saele)
            //            .WithOne()
            //            .HasForeignKey(s => s.FK_Kino);

            // Add other configurations
        }
    }
}
