using BudgetApp.Common.Exceptions;
using BudgetApp.Data;
using BudgetApp.Models;
using BudgetApp.Models.Enums;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace BudgetApp.Features.Authentication.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _context;

    public RegisterHandler(
        UserManager<ApplicationUser> userManager,
        AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .GroupBy(x => x.Code)
                    .ToDictionary(g => g.Key, g => g.Select(x => x.Description).ToArray());

                throw new AppValidationException(errors);
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
            await transaction.CommitAsync(cancellationToken);

            return new RegisterResponse(user.Id, user.Email!);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}