namespace RapidPay.Domain.Models;

public class Card : BaseEntity
{

    public Card() => Transactions = [];

    public required string Number { get; set; }

    public int ExpirationMonth { get; set; }

    public int ExpirationtYear { get; set; }

    public required string CVC { get; set; }

    public decimal Balance { get; set; }

    public Guid AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; }

}
