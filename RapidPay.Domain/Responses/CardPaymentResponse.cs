namespace RapidPay.Domain.Responses;

public class CardPaymentResponse(string number)
{
    public string CardNumber { get; set; } = number;

    public decimal AmountPaid { get; set; }

    public decimal FeePaid { get; set; }
}
