using MusicFestivalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace MusicFestivalManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for existing models
        public DbSet<Performance> Performances { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!; // Removed nullable for consistency.

        // DbSet for Performers
        public DbSet<Performer> Performers { get; set; } = null!;

        // Fluent API configuration (optional but recommended)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration for Performer model
            modelBuilder.Entity<Performer>(entity =>
            {
                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Genre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.Description)
                    .HasMaxLength(500);
            });

            // Configuration for User model (example if needed)
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.Role)
                    .HasDefaultValue("User");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
