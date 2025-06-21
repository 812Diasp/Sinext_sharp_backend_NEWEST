namespace Sinext_sharp_backend.Models;
public class Transaction
{
    public Guid Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } // хранится как DateTime
    public string Type { get; set; } = "expense";
    public Guid WalletId { get; set; }
}