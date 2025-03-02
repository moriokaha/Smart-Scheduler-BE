namespace SmartScheduler.Data.Models
{
    public class Employee: BaseEntity
    {
        public required string Name { get; set; }
        public string?  Description { get; set; }
        public required int LocationId { get; set; } // FK to Locations table
    }
}