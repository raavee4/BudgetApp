using BudgetApp.Models.Enums;

namespace BudgetApp.Models;

public class Statement
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string StatementAlias { get; set; }
    public string OriginalFileName { get; set; }
    public DateTime UploadedAt { get; set; }
    public StatementStatus StatementStatus { get; set; }
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    public ApplicationUser User { get; set; }
}
