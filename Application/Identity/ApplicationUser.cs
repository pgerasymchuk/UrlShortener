using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public ICollection<Url> Urls { get; set; } = [];
}