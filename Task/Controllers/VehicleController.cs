using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task.Dto;
using Task.Models;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public VehicleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllVehicles")]
        public async Task<IActionResult> GetAllVehicles()
        {
            var vehicles = await _context.vehicles.Include(x => x.category).ToListAsync();
            var vehicleDtos = vehicles.Select(v =>
             new VehicleDto
             {
                 Id = v.Id,
                 Name = v.Name,
                 Brand = v.Brand,
                 Color = v.Color,
                 Year = v.Year,
                 categoryname = v.category.Name,
             }).ToList();
            return Ok(vehicleDtos);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetbyId([FromRoute] int id)
        {
            var vehicle = await _context.vehicles.Include(x => x.category).FirstOrDefaultAsync(x => x.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            var vehicledto = new VehicleDto
            {
                Id = vehicle.Id,
                Name = vehicle.Name,
                Brand = vehicle.Brand,
                Color = vehicle.Color,
                Year = vehicle.Year,
                categoryname = vehicle.category != null ? vehicle.category.Name : null
            };
            return Ok(vehicledto);
        }

        [HttpPost("addvehicle")]
        public async Task<IActionResult> AddVehicleAsync([FromBody] PostVehicleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var vehicle = new Vehicle
            {
                Name = dto.Name,
                Brand = dto.Brand,
                Color = dto.Color,
                Year = dto.Year,
                CategoryId = dto.categoryId,
            };
            try
            {
                _context.vehicles.Add(vehicle);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created($"/api/Vehicle/GetById/{vehicle.Id}", dto);
        }

        [HttpPut("updatevehicle/{id}")]
        public async Task<IActionResult> UpdateVehicleAsync([FromBody] PostVehicleDto dto, int id)
        {
            var vehicle = await _context.vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            vehicle.Name = dto.Name;
            vehicle.Brand = dto.Brand;
            vehicle.Color = dto.Color;
            vehicle.Year = dto.Year;
            vehicle.CategoryId = dto.categoryId;
            try
            {
                _context.vehicles.Update(vehicle);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("edited successfully");
        }

        [HttpDelete("deletevehicle/{id}")]
        public async Task<IActionResult> DeleteVehicleAsync(int id)
        {
            var vehicle = await _context.vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            try
            {
                _context.vehicles.Remove(vehicle);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("deleted successfully");
        }
    }
}