using SmartScheduler.Data.Models;

namespace SmartScheduler.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<IEnumerable<Appointment>> GetAppointmentsByLocationAsync(string location);
        Task<IEnumerable<Appointment>> GetAppointmentsByUserIdAsync(int userId);
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task<Location> GetLocationByNameAsync(string locationName);
        Task<Appointment> CreateAppointmentAsync(Appointment appointment);
    }
}