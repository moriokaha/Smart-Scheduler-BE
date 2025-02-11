using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartScheduler.Data.Models;
using SmartScheduler.Services;
using System.Security.Claims;

namespace SmartScheduler.Controllers
{
    [Authorize(Roles = "Admin, User, Manager")]
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController(ILogger<AppointmentsController> _logger, IAppointmentService _appointmentsService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
        {
            try
            {
                IEnumerable<Appointment> appointments;

                if (User.IsInRole("Admin"))
                {
                    // Admin poate vedea toate programările din toate locațiile
                    appointments = await _appointmentsService.GetAllAppointmentsAsync();
                }
                else if (User.IsInRole("Manager"))
                {
                    // Manager poate vedea doar programările din locația sa
                    var userLocation = User.Claims.FirstOrDefault(c => c.Type == "Location")?.Value;
                    if (userLocation == null)
                    {
                        return Unauthorized(new { message = "Location not assigned." });
                    }

                    appointments = await _appointmentsService.GetAppointmentsByLocationAsync(userLocation);
                }
                else if (User.IsInRole("User"))
                {
                    // User (simplu) poate vedea doar programările sale
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
    }
}