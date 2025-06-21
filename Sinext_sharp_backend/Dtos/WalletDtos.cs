namespace Sinext_sharp_backend.Dtos;

public class TransactionDto
{
    public Guid Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Date { get; set; } = string.Empty; // приходит как строка ISO8601
    public string Type { get; set; } = "expense";
    public Guid WalletId { get; set; }
}

public class LoanDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Principal { get; set; }
    public decimal InterestRate { get; set; }
    public int TermYears { get; set; }
    public string StartDate { get; set; } = string.Empty;
    public Guid WalletId { get; set; }
}


public class GuaranteedIncomeDto
{
    public Guid Id { get; set; }
    public string Source { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Frequency { get; set; } = "monthly";
    public Guid WalletId { get; set; }
}

public class StockDto
{
    public Guid Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public Guid WalletId { get; set; }
}
