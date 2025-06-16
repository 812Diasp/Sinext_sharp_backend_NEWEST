namespace Sinext_sharp_backend.Models;

public class Transaction
{
    public Guid Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; } = "expense"; // "income" | "expense"
    public Guid WalletId { get; set; }
}