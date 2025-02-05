namespace SmartScheduler.Data.Models
{
    public class Calendar
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public List<Appointment> Appointments { get; set; } = [];
        public DateTime CreationDate { get; set; }
        public bool IsGlobal { get; set; }
    }
}