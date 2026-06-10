using System.ComponentModel.DataAnnotations;
using EmployeeLeaveAPI.Validations;

namespace EmployeeLeaveAPI.DTOs
{
    public class LeaveRequestCreateDto : IValidatableObject
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string EmployeeName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string EmployeeEmail { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter a valid 10-digit Indian mobile number.")]
        public string MobileNumber { get; set; } = string.Empty;

        [Required]
        [ValidLeaveType]
        public string LeaveType { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(250)]
        public string Reason { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate.Date <= DateTime.Today)
            {
                yield return new ValidationResult(
                    "Start date must be a future date.",
                    new[] { nameof(StartDate) });
            }

            if (EndDate.Date <= DateTime.Today)
            {
                yield return new ValidationResult(
                    "End date must be a future date.",
                    new[] { nameof(EndDate) });
            }

            if (EndDate.Date < StartDate.Date)
            {
                yield return new ValidationResult(
                    "End date cannot be before start date.",
                    new[] { nameof(EndDate) });
            }
        }
    }
}