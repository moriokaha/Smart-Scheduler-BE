namespace SmartScheduler.Data.Models
{
    public class Appointment: BaseEntity
    {
        public int CalendarId { get; set; } // FK to Calendar table
        public string Title { get; set; } = "Appointment";
        public string? Description { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public int EmployeeId { get; set; }
        public required virtual Location Location { get; set; }
        public required virtual Employee Employee { get; set; }
        public required virtual AppointmentState AppointmentState { get; set; }

        // Opțional user
        public int? UserId { get; set; }
        public virtual User? User { get; set; }

        // For anonymous clients
        public string? ClientName { get; set; }
        public string? ClientPhone { get; set; }
    }
}
