namespace MusicFestivalManagementSystem.Models
{
    public class Performance
    {
        public int Id { get; set; } // Primary key
        public string Title { get; set; } = string.Empty; // Title of the performance
        public string Performer { get; set; } = string.Empty; // Performer name
        public string Venue { get; set; } = string.Empty; // Venue name
        public DateTime Date { get; set; } // Date of the performance
    }
}
