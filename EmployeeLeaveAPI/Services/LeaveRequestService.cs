using EmployeeLeaveAPI.DTOs;
using EmployeeLeaveAPI.Models;

namespace EmployeeLeaveAPI.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly List<LeaveRequest> _leaveRequests = new();
        private int _nextId = 1;

        public LeaveRequestResponseDto CreateLeaveRequest(LeaveRequestCreateDto dto)
        {
            int totalDays = (dto.EndDate.Date - dto.StartDate.Date).Days + 1;

            var leaveRequest = new LeaveRequest
            {
                LeaveRequestId = _nextId++,
                EmployeeName = dto.EmployeeName,
                EmployeeEmail = dto.EmployeeEmail,
                MobileNumber = dto.MobileNumber,
                LeaveType = dto.LeaveType,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Reason = dto.Reason,
                TotalDays = totalDays,
                Status = "Pending",
                CreatedOn = DateTime.Now
            };

            _leaveRequests.Add(leaveRequest);

            return MapToResponseDto(leaveRequest);
        }

        public List<LeaveRequestResponseDto> GetAllLeaveRequests()
        {
            return _leaveRequests
                .Select(MapToResponseDto)
                .ToList();
        }

        public LeaveRequestResponseDto? GetLeaveRequestById(int id)
        {
            var leaveRequest = _leaveRequests
                .FirstOrDefault(x => x.LeaveRequestId == id);

            if (leaveRequest == null)
            {
                return null;
            }

            return MapToResponseDto(leaveRequest);
        }

        private LeaveRequestResponseDto MapToResponseDto(LeaveRequest leaveRequest)
        {
            return new LeaveRequestResponseDto
            {
                LeaveRequestId = leaveRequest.LeaveRequestId,
                EmployeeName = leaveRequest.EmployeeName,
                EmployeeEmail = leaveRequest.EmployeeEmail,
                LeaveType = leaveRequest.LeaveType,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                Reason = leaveRequest.Reason,
                TotalDays = leaveRequest.TotalDays,
                Status = leaveRequest.Status,
                CreatedOn = leaveRequest.CreatedOn
            };
        }
        public LeaveRequestResponseDto? ApproveLeave(int id)
        {
            var leaveRequest = _leaveRequests
                .FirstOrDefault(x => x.LeaveRequestId == id);

            if (leaveRequest == null)
            {
                return null;
            }

            leaveRequest.Status = "Approved";

            return MapToResponseDto(leaveRequest);
        }

        public LeaveRequestResponseDto? RejectLeave(int id)
        {
            var leaveRequest = _leaveRequests
                .FirstOrDefault(x => x.LeaveRequestId == id);

            if (leaveRequest == null)
            {
                return null;
            }

            leaveRequest.Status = "Rejected";

            return MapToResponseDto(leaveRequest);
        }
    }
}