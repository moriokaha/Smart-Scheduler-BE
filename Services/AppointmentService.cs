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

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
            {
                throw new Exception("Employee not found.");
            }

            return employee;
        }

        public async Task<Location> GetLocationByNameAsync(string locationName)
        {
            var location = await _context.Locations.FirstOrDefaultAsync(l => l.Name == locationName);

            if (location == null)
            {
                throw new Exception("Location not found.");
            }

            return location;
        }

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            var employee = await GetEmployeeByIdAsync(appointment.EmployeeId);

            var location = await GetLocationByNameAsync(appointment.Location.Name);

            appointment.Location = location;
            appointment.Employee = employee;

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }
    }
}