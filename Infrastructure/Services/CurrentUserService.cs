using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly ClaimsPrincipal? _user = httpContextAccessor.HttpContext?.User;

    public Guid? UserId
    {
        get
        {
            var userIdClaim = _user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userIdClaim, out var id) ? id : null;
        }
    }

    public bool IsAdmin => _user?.IsInRole("Admin") == true;
}
