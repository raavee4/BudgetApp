using BudgetApp.Common.Exceptions;
using BudgetApp.Data;
using BudgetApp.Infrastructure.Services;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Features.Profile;

public class GetProfileHandler : IRequestHandler<GetProfileQuery, GetProfileResponse>
{
    private readonly AppDbContext _context;
    private readonly IUserService _userService;

    public GetProfileHandler(AppDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<GetProfileResponse> Handle(GetProfileQuery query, CancellationToken cancellationToken)
    {
        var profile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == _userService.UserId, cancellationToken);

        if (profile is null)
        {
            throw new AppValidationException(new Dictionary<string, string[]>
            {
                ["Profile"] = ["Profile not found."]
            });
        }

        return new GetProfileResponse(profile.MonthlyIncome, profile.Age, profile.BudgetFrequency, profile.ReportFrequency, profile.SavingPreference);
    }
}
