using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sinext_sharp_backend.Data;
using Sinext_sharp_backend.Models;

[ApiController]
[Route("api/wallet")]
[Authorize]
public class WalletController : ControllerBase
{
    private readonly AppDbContext _context;

    public WalletController(AppDbContext context) => _context = context;

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetWallet(Guid userId)
    {
        var wallet = await _context.Wallets
            .Include(w => w.Transactions)
            .Include(w => w.Loans)
            .FirstOrDefaultAsync(w => w.Id == userId);

        if (wallet == null) return NotFound();

        return Ok(wallet);
    }

    [HttpPost("transaction")]
    public async Task<IActionResult> AddTransaction(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("loan")]
    public async Task<IActionResult> AddLoan(Loan loan)
    {
        await _context.Loans.AddAsync(loan);
        await _context.SaveChangesAsync();
        return Ok();
    }
}