using SmartScheduler.Data.Models;

namespace SmartScheduler.Repositories.Contracts
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetAllForLocationAsync(int locationId);
    }
}
