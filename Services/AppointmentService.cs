using Microsoft.EntityFrameworkCore;
using SmartScheduler.Data;
using SmartScheduler.Data.Models;

namespace SmartScheduler.Services
{
    public class AppointmentService(AppDbContext _context) : IAppointmentService
    {
        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments.Include(a => a.Location).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByLocationAsync(string location)
        {
            return await _context.Appointments
                .Include(a => a.Location)
                .Where(a => a.Location.Name == location)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByUserIdAsync(int userId)
        {
            return await _context.Appointments
                                 .Include(a => a.Location)
                                 .Include(a => a.Employee)
                                 .Where(a => a.EmployeeId == userId)
                                 .ToListAsync();
        }
    }
}