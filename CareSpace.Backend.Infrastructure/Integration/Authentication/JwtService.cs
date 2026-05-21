using CareSpace.Backend.Domain.Entities;
using CareSpace.Backend.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CareSpace.Backend.Infrastructure.Integration.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly TokenValidationParameters _tokenValidationParameters;

        private static byte[] GetJwtKeyBytes(IConfiguration configuration)
        {
            var key = configuration["Jwt:Key"];
            if (string.IsNullOrWhiteSpace(key))
                throw new InvalidOperationException("JWT key 'Jwt:Key' is missing or empty.");

            return Encoding.UTF8.GetBytes(key);
        }

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;

            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(GetJwtKeyBytes(_configuration)),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),

                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],

                NameClaimType = ClaimTypes.NameIdentifier,
                RoleClaimType = ClaimTypes.Role
            };
        }

        public string GenerateAccessToken(User user)
        {
            var key = new SymmetricSecurityKey(GetJwtKeyBytes(_configuration));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiryMinutesStr = _configuration["Jwt:ExpiryInMinutes"];
            if (!double.TryParse(expiryMinutesStr, out var expiryMinutes))
                expiryMinutes = 60;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            RandomNumberGenerator.Fill(bytes);
            return Convert.ToBase64String(bytes);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tvp = _tokenValidationParameters.Clone();
            tvp.ValidateLifetime = false;

            try
            {
                var principal = tokenHandler.ValidateToken(token, tvp, out var securityToken);

                if (securityToken is not JwtSecurityToken jwt ||
                    !jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    return null;

                return principal;
            }
            catch
            {
                return null;
            }
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, _tokenValidationParameters, out _);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetUserIdFromToken(string token)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            return principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
