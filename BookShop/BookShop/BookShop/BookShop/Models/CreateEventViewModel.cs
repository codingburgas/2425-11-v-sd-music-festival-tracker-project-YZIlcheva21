namespace MusicEvents.Models;

public class CreateEventViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool IsPublic { get; set; } = false;
    public IFormFile? Image { get; set; }
}