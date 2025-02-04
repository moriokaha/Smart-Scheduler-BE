namespace SmartScheduler.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? Department { get; set; }
        public required Role Role { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}