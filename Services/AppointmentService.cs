using SmartScheduler.Data.Models;
using SmartScheduler.Repositories.Contracts;
using SmartScheduler.Services.Contracts;

namespace SmartScheduler.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _appointmentRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByLocationAsync(string location)
        {
            return await _appointmentRepository.GetAppointmentsByLocationAsync(location);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByUserIdAsync(int userId)
        {
            return await _appointmentRepository.GetAppointmentsByUserIdAsync(userId);
        }

        public async Task<Appointment> CreateAsync(Appointment appointment)
        {
            return await _appointmentRepository.CreateAsync(appointment);
        }

        public async Task<Appointment> UpdateAsync(Appointment appointment)
        {
            return await _appointmentRepository.UpdateAsync(appointment);
        }

        public async Task DeleteAsync(int appointmentId)
        {
            await _appointmentRepository.DeleteByIdAsync(appointmentId);
        }
    }
}