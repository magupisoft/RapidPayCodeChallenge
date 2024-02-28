using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace RapidPay.Domain.Validators;

public class CardValidator : AbstractValidator<string>
{
    public CardValidator()
    {
        RuleFor(request => request)
           .Must(x => !string.IsNullOrWhiteSpace(x))
           .WithMessage("Card number es required");
        RuleFor(x => x)
            .Must(x => new RegularExpressionAttribute("(\\d{15})$")
            .IsValid(x?.TrimEnd()))
            .WithMessage("Card number must be 15 digits");
    }
}
