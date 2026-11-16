namespace BudgetApp.Models;

public class BudgetAlerts
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsRead { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}
