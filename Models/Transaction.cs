namespace BudgetApp.Models;

public class Transaction
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int StatementId { get; set; }
    public int CategoryId { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public bool IsManuallyEntered { get; set; }
    public bool IsReviewed { get; set; }
    public bool RequiresReview { get; set; }
    public decimal? AiConfidenceScore { get; set; }
    public Statement Statement { get; set; }
    public Category Category { get; set; }
    public ApplicationUser User { get; set; }
}
