using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartScheduler.Data.Models;
using SmartScheduler.Services;
using System.Security.Claims;

namespace SmartScheduler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController(ILogger<AppointmentsController> _logger, IAppointmentService _appointmentsService) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin, User, Manager")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
        {
            try
            {
                IEnumerable<Appointment> appointments;

                if (User.IsInRole("Admin"))
                {
                    // The admin can see all appointments from all locations.
                    appointments = await _appointmentsService.GetAllAppointmentsAsync();
                }
                else if (User.IsInRole("Manager"))
                {
                    // The manager can only see appointments from their location.
                    var userLocation = User.Claims.FirstOrDefault(c => c.Type == "Location")?.Value;
                    if (userLocation == null)
                    {
                        return Unauthorized(new { message = "Location not assigned." });
                    }

                    appointments = await _appointmentsService.GetAppointmentsByLocationAsync(userLocation);
                }
                else if (User.IsInRole("User"))
                {
                    // The user (simple) can only see their own appointments.
                    var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    if (userId == null)
                    {
                        return Unauthorized(new { message = "User ID not found." });
                    }

                    appointments = await _appointmentsService.GetAppointmentsByUserIdAsync(int.Parse(userId));
                }
                else
                {
                    return Unauthorized(new { message = "Access denied." });
                }

                if (appointments == null || appointments.Count() == 0)
                {
                    _logger.LogInformation("No appointments found.");
                    return NotFound(new { message = "No appointments found." });
                }

                return Ok(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving appointments.");
                return StatusCode(500, new { message = "An internal server error occurred." });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Appointment>> CreateAppointment([FromBody] Appointment appointment)
        {
            try
            {
                // Validate appointment data
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var validationError = ValidateAppointment(appointment, userId);

                if (validationError != null)
                {
                    return BadRequest(new { message = validationError });
                }

                // Check if employee exists
                var employee = await _appointmentsService.GetEmployeeByIdAsync(appointment.EmployeeId);
                if (employee == null)
                {
                    return BadRequest(new { message = "Selected employee does not exist." });
                }

                // Check if the location exists
                var location = await _appointmentsService.GetLocationByIdAsync(appointment.Location.Id);
                if (location == null)
                {
                    return BadRequest(new { message = "Selected location does not exist." });
                }

                // Assign UserId if authenticated
                appointment.UserId = userId != null ? int.Parse(userId) : (int?)null;

                // Save the appointment
                var createdAppointment = await _appointmentsService.CreateAppointmentAsync(appointment);

                return CreatedAtAction(nameof(GetAllAppointments), new { id = createdAppointment.Id }, createdAppointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating an appointment.");
                return StatusCode(500, new { message = "An internal server error occurred." });
            }
        }

        /// <summary>
        /// Validates the appointment data before processing.
        /// </summary>
        /// <returns>Null if no errors, otherwise an error message.</returns>
        private string ValidateAppointment(Appointment appointment, string userId)
        {
            if (appointment == null)
            {
                return "Invalid appointment data.";
            }

            if (userId == null && (string.IsNullOrWhiteSpace(appointment.ClientName) || string.IsNullOrWhiteSpace(appointment.ClientPhone)))
            {
                return "Client information was not provided.";
            }

            return null; // No errors
        }
    }
}