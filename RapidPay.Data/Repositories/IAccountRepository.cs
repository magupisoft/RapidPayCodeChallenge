using RapidPay.Domain.Models;
using RapidPay.Domain.Requests;

namespace RapidPay.Data.Repositories;

public interface IAccountRepository
{
    Task<Guid> CreateAccountAsync(AccountRequest request);
    Task<Account> GetAccountAsync(string email);
}
