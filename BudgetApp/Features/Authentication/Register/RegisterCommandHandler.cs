using BudgetApp.Data;
using BudgetApp.Models;
using BudgetApp.Models.Enums;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace BudgetApp.Features.Authentication.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _context;

    public RegisterCommandHandler(
        UserManager<ApplicationUser> userManager,
        AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Temporary validation, replace with FluentValidation later
        if (request.Password != request.ConfirmPassword)
        {
            throw new Exception("Passwords do not match.");
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(x => x.Description)));
        }

        var profile = new UserProfile
        {
            UserId = user.Id,
            BudgetFrequency = BudgetFrequency.BiWeekly,
            ReportFrequency = ReportFrequency.Monthly,
            SavingPreference = SavingPreference.Normal
        };

        _context.UserProfiles.Add(profile);
        await _context.SaveChangesAsync(cancellationToken);

        return new RegisterResponse
        {
            UserId = user.Id,
            Email = user.Email
        };

    }
}
