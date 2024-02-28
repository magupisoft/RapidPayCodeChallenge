namespace RapidPay.Domain.Requests;

public class DoPaymentRequest
{
    public string Number { get; set; } = null!;

    public decimal Amount { get; set; }

    public string? Reference { get; set; }
}
