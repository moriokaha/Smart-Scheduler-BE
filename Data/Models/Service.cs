﻿namespace SmartScheduler.Data.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Default service";
        public string? Description { get; set; }
        public int ServiceTypeId { get; set; } // FK to ServiceType table
        public TimeSpan CompletionTime { get; set; } // How long is going to take
        public required virtual ServiceType ServiceType { get; set; }
    }
}