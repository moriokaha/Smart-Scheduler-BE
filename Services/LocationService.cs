using SmartScheduler.Data.Models;
using SmartScheduler.Repositories.Contracts;
using SmartScheduler.Services.Contracts;

namespace SmartScheduler.Services
{
    public class LocationService(ILocationsRepository _repository) : ILocationService
    {
        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
         
        public async Task<Location> GetByIdAsync(int locationId)
        {
            return await _repository.GetByIdAsync(locationId) ?? throw new Exception("Location not found.");
        }

        public async Task<Location> CreateLocationAsync(Location location)
        {
            ValidateLocation(location);
            return await _repository.CreateAsync(location);
        }

        public async Task<Location> UpdateLocationAsync(Location location)
        {
            var existingLocation = await _repository.GetByIdAsync(location.Id);
            if (existingLocation == null)
            {
                throw new Exception("Location not found.");
            }

            ValidateLocation(location);
            return await _repository.UpdateAsync(location);
        }

        public async Task DeleteLocationAsync(int locationId)
        {
            var location = await _repository.GetByIdAsync(locationId);
            if (location == null)
            {
                throw new Exception("Location not found.");
            }

            await _repository.DeleteAsync(locationId);
        }

        private static void ValidateLocation(Location location)
        {
            if (string.IsNullOrWhiteSpace(location.Name))
            {
                throw new Exception("Location name is required.");
            }

            if (location.Latitude < -90 || location.Latitude > 90)
            {
                throw new Exception("Latitude must be between -90 and 90.");
            }

            if (location.Longitude < -180 || location.Longitude > 180)
            {
                throw new Exception("Longitude must be between -180 and 180.");
            }
        }
    }
}
