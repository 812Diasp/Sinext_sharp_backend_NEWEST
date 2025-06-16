namespace Sinext_sharp_backend.Models;

public class Wallet
{
    public Guid Id { get; set; }
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}