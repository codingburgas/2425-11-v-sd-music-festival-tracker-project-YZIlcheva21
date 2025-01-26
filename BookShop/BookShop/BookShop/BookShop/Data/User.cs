namespace MusicEvents.Data;

public class User : Entity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public ICollection<Music> Events { get; set; } = new HashSet<Music>();
}