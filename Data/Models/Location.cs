namespace SmartScheduler.Data.Models
{
    public class Location : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}