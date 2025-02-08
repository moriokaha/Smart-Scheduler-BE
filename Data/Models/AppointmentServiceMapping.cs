namespace SmartScheduler.Data.Models
{
    public class AppointmentServiceMapping
    {
        public int Id { get; set; }
        public required  int AppointmentId { get; set; } // FK to Appointments table
        public required int EmployeeServiceId { get; set; } // FK to EmployeeServices table
        public required virtual Appointment Appointment { get; set; }
        public required virtual EmployeeService EmployeeService { get; set; }
    }
}