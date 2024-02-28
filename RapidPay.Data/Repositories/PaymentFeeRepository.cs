using Microsoft.EntityFrameworkCore;
using RapidPay.Domain.Models;

namespace RapidPay.Data.Repositories;

public class PaymentFeeRepository(RapidPayDbContext context) : IPaymentFeeRepository
{
    public async Task<bool> CreateNewPaymentFee(decimal fee)
    {
        var paymentFee = new PaymentFee
        {
            Fee = fee
        };
        context.Add(paymentFee);
        try
        {
            await context.SaveChangesAsync();
        }
        catch
        {
            return false;
        }

        return true;
    }

    public async Task<(decimal currentFee, DateTime lastUpdated)> GetLastPaymentFee()
    {
        var paymentFee = await context.PaymentFees
                                    .OrderBy(x => x.Created)
                                    .Take(1)
                                    .SingleOrDefaultAsync();
        if (paymentFee == null)
        {
            return default;
        }
        return (paymentFee.Fee, paymentFee.Created);
    }
}
