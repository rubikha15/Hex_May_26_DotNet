using System.ComponentModel.DataAnnotations;

namespace FleetMaintenanceApi.DTOs;

public class VehicleCreateDto
{
    [Required] public string VehicleNumber { get; set; } = string.Empty;
    [Required] public string VehicleType { get; set; } = string.Empty;
    [Required] public string Brand { get; set; } = string.Empty;
    [Required] public string Model { get; set; } = string.Empty;
    [Range(2001, 2100, ErrorMessage = "Purchase year must be greater than 2000")]
    public int PurchaseYear { get; set; }
    public bool IsActive { get; set; }
}
