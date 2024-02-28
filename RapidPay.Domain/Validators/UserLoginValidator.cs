using FluentValidation;
using RapidPay.Domain.Requests;

namespace RapidPay.Domain.Validators;

public class UserLoginValidator : AbstractValidator<UserLoginRequest>
{
    public UserLoginValidator()
    {
        RuleFor(request => request.Username)
           .Must(x => !string.IsNullOrWhiteSpace(x))
           .WithMessage("Username es required");
        RuleFor(request => request.Password)
          .Must(x => !string.IsNullOrWhiteSpace(x))
          .WithMessage("Password es required");
    }
}
