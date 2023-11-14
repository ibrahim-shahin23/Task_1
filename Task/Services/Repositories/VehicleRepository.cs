using Microsoft.EntityFrameworkCore;
using Task.Api.Models;
using Task.Api.Services.Interfaces;

namespace Task.Api.Services.Repositories
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        private readonly ApplicationDbContext _context;
        public VehicleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Vehicle> FindVehicle(int id)
        {
            var query = await _context.vehicles.Include(x => x.category).FirstOrDefaultAsync(x => x.Id == id);
            return query;
        }
    }
}
