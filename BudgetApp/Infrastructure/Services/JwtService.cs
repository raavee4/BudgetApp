using System.Security.Claims;
using System.Text;

using BudgetApp.Infrastructure.Configuration;
using BudgetApp.Models;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BudgetApp.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly JwtOptions _jwtOptions;

    public JwtService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateAccessToken(ApplicationUser user)
    {
        // Get Key
        var key = _jwtOptions.Key;
        // Convert to bytes.
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
        // Create creds
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        // Build claims
        var claims = new ClaimsIdentity([
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!)
            ]);

        // Create Token
        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            Subject = claims,
            Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenMinutes),
            SigningCredentials = credentials
        };
        var handler = new JsonWebTokenHandler();

        return handler.CreateToken(descriptor);
    }

    public string GenerateRefreshToken()
    {
        return "";
    }
}
