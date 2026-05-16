using System.Security.Claims;
using GymBro.Services;
using GymBroAspBackend.DTOs;
using GymBroAspBackend.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GymBro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService service)
        {
            _authService = service;
        }
        [HttpPost("register")]
        public async Task<ActionResult<AuthDto>> Register([FromBody] UserDto request)
        {
            var tokenDto = await _authService.RegisterUserAsync(request);
            return Ok(tokenDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthDto>> Login([FromBody] UserDto request)
        {
            var authDto = await _authService.Login(request);
            return authDto;
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<string>> GetNewAccessToken([FromBody] string refreshToken)
        {
            return Ok(await _authService.Refresh(refreshToken));
        }

        [HttpGet]
        [Authorize]
        public async Task<string> AuthPointTest()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine(userId);
            return "authorized";
        }

        [HttpGet("users")]
        public async Task<IEnumerable<McpUser>> GetUsers()
        {
            var users = await _authService.GetUsers();
            return users;
        }

    }
}