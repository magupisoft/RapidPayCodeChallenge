using System.ComponentModel.DataAnnotations;
using FluentValidation;
using RapidPay.Domain.Requests;

namespace RapidPay.Domain.Validators;

public class PaymentValidator : AbstractValidator<DoPaymentRequest>
{
    public PaymentValidator()
    {
        RuleFor(request => request.Number)
           .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Card number es required");

        RuleFor(x => x.Number)
            .Must(x => new RegularExpressionAttribute("(\\d{15})$")
            .IsValid(x?.TrimEnd())).WithMessage("Card number must be 15 digits");
        
        RuleFor(request => request.Amount)
          .Must(x => x > 0).WithMessage("Amount must be greater than zero");
    }
}
