using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GymBro.Models;
using GymBroAspBackend.DTOs;
using Microsoft.IdentityModel.Tokens;


namespace GymBroAspBackend.Services
{
    public class TokenService : ITokenService
    {
        private IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim(ClaimTypes.Name , user.DisplayName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(30);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var accessToken =  new JwtSecurityTokenHandler().WriteToken(token);
            return accessToken;
        }

    
        public string GenerateRefreshToken()
        {
            var randomNumber = new Byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}