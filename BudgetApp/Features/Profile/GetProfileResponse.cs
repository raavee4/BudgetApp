using BudgetApp.Models.Enums;

namespace BudgetApp.Features.Profile;

public record GetProfileResponse(
    decimal? MonthlyIncome,
    int? Age,
    BudgetFrequency BudgetFrequency,
    ReportFrequency ReportFrequency,
    SavingPreference SavingPreference
    );
