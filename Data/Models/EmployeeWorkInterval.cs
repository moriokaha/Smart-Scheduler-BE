namespace SmartScheduler.Data.Models
{
    public class EmployeeWorkInterval
    {
        public int Id { get; set; }
        public int  EmployeeId { get; set; }
        public int Day { get; set; }
        public TimeOnly TimeStart { get; set; }
        public TimeOnly TimeEnd { get; set; }
    }
}
