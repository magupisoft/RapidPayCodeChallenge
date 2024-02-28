using Microsoft.EntityFrameworkCore;
using RapidPay.Domain.Models;
using RapidPay.Domain.Requests;

namespace RapidPay.Data.Repositories;

public class AccountRepository(RapidPayDbContext context) : IAccountRepository
{    
    public async Task<Guid> CreateAccountAsync(AccountRequest request)
    {
        var account = await GetAccountAsync(request.Email ?? string.Empty);
        if(account != null)
        {
            return account.Id;
        }

        var newAccount = new Account {
            FirstName = request.FirstName ?? string.Empty,
            LastName = request.LastName ?? string.Empty,
            Email = request.Email ?? string.Empty,
            Password = request.Password ?? string.Empty,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            CardHolderSince = DateTime.UtcNow,
        };
        context.Add(newAccount);
        await context.SaveChangesAsync();

        return newAccount.Id;
    }

    public async Task<Account> GetAccountAsync(string email) => 
        await context.Accounts.FirstOrDefaultAsync(x => x.Email == email);
}
