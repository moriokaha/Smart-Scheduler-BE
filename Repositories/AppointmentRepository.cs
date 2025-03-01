using Microsoft.EntityFrameworkCore;
using SmartScheduler.Data;
using SmartScheduler.Data.Models;
using SmartScheduler.Exceptions;
using SmartScheduler.Repositories.Contracts;
using System.Net;

namespace SmartScheduler.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByLocationAsync(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                throw new ClientException("Invalid location name", HttpStatusCode.BadRequest);
            }

            return await Context.Appointments
                .Include(a => a.Location)
                .Where(a => a.Location.Name == location)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ClientException("Invalid user id", HttpStatusCode.BadRequest);
            }

            return await Context.Appointments
                                 .Include(a => a.Location)
                                 .Include(a => a.Employee)
                                 .Where(a => a.EmployeeId == userId)
                                 .ToListAsync();
        }
    }
}
