using BudgetApp.Models.Enums;

using MediatR;

namespace BudgetApp.Features.Profile;

public record UpdateProfileCommand(
    decimal? MonthlyIncome,
    int? Age,
    BudgetFrequency BudgetFrequency,
    ReportFrequency ReportFrequency,
    SavingPreference SavingPreference
    ) : IRequest<UpdateProfileResponse>;