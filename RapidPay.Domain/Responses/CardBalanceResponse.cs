namespace RapidPay.Domain.Responses;

public class CardBalanceResponse
{
    public string Number { get; set; } = null!;
    public decimal Balance { get; set; }
}
