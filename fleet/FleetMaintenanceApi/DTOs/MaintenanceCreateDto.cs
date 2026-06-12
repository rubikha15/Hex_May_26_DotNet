using System.ComponentModel.DataAnnotations;

namespace FleetMaintenanceApi.DTOs;

public class MaintenanceCreateDto
{
    [Range(1, int.MaxValue)] public int VehicleId { get; set; }
    [Range(1, int.MaxValue)] public int DriverId { get; set; }
    [Required] public DateOnly ServiceDate { get; set; }
    [Required] public string ServiceType { get; set; } = string.Empty;
    [Range(0.01, double.MaxValue, ErrorMessage = "Service cost must be greater than 0")]
    public decimal ServiceCost { get; set; }
    [Required] public string ServiceStatus { get; set; } = string.Empty;
    public string? Remarks { get; set; }
}
