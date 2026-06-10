using EmployeeLeaveAPI.DTOs;

namespace EmployeeLeaveAPI.Services
{
    public interface ILeaveRequestService
    {
        LeaveRequestResponseDto CreateLeaveRequest(LeaveRequestCreateDto dto);

        List<LeaveRequestResponseDto> GetAllLeaveRequests();

        LeaveRequestResponseDto? GetLeaveRequestById(int id);

        LeaveRequestResponseDto? ApproveLeave(int id);

        LeaveRequestResponseDto? RejectLeave(int id);
    }
}