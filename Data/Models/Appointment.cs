namespace SmartScheduler.Data.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public int LocationId { get; set; }
        public int EmployeeId { get; set; }
        public required virtual Location Location { get; set; }
        public required virtual Employee Employee { get; set; }
    }
}
