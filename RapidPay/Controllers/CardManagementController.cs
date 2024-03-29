﻿using System.Net.Mime;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.CardManagement;
using RapidPay.Domain.Requests;
using RapidPay.Domain.Responses;

namespace RapidPay.Controllers;

[Authorize]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/[controller]")]
[ApiController]
public class CardManagementController(
    ICardManagementService cardManagementService,
    IValidator<CreateCardRequest> createCardValidator,
    IValidator<DoPaymentRequest> paymentValidator,
    IValidator<string> cardValidator,
    ILogger<CardManagementController> logger) : BaseController
{
    /// <summary>
    /// POST: Create new Card
    /// </summary>
    /// <param name="request">CreateCardRequest</param>
    /// <returns>Created Card Number</returns>
    [HttpPost("card/create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CreateCardResponse>> CreatesCardAsync(CreateCardRequest request)
    {
        var validationResult = await createCardValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(GetValidationErrors(validationResult));
        }

        try
        {
            var response = await cardManagementService.CreateNewCardAsync(request);
            return CreatedAtAction("GetsCardBalance", new { cardNumber = response.Number }, response);
        }
        catch (Exception ex)
        {
            var logError = $"Error when creating new card with number: {request.Number}. Error message: {ex.Message}";
            logger.LogError(logError, ex);
            return this.StatusCode(StatusCodes.Status500InternalServerError, logError);
        }
    }

    /// <summary>
    /// PUT: Execute a payment using a given card number and the payment amount
    /// </summary>
    /// <param name="request">Card Number and Amount to be paid</param>
    /// <returns>Transaction details</returns>
    [HttpPut("card/payment")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CardPaymentResponse>> PaymentAsync(
        [FromBody] DoPaymentRequest request
    )
    {
        var validationResult = await paymentValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(GetValidationErrors(validationResult));
        }

        CardPaymentResponse response;
        try
        {
            response = await cardManagementService.ProcessPayment(request);
        }
        catch (Exception ex)
        {
            var logError =
                $"Error doing a card payment with number: {request.Number} " +
                $"and amount: {request.Amount}. " +
                $"Error message: {ex.Message}";
            logger.LogError(logError, ex);
            return this.StatusCode(StatusCodes.Status500InternalServerError, logError);
        }

        return Accepted(response);
    }

    /// <summary>
    /// GET: Gets Card Balance
    /// </summary>
    /// <param name="cardNumber">15 digits card number</param>
    /// <returns>CardBalanceResponse</returns>
    [HttpGet("card/{cardNumber}/balance")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CardBalanceResponse>> GetsCardBalance([FromRoute] string cardNumber)
    {
        var validationResult = await cardValidator.ValidateAsync(cardNumber);
        if (!validationResult.IsValid)
        {
            return BadRequest(GetValidationErrors(validationResult));
        }

        try
        {
            var card = await cardManagementService.GetCardBalance(cardNumber);
            if (card == null)
            {
                return NotFound(new { message = $"Card with number {cardNumber} does not exist" });
            }

            return Ok(card);
        }
        catch (Exception ex)
        {
            var logError = $"Error retrieving card with number: {cardNumber}. Error message: {ex.Message}";
            logger.LogError(logError, ex);
            return this.StatusCode(StatusCodes.Status500InternalServerError, logError);
        }
    }

}
