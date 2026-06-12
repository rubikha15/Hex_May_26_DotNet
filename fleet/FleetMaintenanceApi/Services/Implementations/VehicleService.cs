using FleetMaintenanceApi.DTOs;
using FleetMaintenanceApi.Models;
using FleetMaintenanceApi.Repositories.Interfaces;
using FleetMaintenanceApi.Services.Interfaces;
namespace FleetMaintenanceApi.Services.Implementations;
public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _repo;
    public VehicleService(IVehicleRepository repo) => _repo = repo;
    public async Task<List<VehicleResponseDto>> GetAllVehiclesAsync() => (await _repo.GetAllVehiclesAsync()).Select(Map).ToList();
    public async Task<VehicleResponseDto?> GetVehicleByIdAsync(int vehicleId) => (await _repo.GetVehicleByIdAsync(vehicleId)) is { } v ? Map(v) : null;
    public async Task<(bool Success, string Message, VehicleResponseDto? Data)> AddVehicleAsync(VehicleCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.VehicleNumber)) return (false, "Vehicle number is required", null);
        if (string.IsNullOrWhiteSpace(dto.VehicleType)) return (false, "Vehicle type is required", null);
        if (string.IsNullOrWhiteSpace(dto.Brand)) return (false, "Brand is required", null);
        if (dto.PurchaseYear <= 2000) return (false, "Purchase year must be greater than 2000", null);
        var vehicle = new Vehicle { VehicleNumber = dto.VehicleNumber, VehicleType = dto.VehicleType, Brand = dto.Brand, Model = dto.Model, PurchaseYear = dto.PurchaseYear, IsActive = dto.IsActive };
        await _repo.AddVehicleAsync(vehicle);
        return (true, "Vehicle added successfully", Map(vehicle));
    }
    private static VehicleResponseDto Map(Vehicle v) => new() { VehicleId = v.VehicleId, VehicleNumber = v.VehicleNumber, VehicleType = v.VehicleType, Brand = v.Brand, Model = v.Model, PurchaseYear = v.PurchaseYear, IsActive = v.IsActive };
}
