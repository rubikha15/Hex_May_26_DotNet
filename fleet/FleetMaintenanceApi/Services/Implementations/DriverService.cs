using FleetMaintenanceApi.DTOs;
using FleetMaintenanceApi.Models;
using FleetMaintenanceApi.Repositories.Interfaces;
using FleetMaintenanceApi.Services.Interfaces;
namespace FleetMaintenanceApi.Services.Implementations;
public class DriverService : IDriverService
{
    private readonly IDriverRepository _repo;
    public DriverService(IDriverRepository repo) => _repo = repo;
    public async Task<List<DriverResponseDto>> GetAllDriversAsync() => (await _repo.GetAllDriversAsync()).Select(Map).ToList();
    public async Task<DriverResponseDto?> GetDriverByIdAsync(int driverId) => (await _repo.GetDriverByIdAsync(driverId)) is { } d ? Map(d) : null;
    public async Task<(bool Success, string Message, DriverResponseDto? Data)> AddDriverAsync(DriverCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.DriverName)) return (false, "Driver name is required", null);
        if (string.IsNullOrWhiteSpace(dto.LicenseNumber)) return (false, "License number is required", null);
        if (string.IsNullOrWhiteSpace(dto.PhoneNumber)) return (false, "Phone number is required", null);
        if (string.IsNullOrWhiteSpace(dto.City)) return (false, "City is required", null);
        var driver = new Driver { DriverName = dto.DriverName, LicenseNumber = dto.LicenseNumber, PhoneNumber = dto.PhoneNumber, City = dto.City, IsAvailable = dto.IsAvailable };
        await _repo.AddDriverAsync(driver);
        return (true, "Driver added successfully", Map(driver));
    }
    private static DriverResponseDto Map(Driver d) => new() { DriverId = d.DriverId, DriverName = d.DriverName, LicenseNumber = d.LicenseNumber, PhoneNumber = d.PhoneNumber, City = d.City, IsAvailable = d.IsAvailable };
}
