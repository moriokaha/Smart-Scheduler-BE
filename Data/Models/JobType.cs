namespace SmartScheduler.Data.Models
{
    public class JobType
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int ServiceGroupId { get; set; } // FK to ServiceGroup table

        // Proprietate de navigație
        public required virtual JobGroup ServiceGroup { get; set; }
    }
}