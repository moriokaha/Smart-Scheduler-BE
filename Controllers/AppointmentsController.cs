using Microsoft.AspNetCore.Mvc;
using SmartScheduler.Data.Models;
using SmartScheduler.Services;


namespace SmartScheduler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly ILogger<AppointmentsController> _logger;
        private readonly IAppointmentService _appointmentsService;

        public AppointmentsController(ILogger<AppointmentsController> logger, IAppointmentService appointmentsService)
        {
            _logger = logger;
            _appointmentsService = appointmentsService;
        }

        [HttpGet(Name = "GetAppointments")]
        public async Task<ActionResult<IEnumerable<Appointment>>> Get()
        {
            var appointments = await _appointmentsService.GetAllAppointmentsAsync();
            if (appointments == null || appointments.Count == 0)
            {
                _logger.LogInformation("No appointments found.");
                return NotFound("No appointments found.");
            }

            return Ok(appointments);
        }
    }
}