using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Application.Interfaces
{
    public interface IUserContextService
    {
        Guid? GetCurrentUserId();
        ClaimsPrincipal? GetCurrentUser();
        bool IsAuthenticated();

    }
}
