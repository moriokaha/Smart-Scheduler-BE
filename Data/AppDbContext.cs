using Microsoft.EntityFrameworkCore;
using SmartScheduler.Data.Models;

namespace SmartScheduler.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseNpgsql(configuration.GetConnectionString("WebApiDatabase"));
            }
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<ServiceGroup> ServiceGroups { get; set; }
        public DbSet<EmployeeService> EmployeServices { get; set; }
        public DbSet<AppointmentServiceMapping> AppointmentServices { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<AppointmentState> AppointmentStates { get; set; }
    }
}
