namespace SmartScheduler.Data.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int CalendarId { get; set; } // FK to Calendar table
        public string Title { get; set; } = "Appointment";
        public string? Description { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public int EmployeeId { get; set; }
        public required virtual Location Location { get; set; }
        public required virtual Employee Employee { get; set; }
        public required virtual AppointmentState AppointmentState { get; set; }
    }
}
