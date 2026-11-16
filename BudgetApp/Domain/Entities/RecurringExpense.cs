namespace BudgetApp.Models;

public class RecurringExpense
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public int DayOfMonth { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}
