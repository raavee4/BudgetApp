using BudgetApp.Common.Exceptions;
using BudgetApp.Data;
using BudgetApp.Infrastructure.Services;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Features.Profile;

public class UpdateProfileHandler : IRequestHandler<UpdateProfileCommand, UpdateProfileResponse>
{
    private readonly AppDbContext _context;
    private readonly IUserService _userService;

    public UpdateProfileHandler(AppDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<UpdateProfileResponse> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var profile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == _userService.UserId, cancellationToken);

        if (profile is null)
        {
            throw new AppValidationException(new Dictionary<string, string[]>
            {
                ["Profile"] = ["Profile not found."]
            });
        }

        profile.MonthlyIncome = request.MonthlyIncome;
        profile.Age = request.Age;
        profile.BudgetFrequency = request.BudgetFrequency;
        profile.ReportFrequency = request.ReportFrequency;
        profile.SavingPreference = request.SavingPreference;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateProfileResponse(
     profile.MonthlyIncome,
     profile.Age,
     profile.BudgetFrequency,
     profile.ReportFrequency,
     profile.SavingPreference);
    }
}
