namespace RapidPay.Domain.Helper;

public static class PaymentFeeGenerator
{
    private static readonly int max = 2;
    private static readonly int min = 0;

    public static decimal GeneratePaymentFee(decimal lastFee = 0)
    {
        var randomValue = new Random(DateTime.UtcNow.Millisecond);
        var next = randomValue.NextDouble();
        var newFee = (decimal)(next * (max - min));
        return lastFee > 0 ? newFee * lastFee : newFee;
    }
}
