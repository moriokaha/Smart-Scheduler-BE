namespace SmartScheduler.Data.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int CalendarId { get; set; } // FK to Calendar table
        public string Title { get; set; } = "Default Event Title";
        public string? Description { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public required Location Location { get; set; }
        public required State State { get; set; } // Current event state (in progress, finished, etc.)
    }
}