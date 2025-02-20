using SmartScheduler.Data.Models;

namespace SmartScheduler.Services.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<IEnumerable<Employee>> GetAllForLocationAsync(int locationId);
        Task<Employee> GetByIdAsync(int employeeId);
    }
}
