using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartScheduler.Data.Models;
using SmartScheduler.Exceptions;
using SmartScheduler.Services.Contracts;
using System.Net;

namespace SmartScheduler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController(ILogger<LocationsController> _logger, ILocationService _locationService) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin, Manager, User")]
        public async Task<ActionResult<IEnumerable<Location>>> GetAllLocations()
        {
            var locations = await _locationService.GetAllAsync();

            if (locations == null || !locations.Any())
            {
                throw new ClientException("No locations found.", HttpStatusCode.NotFound);
            }

            return Ok(locations);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager, User")]
        public async Task<ActionResult<Location>> GetLocationById(int id)
        {
            var location = await _locationService.GetByIdAsync(id);

            if (location == null)
            {
                throw new ClientException("Location not found.", HttpStatusCode.NotFound);
            }

            return Ok(location);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Location>> CreateLocation([FromBody] Location location)
        {
            if (location == null || string.IsNullOrWhiteSpace(location.Name))
            {
                throw new ClientException("Invalid location data.", HttpStatusCode.BadRequest);
            }

            var createdLocation = await _locationService.CreateLocationAsync(location);
            return CreatedAtAction(nameof(GetLocationById), new { id = createdLocation.Id }, createdLocation);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> UpdateLocation(int id, [FromBody] Location location)
        {
            if (location == null || id != location.Id)
            {
                throw new ClientException("Invalid location data.", HttpStatusCode.BadRequest);
            }

            var updatedLocation = await _locationService.UpdateLocationAsync(location);
            return Ok(updatedLocation);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            await _locationService.DeleteLocationAsync(id);
            return NoContent();
        }
    }
}
