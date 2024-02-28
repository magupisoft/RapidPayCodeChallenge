using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RapidPay.Data.Repositories;
using RapidPay.Domain.Requests;
using RapidPay.Domain.Responses;
using RapidPay.PaymentFees;

namespace RapidPay.CardManagement;

public class CardManagementService(
    IAccountRepository accountRepository,
    ICardRepository cardRepository,
    IPaymentFeeRepository paymentFeeRepository,
    IConfiguration configuration,
    ILogger<CardManagementService> logger) : ICardManagementService
{
    private readonly string _defaultAccountEmail = configuration["DefaultAccountEmail"];

    public async Task<CreateCardResponse> CreateNewCardAsync(CreateCardRequest request)
    {
        Guid accountId;

        if (request.Account != null)
        {
            accountId = await accountRepository.CreateAccountAsync(request.Account);
        }
        else
        {
            var defaultAccount = await accountRepository.GetAccountAsync(_defaultAccountEmail);
            accountId = defaultAccount != null 
                        ? 
                        defaultAccount.Id 
                        : 
                        throw new ApplicationException("Default account cannot be found");
        }

        return await cardRepository.CreateNewCardAsync(request, accountId);
    }

    public async Task<CardBalanceResponse?> GetCardBalance(string cardNumber)
    {
        decimal? balance = await cardRepository.GetCardBalanceAsync(cardNumber);
        if (balance == null)
        {
            return null;
        }
        return new CardBalanceResponse { Balance = balance.Value, Number = cardNumber };
    }

    public async Task<CardPaymentResponse> ProcessPayment(DoPaymentRequest request)
    {
        logger.LogInformation(
           $"Process a card payment card with number {request.Number} and amount {request.Amount}");
        var currentBalance = await GetCardBalance(request.Number) ?? throw new ApplicationException("There's not balance in the card");

        var feeToPay = await UFEService.Instance.GetPaymentFeeAsync(paymentFeeRepository);

        var totalToBeDiscounted = request.Amount + feeToPay;
        if (currentBalance.Balance - totalToBeDiscounted < 0)
        {
            throw new ApplicationException("There's not enough balance to process this payment");
        }

        var response = new CardPaymentResponse(request.Number);
        if (await cardRepository.SaveTransactionAsync(request.Number, request.Amount, feeToPay, request.Reference))
        {
            await cardRepository.UpdateBalanceAsync(request.Number, request.Amount + feeToPay);
            response.AmountPaid = request.Amount;
            response.FeePaid = feeToPay;
        }

        return response;
    }
}
