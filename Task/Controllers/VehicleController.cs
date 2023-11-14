using Microsoft.AspNetCore.Mvc;
using Task.Api.Dto;
using Task.Api.Models;
using Task.Api.Services.Interfaces;

namespace Task.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public VehicleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllVehicles")]
        public async Task<IActionResult> GetAllVehicles()
        {
            var vehicles = await _unitOfWork.vehicles.FindAll(new[] { "category" });
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
            var vehicle = await _unitOfWork.vehicles.FindVehicle(id);
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
            return Ok(vehicledto);//200
        }

        [HttpPost("addvehicle")]
        public async Task<IActionResult> AddVehicleAsync([FromBody] PostVehicleDto dto)// route header body
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
                _unitOfWork.vehicles.Add(vehicle);
                _unitOfWork.Complete();  // apply to database
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
            var vehicle = await _unitOfWork.vehicles.GetById(id);
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
                _unitOfWork.vehicles.Update(vehicle);
                _unitOfWork.Complete();
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
            var vehicle = await _unitOfWork.vehicles.GetById(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            try
            {
                _unitOfWork.vehicles.Delete(vehicle);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("deleted successfully");
        }
    }
}