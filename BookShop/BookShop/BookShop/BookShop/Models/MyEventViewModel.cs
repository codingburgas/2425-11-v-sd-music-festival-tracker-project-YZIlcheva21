namespace MusicEvents.Models
{
    internal class MyEventViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public string Content { get; set; }
        public string ImageBase64 { get; set; }
    }
}