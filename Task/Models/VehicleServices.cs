using System.ComponentModel.DataAnnotations;

namespace Task.Models
{
    public class VehicleServices
    {
        [Key]
        public int vechicleId { get; set; }
        public int ServiceId { get; set; }
        public Vehicle vehicle { get; set; }
        public Service service { get; set; }
        public DateTime ServiceDate { get; set; }
        public double Cost { get; set; }
    }
}
