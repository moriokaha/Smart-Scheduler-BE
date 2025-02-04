namespace SmartScheduler.Data.Models
{
    public class EmployeeService
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; } // Service price
        public required int EmployeeId { get; set; } // FK to Employees table
        public required int ServiceId { get; set; } // FK to Services table
        public required virtual Employee Employee { get; set; }
        public required virtual Service Service { get; set; }
    }
}