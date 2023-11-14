using Task.Api.Models;
using Task.Api.Services.Interfaces;

namespace Task.Api.Services.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IVehicleRepository vehicles { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            vehicles = new VehicleRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
