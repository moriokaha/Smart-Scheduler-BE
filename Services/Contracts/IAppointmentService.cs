using SmartScheduler.Data.Models;

namespace SmartScheduler.Services.Contracts
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<IEnumerable<Appointment>> GetAppointmentsByLocationAsync(string location);
        Task<IEnumerable<Appointment>> GetAppointmentsByUserIdAsync(int userId);
        Task<Appointment> CreateAsync(Appointment appointment);
        Task<Appointment> UpdateAsync(Appointment appointment);
        Task DeleteAsync(int appointmentId);
    }
}