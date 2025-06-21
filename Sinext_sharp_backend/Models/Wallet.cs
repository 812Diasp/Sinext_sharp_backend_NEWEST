namespace Sinext_sharp_backend.Models;

public class Wallet
{
    public Guid Id { get; set; }
    
    // Ссылка на пользователя
    public Guid UserId { get; set; }

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    public ICollection<GuaranteedIncome> GuaranteedIncomes { get; set; } = new List<GuaranteedIncome>();
    public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
public class GuaranteedIncome
{
    public Guid Id { get; set; }
    public string Source { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Frequency { get; set; } = "monthly"; // daily, weekly, monthly, yearly
    public Guid WalletId { get; set; }
}

public class Stock
{
    public Guid Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public Guid WalletId { get; set; }
}