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
    public class EmployeesController(IEmployeeService _employeeService, ILocationService _locationService): ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult<Employee>> CreateEmployeeAsync([FromBody] Employee employee)
        {
            if (employee == null || string.IsNullOrWhiteSpace(employee.Name))
            {
                throw new ClientException("Invalid employee data.", HttpStatusCode.BadRequest);
            }

            var location = await _locationService.GetByIdAsync(employee.LocationId);
            if (location == null)
            {
                return NotFound($"Location with ID {employee.LocationId} not found.");
            }

            var createdEmployee = await _employeeService.CreateEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, createdEmployee);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager, User")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeService.GetByIdAsync(id);
                return Ok(employee);
            }
            catch (ClientException ex)
            {
                return StatusCode((int)ex.StatusCode, new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal server error" });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager, User")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllForLocationAsync(int locationId)
        {
            var location = await _locationService.GetByIdAsync(locationId);
            if (location == null)
            {
                return NotFound($"Location with ID {locationId} not found.");
            }

            var employees = await _employeeService.GetAllForLocationAsync(locationId);

            if (employees == null || !employees.Any())
            {
                return NotFound("No employees found for the specified location.");
            }

            return Ok(employees);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (employee == null || employee.Id != id)
            {
                throw new ClientException("Invalid employee data.", HttpStatusCode.BadRequest);
            }

            var updatedEmployee = await _employeeService.UpdateEmployeeByIdAsync(id, employee);
            return Ok(updatedEmployee);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                await _employeeService.DeleteEmployeeByIdAsync(id);
                return NoContent();
            }
            catch (ClientException ex)
            {
                return StatusCode((int)ex.StatusCode, new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal server error" });
            }
        }
    }
}
