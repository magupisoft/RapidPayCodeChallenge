using System.ComponentModel.DataAnnotations;
using FluentValidation;
using RapidPay.Domain.Requests;

namespace RapidPay.Domain.Validators;

public class CreateCardValidator : AbstractValidator<CreateCardRequest>
{
    public CreateCardValidator()
    {
        RuleFor(request => request.Number)
           .Must(x => !string.IsNullOrWhiteSpace(x))
           .WithMessage("Card number es required");
        RuleFor(x => x.Number)
            .Must(x => new RegularExpressionAttribute("(\\d{15})$")
            .IsValid(x?.TrimEnd()))
            .WithMessage("Card number must be 15 digits");
        RuleFor(request => request.ExpirationMonth)
           .InclusiveBetween(1, 12)
           .WithMessage("Expiration month must be between 1 and 12");

        RuleFor(request => request.ExpirationtYear)
            .InclusiveBetween(DateTime.UtcNow.Year, DateTime.UtcNow.AddYears(25).Year)
            .WithMessage("Expiration year must be a valid year");

        RuleFor(request => request.CVC)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Card CVC es required");
        RuleFor(x => x.CVC)
            .Must(x => new RegularExpressionAttribute("(\\d{3})$")
            .IsValid(x?.TrimEnd()))
            .WithMessage("Card CVC must be 3 digits");
        RuleFor(request => request.Balance)
          .Must(x => x > 0).WithMessage("Balance must be greater than zero");

        When(request => request.Account != null, () => {
            RuleFor(request => request.Account.FirstName)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("First name is required");
            RuleFor(request => request.Account.LastName)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Last name is required");
            RuleFor(request => request.Account.Email)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Email is required");
            RuleFor(request => request.Account.Password)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Password is required");
        });
    }
}
