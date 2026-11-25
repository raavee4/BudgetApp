using MediatR;

namespace BudgetApp.Features.Authentication.Login;

public record LoginCommand(
    string Email,
    string Password
    ) : IRequest<LoginResponse>;
