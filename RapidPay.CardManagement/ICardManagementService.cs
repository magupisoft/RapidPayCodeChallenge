﻿using RapidPay.Domain.Requests;
using RapidPay.Domain.Responses;

namespace RapidPay.CardManagement;

public interface ICardManagementService
{
    public Task<CreateCardResponse> CreateNewCardAsync(CreateCardRequest request);

    Task<CardPaymentResponse> ProcessPayment(DoPaymentRequest request);

    public Task<CardBalanceResponse?> GetCardBalance(string cardNumber);
}
