using Microsoft.EntityFrameworkCore;
using SmartScheduler.Data;
using SmartScheduler.Data.Models;
using SmartScheduler.Exceptions;
using SmartScheduler.Repositories.Contracts;
using System.Net;

namespace SmartScheduler.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context): base(context) { }

        public async Task<IEnumerable<Employee>> GetAllForLocationAsync(int locationId)
        {
            if (locationId <= 0)
            {
                throw new ClientException("Invalid location id", HttpStatusCode.BadRequest);
            }

            var employees = await Context.Employees.Where(e => e.LocationId == locationId).Include(e => e.Location).ToListAsync();
            return employees;
        }
    }
}
