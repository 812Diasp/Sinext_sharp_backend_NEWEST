using Microsoft.EntityFrameworkCore;
using Sinext_sharp_backend.Models;

namespace Sinext_sharp_backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<GuaranteedIncome> GuaranteedIncomes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=walletdb;Username=postgres;Password=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.Wallet)
            .WithOne()
            .HasForeignKey<User>(u => u.WalletId);
    }
}