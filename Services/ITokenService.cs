using GymBro.Models;

public interface ITokenService
{
    string GenerateToken(User request);
    string GenerateRefreshToken();
}