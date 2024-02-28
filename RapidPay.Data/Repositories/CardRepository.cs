using Microsoft.EntityFrameworkCore;
using RapidPay.Domain.Models;
using RapidPay.Domain.Requests;
using RapidPay.Domain.Responses;

namespace RapidPay.Data.Repositories;

public class CardRepository(RapidPayDbContext context) : ICardRepository
{
    public async Task<CreateCardResponse> CreateNewCardAsync(CreateCardRequest request, Guid accountId)
    {
        var newCard = new Card
        {
            Number = request.Number,
            ExpirationMonth = request.ExpirationMonth,
            ExpirationtYear = request.ExpirationtYear,
            CVC = request.CVC,
            Balance = request.Balance,
            AccountId = accountId
        };
        context.Add(newCard);
        await context.SaveChangesAsync();

        return new CreateCardResponse() { Number = newCard.Number };
    }

    public async Task<decimal?> GetCardBalanceAsync(string cardNumber)
    {
        var card = await GetCardAsync(cardNumber);
        if (card == null)
        {
            return null;
        }
        return card.Balance;
    }

    public async Task<bool> SaveTransactionAsync(string cardNumber, decimal payment, decimal fee, string? reference)
    {
        var card = await GetCardAsync(cardNumber);
        if (card == null)
        {
            return false;
        }
        var payTransaction = new Transaction
        {
            Amount = payment,
            PaymentFee = fee,
            PaymentDate = DateTime.UtcNow,
            CardId = card.Id,
            Reference = reference
        };
        context.Add(payTransaction);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateBalanceAsync(string cardNumber, decimal amount)
    {
        var card = await GetCardAsync(cardNumber);
        if (card == null)
        {
            return false;
        }
        card.Balance -= amount;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Card> GetCardAsync(string cardNumber) =>
        await context.Cards.FirstOrDefaultAsync(x => x.Number == cardNumber);
}
