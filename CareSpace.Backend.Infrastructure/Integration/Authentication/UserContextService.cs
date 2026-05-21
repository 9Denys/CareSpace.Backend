using CareSpace.Backend.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CareSpace.Backend.Infrastructure.Integration.Authentication
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? GetCurrentUserId()
        {
            var claim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return null;

            return Guid.TryParse(claim.Value, out var id) ? id : null;
        }

        public ClaimsPrincipal? GetCurrentUser()
        {
            return _httpContextAccessor.HttpContext?.User;
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }
    }
}