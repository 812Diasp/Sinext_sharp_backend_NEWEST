namespace Sinext_sharp_backend.Extensions;

using Sinext_sharp_backend.Dtos;
using Sinext_sharp_backend.Models;

public static class MappingExtensions
{
    public static TransactionDto ToDto(this Transaction transaction) => new()
    {
        Id = transaction.Id,
        Category = transaction.Category,
        Amount = transaction.Amount,
        Date = transaction.Date.ToString("o"), // конвертируем в ISO 8601 строку
        Type = transaction.Type
    };

    public static LoanDto ToDto(this Loan loan) => new()
    {
        Id = loan.Id,
        Name = loan.Name,
        Principal = loan.Principal,
        InterestRate = loan.InterestRate,
        TermYears = loan.TermYears,
        StartDate = loan.StartDate.ToString("o") // тоже в строку
    };

    public static GuaranteedIncomeDto ToDto(this GuaranteedIncome income) => new()
    {
        Id = income.Id,
        Source = income.Source,
        Amount = income.Amount,
        Frequency = income.Frequency
    };

    public static StockDto ToDto(this Stock stock) => new()
    {
        Id = stock.Id,
        Symbol = stock.Symbol,
        Quantity = stock.Quantity,
        PurchasePrice = stock.PurchasePrice,
        CurrentPrice = stock.CurrentPrice
    };
}