using Microsoft.EntityFrameworkCore;
namespace Task.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleServices>()
                .HasKey(e => new { e.ServiceId, e.vechicleId });
        }

        public DbSet<Vehicle> vehicles { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<VehicleServices> vehicleServices { get; set; }
    }
}
