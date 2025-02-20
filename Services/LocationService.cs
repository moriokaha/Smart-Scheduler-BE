using Microsoft.EntityFrameworkCore;
using SmartScheduler.Data;
using SmartScheduler.Data.Models;
using SmartScheduler.Services.Contracts;

namespace SmartScheduler.Services
{
    public class LocationService(AppDbContext _context) : ILocationService
    {
        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            var locations = await _context.Locations.ToListAsync();
            return locations;
        }

        public async Task<Location> GetByIdAsync(int locationId)
        {
            var location = await _context.Locations.FirstOrDefaultAsync(l => l.Id == locationId) ?? throw new Exception("Location not found.");
            return location;
        }
    }
}
