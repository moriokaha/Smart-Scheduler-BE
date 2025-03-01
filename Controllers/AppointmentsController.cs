using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartScheduler.Data.Models;
using SmartScheduler.Exceptions;
using SmartScheduler.Helpers;
using SmartScheduler.Services.Contracts;
using System.Net;

namespace SmartScheduler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController(ILogger<AppointmentsController> _logger, IAppointmentService _appointmentsService, IEmployeeService _employeeService, ILocationService _locationService) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin, User, Manager")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
        {
            IEnumerable<Appointment> appointments;
            var tokenUserData = HttpHelper.GetUserDataFromToken(HttpContext.Request);

            if (tokenUserData.Role == UserRole.Admin)
            {
                // The admin can see all appointments from all locations.
                appointments = await _appointmentsService.GetAllAsync();
            }
            else if (tokenUserData.Role == UserRole.Manager)
            {
                // The manager can only see appointments from their location.
                var userLocation = User.Claims.FirstOrDefault(c => c.Type == "Location")?.Value;
                
                if (userLocation == null)
                {
                    throw new ClientException("Location not assigned.", HttpStatusCode.Unauthorized);
                }

                appointments = await _appointmentsService.GetAppointmentsByLocationAsync(userLocation);
            }
            else if (tokenUserData.Role == UserRole.User)
            {
                appointments = await _appointmentsService.GetAppointmentsByUserIdAsync(tokenUserData.Id);
            }
            else
            {
                throw new ClientException("Access denied.", HttpStatusCode.Unauthorized);
            }

            if (appointments == null || appointments.Count() == 0)
            {
                throw new ClientException("No appointments found.", HttpStatusCode.NotFound);
            }

            return Ok(appointments);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Appointment>> CreateAppointment([FromBody] Appointment appointment)
        {
            try
            {
                // Validate appointment data
                var tokenUserData = HttpHelper.GetUserDataFromToken(HttpContext.Request);
                var validationError = ValidateAppointment(appointment);

                if (validationError != null)
                {
                    throw new ClientException(validationError, HttpStatusCode.BadRequest);
                }

                // Check if employee exists
                var employee = await _employeeService.GetByIdAsync(appointment.EmployeeId);
                if (employee == null)
                {
                    throw new ClientException("Selected employee does not exist.", HttpStatusCode.BadRequest);
                }

                // Check if the location exists
                var location = await _locationService.GetByIdAsync(appointment.Location.Id);
                if (location == null)
                {
                    throw new ClientException("Selected location does not exist.", HttpStatusCode.BadRequest);
                }

                // Assign UserId if authenticated
                appointment.UserId = tokenUserData.Id;

                // Save the appointment
                var createdAppointment = await _appointmentsService.CreateAsync(appointment);

                return CreatedAtAction(nameof(GetAllAppointments), new { id = createdAppointment.Id }, createdAppointment);
            }
            catch (Exception ex)
            {
                throw new ClientException("An internal server error occurred.", HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Validates the appointment data before processing.
        /// </summary>
        /// <returns>Null if no errors, otherwise an error message.</returns>
        private static string? ValidateAppointment(Appointment appointment)
        {
            if (appointment == null)
            {
                return "Invalid appointment data.";
            }

            if (string.IsNullOrWhiteSpace(appointment.ClientName) || string.IsNullOrWhiteSpace(appointment.ClientPhone))
            {
                return "Client information was not provided.";
            }

            return null; // No errors
        }
    }
}