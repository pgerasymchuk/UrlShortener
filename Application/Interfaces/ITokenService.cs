using Application.Identity;

namespace Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(ApplicationUser user, IEnumerable<string> roles);
}