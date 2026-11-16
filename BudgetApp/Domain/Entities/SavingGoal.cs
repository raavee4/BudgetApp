namespace BudgetApp.Models;

public class SavingGoal
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal CurrentAmount { get; set; }
    public DateTime? TargetDate { get; set; }
    public ApplicationUser User { get; set; }
}
