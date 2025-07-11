using Application.Auth.Dtos;
using Application.Common.Exceptions;
using Application.Identity;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commands;

public class LoginUserCommand : IRequest<LoginResultDto>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class LoginUserCommandHandler(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    ITokenService tokenService)
    : IRequestHandler<LoginUserCommand, LoginResultDto?>
{
    public async Task<LoginResultDto?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new BadCredentialsException("Invalid email or password.");
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
        {
            throw new BadCredentialsException("Invalid email or password.");
        }

        var roles = await userManager.GetRolesAsync(user);
        var accessToken = tokenService.GenerateAccessToken(user, roles);

        return new LoginResultDto
        {
            AccessToken = accessToken,
        };
    }
}