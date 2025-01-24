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

        // DbSets for models
        public DbSet<Performance> Performances { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Performer> Performers { get; set; } = null!;

        // Fluent API configuration
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

            // Configuration for User model
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.Role)
                    .HasDefaultValue("User");
            });

            // Configuration for Performance model (if needed)
            modelBuilder.Entity<Performance>(entity =>
            {
                entity.Property(p => p.Title)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(p => p.Performer)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Venue)
                    .HasMaxLength(200);

                entity.Property(p => p.Date)
                    .IsRequired();
            });

            // Configuration for Event model (if needed)
            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Location)
                    .HasMaxLength(200);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
