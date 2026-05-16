using System.Security.Claims;
using GymBro.Dto;
using GymBro.Models;
using GymBro.Services;
using GymBroAspBackend.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GymBro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private IHubContext<NotificationHub> _hubContext;
        private INotificationService _notificationService;
        public NotificationController(IHubContext<NotificationHub> hubContext, INotificationService service)
        {
            _hubContext = hubContext;
            _notificationService = service;
        }

        [Authorize]
        [HttpGet("friend-request")]
        public async Task SendFriendRequest([FromQuery] int targetId)
        {
            var senderName = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var senderId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _notificationService.SendFriendRequest(int.Parse(senderId) , targetId);
            await _hubContext.Clients.User(targetId.ToString()).SendAsync("FriendRequest", new { senderName, message = "wants to connect" });
        }

        [Authorize]
        [HttpGet("view-requests")]
        public async Task<ActionResult<List<NotificationDto>>> GetFriendRequestsById()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var requests = await _notificationService.GetFriendRequestsAsync(int.Parse(userId));
            return Ok(requests);
        }
    }
}