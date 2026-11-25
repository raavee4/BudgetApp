using BudgetApp.Common.Exceptions;
using BudgetApp.Infrastructure.Configuration;
using BudgetApp.Infrastructure.Services;
using BudgetApp.Models;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BudgetApp.Features.Authentication.Login;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;
    private readonly JwtOptions _jwtOptions;
    public LoginHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService, IOptions<JwtOptions> jwtOptions)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new AppValidationException(new Dictionary<string, string[]> { ["Authentication"] = ["Invalid email or password."] });
        }

        var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isValidPassword)
        {
            throw new AppValidationException(new Dictionary<string, string[]> { ["Authentication"] = ["Invalid email or password."] });
        }

        var accessToken = _jwtService.GenerateAccessToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        return new LoginResponse(accessToken, refreshToken, DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenMinutes));
    }
}
