namespace RapidPay.Domain.Models;

public class Account : BaseEntity
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public string? PhoneNumber { get; set; } = null;

    public string? Address { get; set; } = null;

    public DateTime CardHolderSince { get; set; }

    public Guid? CardId { get; set; }

    public virtual Card? Card { get; set; } = null!;
}
