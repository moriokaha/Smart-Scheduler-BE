using SmartScheduler.Data;
using SmartScheduler.Data.Models;
using SmartScheduler.Repositories.Contracts;

namespace SmartScheduler.Repositories
{
    public class LocationsRepository(AppDbContext context) : BaseRepository<Location>(context), ILocationsRepository
    {
    }
}
