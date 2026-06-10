using EmployeeLeaveAPI.DTOs;
using EmployeeLeaveAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestsController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        [HttpPost]
        public IActionResult CreateLeaveRequest([FromBody] LeaveRequestCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var leaveRequest = _leaveRequestService.CreateLeaveRequest(dto);

            return CreatedAtAction(
                nameof(GetLeaveRequestById),
                new { id = leaveRequest.LeaveRequestId },
                leaveRequest);
        }

        [HttpGet]
        public IActionResult GetAllLeaveRequests()
        {
            var leaveRequests = _leaveRequestService.GetAllLeaveRequests();

            return Ok(leaveRequests);
        }

        [HttpGet("{id}")]
        public IActionResult GetLeaveRequestById(int id)
        {
            var leaveRequest = _leaveRequestService.GetLeaveRequestById(id);

            if (leaveRequest == null)
            {
                return NotFound("Leave request not found.");
            }

            return Ok(leaveRequest);
        }
        [HttpPut("{id}/approve")]
        public IActionResult ApproveLeave(int id)
        {
            var leaveRequest = _leaveRequestService.ApproveLeave(id);

            if (leaveRequest == null)
            {
                return NotFound("Leave request not found.");
            }

            return Ok(leaveRequest);
        }

        [HttpPut("{id}/reject")]
        public IActionResult RejectLeave(int id)
        {
            var leaveRequest = _leaveRequestService.RejectLeave(id);

            if (leaveRequest == null)
            {
                return NotFound("Leave request not found.");
            }

            return Ok(leaveRequest);
        }
    }
}