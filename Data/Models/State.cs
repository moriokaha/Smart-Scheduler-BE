namespace SmartScheduler.Data.Models
{
    public class State
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}