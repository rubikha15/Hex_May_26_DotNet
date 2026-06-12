using FleetMaintenanceApi.DTOs;
using FleetMaintenanceApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FleetMaintenanceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _service;
    public VehiclesController(IVehicleService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAllVehicles()
    {
        var vehicles = await _service.GetAllVehiclesAsync();
        return Ok(new ApiResponseDto<List<VehicleResponseDto>> { StatusCode = 200, Message = "Vehicles retrieved successfully", Data = vehicles });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetVehicleById(int id)
    {
        if (id <= 0) return BadRequest(new { statusCode = 400, message = "Vehicle id must be greater than zero" });
        var vehicle = await _service.GetVehicleByIdAsync(id);
        if (vehicle == null) return NotFound(new { statusCode = 404, message = "Vehicle not found" });
        return Ok(new ApiResponseDto<VehicleResponseDto> { StatusCode = 200, Message = "Vehicle retrieved successfully", Data = vehicle });
    }

    [HttpPost]
    public async Task<IActionResult> AddVehicle(VehicleCreateDto dto)
    {
        var result = await _service.AddVehicleAsync(dto);
        if (!result.Success) return BadRequest(new { statusCode = 400, message = result.Message });
        return Ok(new ApiResponseDto<VehicleResponseDto> { StatusCode = 200, Message = result.Message, Data = result.Data });
    }
}
