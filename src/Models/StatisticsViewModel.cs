namespace MusicFestivalManagementSystem.Models
{
    public class StatisticsViewModel
    {
        public int TotalPerformers { get; set; }
        public int TotalPerformances { get; set; }
        public int TotalEvents { get; set; }
        public string MostPopularGenre { get; set; } = string.Empty;
    }
}
