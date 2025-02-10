namespace SmartScheduler.Data.Models
{
    public class LocationWorkInterval
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public int Day { get; set; }
        public TimeOnly TimeStart { get; set; }
        public TimeOnly TimeEnd { get; set; }
    }
}
