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
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<JobGroup> JobGroups { get; set; }
        public DbSet<EmployeeJob> EmployeJobs { get; set; }
        public DbSet<AppointmentJob> AppointmenTasks { get; set; }
        public DbSet<AppointmentState> AppointmentStates { get; set; }
        public DbSet<AppointmentJob> AppointmentJobs { get; set; }
        public DbSet<LocationWorkInterval> LocationWorkIntervals { get; set; }
        public DbSet<EmployeeWorkInterval> EmployeeWorkIntervals { get; set; }
        public DbSet<EmployeeHolliday> EmployeeHollidays { get; set; }
    }
}
