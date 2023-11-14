namespace Task.Api.Services.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IVehicleRepository vehicles { get; }

        int Complete();
    }
}
