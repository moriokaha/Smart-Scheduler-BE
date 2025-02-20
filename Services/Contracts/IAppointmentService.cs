using SmartScheduler.Data.Models;

namespace SmartScheduler.Services.Contracts
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<IEnumerable<Appointment>> GetAppointmentsByLocationAsync(string location);
        Task<IEnumerable<Appointment>> GetAppointmentsByUserIdAsync(int userId);
        Task<Appointment> CreateAppointmentAsync(Appointment appointment);
    }
}