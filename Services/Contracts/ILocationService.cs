using SmartScheduler.Data.Models;

namespace SmartScheduler.Services.Contracts
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAllAsync();
        Task<Location> GetByIdAsync(int locationId);
        Task<Location> CreateLocationAsync(Location location);
        Task<Location> UpdateLocationAsync(Location location);
        Task DeleteLocationAsync(int locationId);
    }
}
