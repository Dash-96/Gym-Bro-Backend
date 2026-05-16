
using System.Security.Authentication;
using GymBro.Models;
using GymBroAspBackend.DTOs;
using GymBroAspBackend.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymBro.Services
{
    public class AuthService : IAuthService
    {
        private ITokenService _tokenService;
        private GymBroDbContext _context;
        public AuthService(ITokenService service, GymBroDbContext context)
        {
            _tokenService = service;
            _context = context;
        }


        //= If the refresh token was expired , perform a new login for the user, and generate a new access and refresh tokens
        public async Task<AuthDto> Login(UserDto request)
        {
            var response = new AuthDto();
            var user = await _context.Users.Where(u => u.DisplayName == request.DisplayName).FirstOrDefaultAsync();
            if (user != null)
            {
                var passwordCheck = new PasswordHasher<User>().VerifyHashedPassword(user, user.HashedPassword, request.Password);
                if (passwordCheck == PasswordVerificationResult.Success)
                {
                    response.AccessToken = _tokenService.GenerateToken(user);
                    response.RefreshToken = _tokenService.GenerateRefreshToken();
                    return response;
                }
                else
                {
                    throw new InvalidCredentialException();
                }
            }
            else
            {
                throw new InvalidCredentialException();
            }
        }

        //= Genreate a new access token if the previous one is expired
        public async Task<string> Refresh(string refreshToken)
        {
            var user = _context.Users.Where(u => u.RefreshToken == refreshToken).First();
            var now = DateTime.Now.ToUniversalTime();
            if (user != null)
            {
                /// The user has the refresh token and can be issued new access token
                if (user.RefreshTokenExpiery >= now)
                {
                    var accessToken = _tokenService.GenerateToken(user);
                    return accessToken;

                }
                /// The user's refresh token is expired and needs to login again
                else
                {
                    throw new RefreshTokenExpiredException();
                }
            }
            /// The user doesnt exist any more or the refresh token is not valid
            else
            {
                throw new InvalidCredentialException();
            }
        }

        // //= Make a quick sign in for the user to skip the login screen if he still has access token in
        // public async Task SignIn()

        public async Task<AuthDto> RegisterUserAsync(UserDto request)
        {
            var tokenDto = new AuthDto();
            try
            {
                var user = new User
            {
                Phone = request.Phone,
                DisplayName = request.DisplayName
            };
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);
            user.HashedPassword = hashedPassword;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var accessToken = _tokenService.GenerateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            tokenDto = new AuthDto() { RefreshToken = refreshToken, AccessToken = accessToken };
            user.RefreshToken = tokenDto.RefreshToken;
            user.RefreshTokenExpiery = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            return tokenDto;
        }

        public async Task<IEnumerable<McpUser>> GetUsers()
        {
            var users = await _context.Users.Select(user => new McpUser
            {
                Id= user.Id,
                DisplayName= user.DisplayName
            }).ToListAsync();

            return users;
        }
    }
}