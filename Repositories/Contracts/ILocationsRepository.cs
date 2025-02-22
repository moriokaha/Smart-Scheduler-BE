using SmartScheduler.Data.Models;

namespace SmartScheduler.Repositories.Contracts
{
    public interface ILocationsRepository 
    {
        Task<IEnumerable<Location>> GetAllAsync();
        Task<Location?> GetByIdAsync(int locationId);
        Task<Location> CreateAsync(Location location);
        Task<Location> UpdateAsync(Location location);
        Task DeleteAsync(int locationId);
    }
}
