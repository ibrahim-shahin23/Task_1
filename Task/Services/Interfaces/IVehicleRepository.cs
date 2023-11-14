using Task.Api.Models;

namespace Task.Api.Services.Interfaces
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        Task<Vehicle> FindVehicle(int id);
    }
}
