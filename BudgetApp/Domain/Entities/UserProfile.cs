using BudgetApp.Models.Enums;
namespace BudgetApp.Models;

public class UserProfile
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public decimal? MonthlyIncome { get; set; }
    public int? Age { get; set; }
    public SavingPreference SavingPreference { get; set; }
    public BudgetFrequency BudgetFrequency { get; set; }
    public ReportFrequency ReportFrequency { get; set; }
    public ApplicationUser User { get; set; }
}
