using System.Security.Claims;

namespace BudgetApp.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException();
    public string Email => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? throw new UnauthorizedAccessException();

}
