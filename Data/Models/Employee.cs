namespace SmartScheduler.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string?  Description { get; set; }
        public required int LocationId { get; set; } // FK to Locations table
        public required virtual Location Location { get; set; }
    }
}