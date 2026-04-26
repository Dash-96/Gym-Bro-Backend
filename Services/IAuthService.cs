using GymBro.Models;
using GymBroAspBackend.DTOs;

namespace GymBro.Services
{
    public interface IAuthService
    {
        Task<
        AuthDto> RegisterUserAsync(UserDto request);

        Task<AuthDto> Login(UserDto request);

        Task<string> Refresh(string refreshToken);
    }
}