using Microsoft.EntityFrameworkCore;

namespace MusicEvents.Data;

public class EntityContext : DbContext
{
    public EntityContext(DbContextOptions<EntityContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Music> Events { get; set; }
}