using SmartScheduler.Data.Models;
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
    }
}
