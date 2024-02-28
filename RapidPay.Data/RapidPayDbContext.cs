using Microsoft.EntityFrameworkCore;
using RapidPay.Domain.Helper;
using RapidPay.Domain.Models;

namespace RapidPay.Data;

public class RapidPayDbContext(DbContextOptions<RapidPayDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }

    public DbSet<Card> Cards { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<PaymentFee> PaymentFees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Account>()
            .HasKey(k => k.Id);
        modelBuilder.Entity<Account>()
            .Property(k => k.Created)
            .HasDefaultValueSql("getutcdate()");
        modelBuilder.Entity<Account>()
            .Property(k => k.Updated)
            .HasComputedColumnSql("getutcdate()");
        modelBuilder.Entity<Account>()
            .Property(k => k.Email).IsRequired();
        modelBuilder.Entity<Account>()
            .Property(k => k.Password).IsRequired();

        modelBuilder.Entity<Card>()
            .HasKey(k => k.Id);
        modelBuilder.Entity<Card>()
            .Property(k => k.Created)
            .HasDefaultValueSql("getutcdate()");
        modelBuilder.Entity<Card>()
            .Property(k => k.Updated)
            .HasComputedColumnSql("getutcdate()");
        modelBuilder.Entity<Card>()
            .Property(k => k.Number)
            .HasMaxLength(15);
        modelBuilder.Entity<Card>()
            .Property(k => k.Number)
            .IsRequired();
        modelBuilder.Entity<Card>()
            .Property(k => k.Balance)
            .IsRequired();

        modelBuilder.Entity<Transaction>()
            .HasKey(k => k.Id);
        modelBuilder.Entity<Transaction>()
            .Property(k => k.Created)
            .HasDefaultValueSql("getutcdate()");
        modelBuilder.Entity<Transaction>()
            .Property(k => k.Updated)
            .HasComputedColumnSql("getutcdate()");
        modelBuilder.Entity<Transaction>()
            .Property(k => k.Amount)
            .IsRequired();
        modelBuilder.Entity<Transaction>()
            .Property(k => k.PaymentDate)
            .IsRequired();

        modelBuilder.Entity<PaymentFee>()
            .HasKey(k => k.Id);
        modelBuilder.Entity<PaymentFee>()
            .Property(k => k.Created)
            .HasDefaultValueSql("getutcdate()");
        modelBuilder.Entity<PaymentFee>()
            .Property(k => k.Updated)
            .HasComputedColumnSql("getutcdate()");

        modelBuilder.Entity<Account>()
           .HasOne(k => k.Card)
           .WithOne(k => k.Account)
           .HasForeignKey<Card>(k => k.AccountId)
           .IsRequired(false);

        modelBuilder.Entity<Card>()
          .HasMany(k => k.Transactions)
          .WithOne(k => k.Card)
          .HasForeignKey(k => k.CardId)
          .HasPrincipalKey(k => k.Id);

        // Seed initial Data.
        var demoAccountId = Guid.NewGuid();
        modelBuilder.Entity<Account>().HasData(
            new Account {
                Id = Guid.NewGuid(), 
                FirstName = "SysAdmin", 
                LastName = "RapidPay",
                Email = "admin@payrapid.io", 
                Password = "Password1!",
                CardHolderSince = DateTime.UtcNow
            },
            new Account
            {
                Id = demoAccountId,
                FirstName = "Demo",
                LastName = "Demo",
                Email = "demo@payrapid.io",
                Password = "R@pidPay!",
                CardHolderSince = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<Card>().HasData(
           new Card
           {
               Id = Guid.NewGuid(),
               Number = "422856726227993",
               ExpirationMonth = 12,
               ExpirationtYear = 2026,
               CVC = "481",
               Balance = 10000,
               AccountId = demoAccountId
           });

        modelBuilder.Entity<PaymentFee>().HasData(
          new PaymentFee
          {
              Id = Guid.NewGuid(),
              Fee = PaymentFeeGenerator.GeneratePaymentFee()
          });
    }
}
