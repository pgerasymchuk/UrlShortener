using Application.Identity;

namespace Application.Extensions;

public static class RoleExtensions
{
    public static string ToString(this Role r)
    {
        return r switch
        {
            Role.Admin => "Admin",
            Role.User => "User",
            _ => "User"
        };
    }
}
