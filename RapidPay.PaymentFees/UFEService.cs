using RapidPay.Data.Repositories;
using RapidPay.Domain.Helper;

namespace RapidPay.PaymentFees;

public sealed class UFEService
{
    private static readonly Lazy<UFEService> lazy = new(() => new UFEService());

    public static UFEService Instance { get { return lazy.Value; } }

    private UFEService()
    {
    }

    public async Task<decimal> GetPaymentFeeAsync(IPaymentFeeRepository repository)
    {
        var (currentFee, lastUpdated) = await repository.GetLastPaymentFee();
        decimal paymentFee = currentFee;
        if ((lastUpdated - DateTime.UtcNow).TotalHours > 1)
        {
            var newFee = PaymentFeeGenerator.GeneratePaymentFee(paymentFee);
            await repository.CreateNewPaymentFee(newFee);
            paymentFee = newFee;
        }

        return paymentFee;
    }
}
