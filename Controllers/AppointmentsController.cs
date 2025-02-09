using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartScheduler.Data.Models;
using SmartScheduler.Services;

namespace SmartScheduler.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly ILogger<AppointmentsController> _logger;
        private readonly IAppointmentService _appointmentsService;

        public AppointmentsController(ILogger<AppointmentsController> logger, IAppointmentService appointmentsService)
        {
            _logger = logger;
            _appointmentsService = appointmentsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
        {
            try
            {
                var appointments = await _appointmentsService.GetAllAppointmentsAsync();
                if (appointments == null || appointments.Count == 0)
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

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Test endpoint working");
        }
    }
}