namespace SmartScheduler.Data.Models
{
    public class AppointmentState
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; } // FK pentru programare
        public DateTime StateChangedDate { get; set; }
        public StateType State { get; set; }
        public string? Comments { get; set; }
    }

    public enum StateType
    {
        Scheduled,
        Cancelled,
        Completed,
        Pending,
        Rescheduled
    }
}