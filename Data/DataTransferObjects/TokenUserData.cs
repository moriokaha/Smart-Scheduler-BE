using SmartScheduler.Data.Models;

namespace SmartScheduler.Data.DataTransferObjects
{
    public class TokenUserData
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.User;
    }
}
