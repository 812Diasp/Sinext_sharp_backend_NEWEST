namespace Sinext_sharp_backend.Models;

public class Loan
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Principal { get; set; }
    public decimal InterestRate { get; set; } // годовой %
    public int TermYears { get; set; }
    public DateTime StartDate { get; set; }
    public Guid WalletId { get; set; }
}