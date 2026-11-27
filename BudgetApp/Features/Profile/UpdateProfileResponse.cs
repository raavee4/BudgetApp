using BudgetApp.Models.Enums;

namespace BudgetApp.Features.Profile;

public record UpdateProfileResponse
(decimal? MonthlyIncome,
    int? Age,
    BudgetFrequency BudgetFrequency,
    ReportFrequency ReportFrequency,
    SavingPreference SavingPreference
);