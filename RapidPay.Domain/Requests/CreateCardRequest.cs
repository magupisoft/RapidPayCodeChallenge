namespace RapidPay.Domain.Requests;

public class CreateCardRequest
{
    public required string Number { get; set; }

    public required int ExpirationMonth { get; set; }

    public required int ExpirationtYear { get; set; }

    public required string CVC { get; set; }

    public decimal Balance { get; set; } = 0;

    public AccountRequest? Account { get; set; } = null!;
}

public class AccountRequest
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }
}
