using BudgetApp.Models;

namespace BudgetApp.Infrastructure.Services;

public interface IJwtService
{
    string GenerateAccessToken(ApplicationUser user);
    string GenerateRefreshToken();
}
