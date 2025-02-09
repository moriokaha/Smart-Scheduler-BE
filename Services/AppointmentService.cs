using Microsoft.EntityFrameworkCore;
using SmartScheduler.Data;
using SmartScheduler.Data.Models;

namespace SmartScheduler.Services
{
    public class AppointmentService(AppDbContext context, IConfiguration configuration) : IAppointmentService
    {

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return await context.Appointments
                .Include(a => a.Location)
                .Include(a => a.Employee)
                .ToListAsync();
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            Appointment? appointment = await context.Appointments
                .Include(a => a.Location)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.Id == id);

            return appointment;
        }

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            context.Appointments.Add(appointment);
            await context.SaveChangesAsync();
            return appointment;
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            context.Entry(appointment).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            var appointment = await context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                context.Appointments.Remove(appointment);
                await context.SaveChangesAsync();
            }
        }
    }
}
