using Microsoft.EntityFrameworkCore;
using SmartScheduler.Data;
using SmartScheduler.Data.Models;
using SmartScheduler.Repositories.Contracts;

namespace SmartScheduler.Repositories
{
    public class LocationsRepository : BaseRepository<Location>, ILocationsRepository
    {
        public LocationsRepository(AppDbContext context) : base(context) { }

        public new async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await Context.Locations.ToListAsync();
        }

        public new async Task<Location?> GetByIdAsync(int locationId)
        {
            return await Context.Locations.FirstOrDefaultAsync(l => l.Id == locationId);
        }

        public new async Task<Location> CreateAsync(Location location)
        {
            Context.Locations.Add(location);
            await Context.SaveChangesAsync();
            return location;
        }

        public new async Task<Location> UpdateAsync(Location location)
        {
            Context.Locations.Update(location);
            await Context.SaveChangesAsync();
            return location;
        }

        public async Task DeleteAsync(int locationId)
        {
            var location = await Context.Locations.FindAsync(locationId);
            if (location != null)
            {
                Context.Locations.Remove(location);
                await Context.SaveChangesAsync();
            }
        }
    }
}
