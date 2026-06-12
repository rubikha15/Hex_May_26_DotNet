using System.ComponentModel.DataAnnotations;

namespace FleetMaintenanceApi.DTOs;

public class DriverCreateDto
{
    [Required] public string DriverName { get; set; } = string.Empty;
    [Required] public string LicenseNumber { get; set; } = string.Empty;
    [Required] public string PhoneNumber { get; set; } = string.Empty;
    [Required] public string City { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
}
