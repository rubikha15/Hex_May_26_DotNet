using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveAPI.Validations
{
    public class ValidLeaveTypeAttribute : ValidationAttribute
    {
        private readonly string[] _allowedLeaveTypes =
        {
            "Sick",
            "Casual",
            "Earned"
        };

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Leave type is required.");
            }

            string leaveType = value.ToString()!;

            if (!_allowedLeaveTypes.Contains(leaveType))
            {
                return new ValidationResult("Leave type must be Sick, Casual, or Earned.");
            }

            return ValidationResult.Success;
        }
    }
}