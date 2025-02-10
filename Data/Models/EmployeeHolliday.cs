namespace SmartScheduler.Data.Models
{
    public class EmployeeHolliday
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
