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

        public DbSet<Performance> Performances { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
    }
}
