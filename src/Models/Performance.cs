using System.ComponentModel.DataAnnotations;

namespace MusicFestivalManagementSystem.Models
{
    public class Performance
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Venue { get; set; } = string.Empty;

        [Required]
        public string Performer { get; set; } = string.Empty;

        public DateTime Date { get; set; }
    }
}
