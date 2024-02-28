using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.CardManagement;
using RapidPay.Domain.Requests;
using RapidPay.Domain.Responses;

namespace RapidPay.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController(
    IUserSecurityService userSecurityService,
    IValidator<UserLoginRequest> userCardValidator,
    ILogger<CardManagementController> logger) : BaseController
    {
        /// <summary>
        /// POST: Create new Card
        /// </summary>
        /// <param name="request">CreateCardRequest</param>
        /// <returns>Created Card Number</returns>
        [HttpPost("access-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateCardResponse>> GetAccessTokenAsync(UserLoginRequest request)
        {
            var validationResult = await userCardValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(GetValidationErrors(validationResult));
            }

            try
            {
                var (accessToken, expiresAt) = await userSecurityService.GetAccessToken(request);
                return Ok(new { Token = accessToken, ExpiresAt = expiresAt });
            }
            catch (Exception ex)
            {
                var logError = $"Error when getting access token for username: {request.Username}. Error message: {ex.Message}";
                logger.LogError(logError, ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, logError);
            }
        }
    }
}
