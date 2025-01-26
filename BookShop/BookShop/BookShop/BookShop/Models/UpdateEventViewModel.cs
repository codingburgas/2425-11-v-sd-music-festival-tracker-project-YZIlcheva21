namespace MusicEvents.Models;

public class UpdateEventViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
}