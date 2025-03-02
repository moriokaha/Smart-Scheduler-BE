using SmartScheduler.Data.Models;
using SmartScheduler.Exceptions;
using SmartScheduler.Repositories.Contracts;
using SmartScheduler.Services.Contracts;

namespace SmartScheduler.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return employees;
        }

        public async Task<Employee> GetByIdAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllForLocationAsync(int locationId)
        {
            var employees = await _employeeRepository.GetAllForLocationAsync(locationId);
            return employees;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            if (employee == null || string.IsNullOrWhiteSpace(employee.Name))
            {
                throw new ArgumentException("Invalid employee data.");
            }

            var createdEmployee = await _employeeRepository.CreateAsync(employee);
            return createdEmployee;
        }

        public async Task<Employee> UpdateEmployeeByIdAsync(int employeeId, Employee updatedEmployee)
        {
            if (updatedEmployee == null || employeeId != updatedEmployee.Id)
            {
                throw new ArgumentException("Invalid employee data.");
            }

            var existingEmployee = await _employeeRepository.GetByIdAsync(employeeId);
            if (existingEmployee == null)
            {
                throw new ClientException($"Employee with ID {employeeId} not found.", System.Net.HttpStatusCode.NotFound);
            }

            var employee = await _employeeRepository.UpdateAsync(updatedEmployee);
            return employee;
        }

        public async Task DeleteEmployeeByIdAsync(int employeeId)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(employeeId);
            if (existingEmployee == null)
            {
                throw new ClientException($"Employee with ID {employeeId} not found.", System.Net.HttpStatusCode.NotFound);
            }

            await _employeeRepository.DeleteByIdAsync(employeeId);
        }
    }
}
