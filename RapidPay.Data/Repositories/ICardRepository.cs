using RapidPay.Domain.Models;
using RapidPay.Domain.Requests;
using RapidPay.Domain.Responses;

namespace RapidPay.Data.Repositories;

public interface ICardRepository
{
    Task<CreateCardResponse> CreateNewCardAsync(CreateCardRequest request, Guid accountId);

    Task<decimal?> GetCardBalanceAsync(string cardNumber);

    Task<bool> SaveTransactionAsync(string cardNumber, decimal amount, decimal fee, string? reference);

    Task<bool> UpdateBalanceAsync(string cardNumber, decimal amount);

    Task<Card> GetCardAsync(string cardNumber);
}
