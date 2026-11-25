using MediatR;

namespace BudgetApp.Features.Authentication.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    string ConfirmPassword
    ) : IRequest<RegisterResponse>;
