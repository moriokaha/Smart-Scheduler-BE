using Microsoft.AspNetCore.Identity;

namespace SmartScheduler.Data.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? Department { get; set; }
        public Role Role { get; set; } = Role.User;
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }

    public enum Role
    {
        User = 0,
        Admin = 1,
        Manager = 2
    }
}