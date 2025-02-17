﻿using Microsoft.EntityFrameworkCore;
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
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId) ?? throw new Exception("Employee not found.");
            return employee;
        }

        public async Task<Location> GetLocationByIdAsync(int locationId)
        {
            var location = await _context.Locations.FirstOrDefaultAsync(l => l.Id == locationId);

            return location ?? throw new Exception("Location not found.");
        }

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            var employee = await GetEmployeeByIdAsync(appointment.EmployeeId);
            var location = await GetLocationByIdAsync(appointment.Location.Id);

            appointment.Location = location;
            appointment.Employee = employee;

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }
    }
}