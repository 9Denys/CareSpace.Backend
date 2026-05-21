using CareSpace.Backend.Domain.Entities;
using System.Security.Claims;

namespace CareSpace.Backend.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
        bool ValidateToken(string token);
        string GetUserIdFromToken(string token);
    }
}