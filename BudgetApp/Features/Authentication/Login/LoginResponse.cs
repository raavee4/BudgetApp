namespace BudgetApp.Features.Authentication.Login;

public record LoginResponse(string Token,
     string RefreshToken,
     DateTime Expiration);
