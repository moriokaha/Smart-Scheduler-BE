using Microsoft.AspNetCore.Identity;

namespace SmartScheduler.Data.Models
{
    public class UserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.User;
    }

    public enum UserRole
    {
        User,
        Manager,
        Admin
    }
}