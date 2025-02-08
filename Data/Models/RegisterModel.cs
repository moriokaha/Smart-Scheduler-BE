﻿namespace SmartScheduler.Data.Models
{
    public class RegisterModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string Role { get; set; } = "User";
    }
}