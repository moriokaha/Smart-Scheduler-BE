namespace SmartScheduler.Data.Models
{
    public class ServiceType
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int ServiceGroupId { get; set; } // FK to ServiceGroup table

        // Proprietate de navigație
        public required virtual ServiceGroup ServiceGroup { get; set; }
    }
}