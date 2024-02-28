namespace RapidPay.Domain.Models;

public class Transaction : BaseEntity
{
    public decimal Amount { get; set; }

    public decimal PaymentFee { get; set; }

    public DateTime PaymentDate { get; set; }

    public string? Reference { get; set; }

    public Guid CardId { get; set; }

    public virtual Card Card { get; set; } = null!;
}
