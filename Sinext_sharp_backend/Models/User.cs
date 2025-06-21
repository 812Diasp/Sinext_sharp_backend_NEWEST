namespace Sinext_sharp_backend.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; } = false;

    // Навигационное свойство
    public Guid WalletId { get; set; }
    public Wallet Wallet { get; set; } = null!;
}