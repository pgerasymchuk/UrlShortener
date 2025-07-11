using Application.Auth.Dtos;
using Application.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commands;

public class RegisterUserCommand : IRequest<LoginResultDto>
{
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public bool RegisterAsAdmin { get; set; }
}

public class RegisterUserCommandHandler(
    UserManager<ApplicationUser> userManager, 
    RoleManager<IdentityRole<Guid>> roleManager,
    IMediator mediator)
    : IRequestHandler<RegisterUserCommand, LoginResultDto>
{
    public async Task<LoginResultDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var result = await userManager.CreateAsync(user, request.Password);
        
        var role = (request.RegisterAsAdmin ? Role.Admin : Role.User).ToString();

        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(role));
        }

        await userManager.AddToRoleAsync(user, role);

        var loginCommand = new LoginUserCommand
        {
            Email = request.Email,
            Password = request.Password
        };

        var loginResult = await mediator.Send(loginCommand, cancellationToken);
        return loginResult;
    }
}