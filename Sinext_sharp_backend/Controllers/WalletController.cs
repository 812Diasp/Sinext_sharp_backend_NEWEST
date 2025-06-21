using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sinext_sharp_backend.Data;
using Sinext_sharp_backend.Dtos;
using Sinext_sharp_backend.Extensions;
using Sinext_sharp_backend.Models;
using System.Globalization;

[ApiController]
[Route("api/wallet")]
[Authorize]
public class WalletController(AppDbContext context) : ControllerBase
{
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetWallet(Guid userId)
    {
        var wallet = await context.Wallets
            .Include(w => w.Transactions)
            .Include(w => w.Loans)
            .Include(w => w.GuaranteedIncomes)
            .Include(w => w.Stocks)
            .FirstOrDefaultAsync(w => w.UserId == userId);

        if (wallet == null) return NotFound();

        return Ok(new
        {
            Transactions = wallet.Transactions.Select(t => t.ToDto()),
            Loans = wallet.Loans.Select(l => l.ToDto()),
            GuaranteedIncomes = wallet.GuaranteedIncomes.Select(g => g.ToDto()),
            Stocks = wallet.Stocks.Select(s => s.ToDto())
        });
    }

    [HttpPost("transaction")]
    public async Task<IActionResult> AddTransaction([FromBody] TransactionDto dto)
    {
        if (dto.WalletId == Guid.Empty)
            return BadRequest("WalletId не указан");

        if (dto.Amount <= 0)
            return BadRequest("Сумма должна быть больше нуля");

        if (string.IsNullOrWhiteSpace(dto.Date))
            return BadRequest("Дата не указана");

        if (!DateTime.TryParseExact(
                dto.Date,
                new[] {
                    "yyyy-MM-ddTHH:mm:ss.fffZ", // ISO UTC
                    "yyyy-MM-ddTHH:mm:ssZ",     // ISO UTC без миллисекунд
                    "yyyy-MM-ddTHH:mm:ss.fffK", // ISO с зоной
                    "yyyy-MM-ddTHH:mm:ssK"      // ISO с зоной без миллисекунд
                },
                CultureInfo.InvariantCulture,
                DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal,
                out var parsedDate))
        {
            return BadRequest("Неверный формат даты. Ожидается ISO 8601, например: '2025-06-20T03:00:00.000Z'");
        }

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Category = dto.Category,
            Amount = dto.Amount,
            Date = parsedDate,
            Type = dto.Type,
            WalletId = dto.WalletId
        };

        await context.Transactions.AddAsync(transaction);
        await context.SaveChangesAsync();

        return Ok(transaction.ToDto());
    }


    [HttpDelete("transaction/{id:guid}")]
    public async Task<IActionResult> DeleteTransaction(Guid id)
    {
        var transaction = await context.Transactions.FindAsync(id);
        if (transaction == null) return NotFound();

        context.Transactions.Remove(transaction);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("loan")]
    public async Task<IActionResult> AddLoan([FromBody] LoanDto dto)
    {
        if (dto.WalletId == Guid.Empty)
            return BadRequest("WalletId не указан");

        if (string.IsNullOrWhiteSpace(dto.StartDate))
            return BadRequest("Дата начала займа не указана");

        if (!DateTime.TryParseExact(
                dto.StartDate,
                new[] {
                    "yyyy-MM-ddTHH:mm:ss.fffZ",
                    "yyyy-MM-ddTHH:mm:ssZ",
                    "yyyy-MM-ddTHH:mm:ss.fffK",
                    "yyyy-MM-ddTHH:mm:ssK"
                },
                CultureInfo.InvariantCulture,
                DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal,
                out var parsedStartDate))
        {
            return BadRequest("Неверный формат даты начала. Ожидается ISO 8601, например: '2025-06-20T00:00:00.000Z'");
        }

        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Principal = dto.Principal,
            InterestRate = dto.InterestRate,
            TermYears = dto.TermYears,
            StartDate = parsedStartDate,
            WalletId = dto.WalletId
        };

        await context.Loans.AddAsync(loan);
        await context.SaveChangesAsync();
        return Ok(loan.ToDto());
    }


    [HttpDelete("loan/{id:guid}")]
    public async Task<IActionResult> DeleteLoan(Guid id)
    {
        var loan = await context.Loans.FindAsync(id);
        if (loan == null) return NotFound();

        context.Loans.Remove(loan);
        await context.SaveChangesAsync();
        return NoContent();
    }
    // Получить все транзакции по кошельку
    [HttpGet("{walletId:guid}/transactions")]
    public async Task<IActionResult> GetTransactions(Guid walletId)
    {
        var transactions = await context.Transactions
            .Where(t => t.WalletId == walletId)
            .ToListAsync();

        return Ok(transactions.Select(t => t.ToDto()));
    }

// Получить все займы по кошельку
    [HttpGet("{walletId:guid}/loans")]
    public async Task<IActionResult> GetLoans(Guid walletId)
    {
        var loans = await context.Loans
            .Where(l => l.WalletId == walletId)
            .ToListAsync();

        return Ok(loans.Select(l => l.ToDto()));
    }

// Получить все гарантированные доходы по кошельку
    [HttpGet("{walletId:guid}/guaranteed-incomes")]
    public async Task<IActionResult> GetGuaranteedIncomes(Guid walletId)
    {
        var incomes = await context.GuaranteedIncomes
            .Where(g => g.WalletId == walletId)
            .ToListAsync();

        return Ok(incomes.Select(g => g.ToDto()));
    }

// Получить все акции по кошельку
    [HttpGet("{walletId:guid}/stocks")]
    public async Task<IActionResult> GetStocks(Guid walletId)
    {
        var stocks = await context.Stocks
            .Where(s => s.WalletId == walletId)
            .ToListAsync();

        return Ok(stocks.Select(s => s.ToDto()));
    }
// POST /api/wallet/guaranteed-income
    [HttpPost("guaranteed-income")]
    public async Task<IActionResult> AddGuaranteedIncome([FromBody] GuaranteedIncomeDto dto)
    {
        if (dto.WalletId == Guid.Empty)
            return BadRequest("WalletId не указан");

        var income = new GuaranteedIncome
        {
            Id = Guid.NewGuid(),
            Source = dto.Source,
            Amount = dto.Amount,
            Frequency = dto.Frequency,
            WalletId = dto.WalletId
        };

        await context.GuaranteedIncomes.AddAsync(income);
        await context.SaveChangesAsync();

        return Ok(income.ToDto());
    }

// DELETE /api/wallet/guaranteed-income/{id}
    [HttpDelete("guaranteed-income/{id:guid}")]
    public async Task<IActionResult> DeleteGuaranteedIncome(Guid id)
    {
        var income = await context.GuaranteedIncomes.FindAsync(id);
        if (income == null) return NotFound();

        context.GuaranteedIncomes.Remove(income);
        await context.SaveChangesAsync();
        return NoContent();
    }

// POST /api/wallet/stock
    [HttpPost("stock")]
    public async Task<IActionResult> AddStock([FromBody] StockDto dto)
    {
        if (dto.WalletId == Guid.Empty)
            return BadRequest("WalletId не указан");

        var stock = new Stock
        {
            Id = Guid.NewGuid(),
            Symbol = dto.Symbol,
            Quantity = dto.Quantity,
            PurchasePrice = dto.PurchasePrice,
            CurrentPrice = dto.CurrentPrice,
            WalletId = dto.WalletId
        };

        await context.Stocks.AddAsync(stock);
        await context.SaveChangesAsync();

        return Ok(stock.ToDto());
    }

// DELETE /api/wallet/stock/{id}
    [HttpDelete("stock/{id:guid}")]
    public async Task<IActionResult> DeleteStock(Guid id)
    {
        var stock = await context.Stocks.FindAsync(id);
        if (stock == null) return NotFound();

        context.Stocks.Remove(stock);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
