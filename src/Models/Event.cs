using System;

namespace MusicFestivalManagementSystem.Models
{
    public class Event
    {
        public int Id { get; set; } // Primary Key
        public string? Name { get; set; }
        public DateTime Date { get; set; } // Date and time of the event
        public string? Location { get; set; }
        public string? Description { get; set; }

    }
}
