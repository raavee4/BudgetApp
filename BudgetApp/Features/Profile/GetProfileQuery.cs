using MediatR;

namespace BudgetApp.Features.Profile;

public record GetProfileQuery() : IRequest<GetProfileResponse>;
