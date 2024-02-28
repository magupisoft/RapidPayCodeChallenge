using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace RapidPay.Controllers
{
    public class BaseController : ControllerBase
    {
        protected static List<string> GetValidationErrors(ValidationResult validationResult)
        {
            var message = new List<string>();
            foreach (var failure in validationResult.Errors)
            {
                message.Add(failure.ErrorMessage);
            }
            return message;
        }
    }
}
