namespace RapidPay.Data.Repositories;

public interface IPaymentFeeRepository
{
    Task<bool> CreateNewPaymentFee(decimal fee);

    Task<(decimal currentFee, DateTime lastUpdated)> GetLastPaymentFee();
}
