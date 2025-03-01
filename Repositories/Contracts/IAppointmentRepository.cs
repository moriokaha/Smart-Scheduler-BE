using SmartScheduler.Data.Models;

namespace SmartScheduler.Repositories.Contracts
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAppointmentsByLocationAsync(string location);
        Task<IEnumerable<Appointment>> GetAppointmentsByUserIdAsync(int userId);
    }
}
