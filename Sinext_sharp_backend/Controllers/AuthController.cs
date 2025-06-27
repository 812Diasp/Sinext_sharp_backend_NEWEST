using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sinext_sharp_backend.Data;
using Sinext_sharp_backend.Dtos;
using Sinext_sharp_backend.Models;
using Sinext_sharp_backend.Services;

namespace Sinext_sharp_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;
    private readonly EmailService _emailService;
    public AuthController(AppDbContext context, TokenService tokenService, EmailService emailService)
    {
        _context = context;
        _tokenService = tokenService;
        _emailService = emailService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
            return BadRequest("Пользователь уже существует");

        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return BadRequest("Email уже используется");

        if (dto.Password != dto.ConfirmPassword)
            return BadRequest("Пароли не совпадают");

        // Генерируем ID пользователя и кошелька
        var userId = Guid.NewGuid();
        var walletId = Guid.NewGuid();

        // Создаём кошелёк
        var wallet = new Wallet
        {
            Id = walletId,
            UserId = userId,  // <-- Привязываем к пользователю
            Transactions = new List<Transaction>(),
            Loans = new List<Loan>(),
            GuaranteedIncomes = new List<GuaranteedIncome>(),
            Stocks = new List<Stock>()
        };

        // Создаём пользователя
        var user = new User
        {
            Id = userId,
            Username = dto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Email = dto.Email,
            EmailConfirmed = false,
            WalletId = walletId, // <-- Указываем WalletId
            Wallet = wallet      // <-- Можно оставить, можно и без этого, если будете загружать по навигации
        };

        // Добавляем оба объекта
        await _context.Wallets.AddAsync(wallet);
        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();

        // Отправка подтверждения email
        var confirmationLink = $"http://localhost:3000/api/auth/confirm-email?userId={user.Id}";
        var emailBody = $"<h3>Подтвердите ваш email</h3><p><a href='{confirmationLink}'>Нажмите здесь</a>, чтобы подтвердить регистрацию.</p>";
        await _emailService.SendEmailAsync(user.Email, "Подтверждение почты", emailBody);

        return Ok(new AuthResponseDto
        {
            Token = _tokenService.GenerateToken(user),
            UserId = user.Id
        });
    }
    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return NotFound();

        user.EmailConfirmed = true;
        await _context.SaveChangesAsync();

        return Redirect("http://localhost:3000/pages/login"); // или страница успеха
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _context.Users
            .Include(u => u.Wallet)
            .FirstOrDefaultAsync(u => u.Username == dto.UsernameOrEmail || u.Email == dto.UsernameOrEmail);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized("Неверные учетные данные");

        if (!user.EmailConfirmed)
            return StatusCode(403, "Email не подтвержден");

        return Ok(new AuthResponseDto
        {
            Token = _tokenService.GenerateToken(user),
            UserId = user.Id,
            WalletId = user.WalletId
        });
    }
}