using System;

namespace Test.App.Shop.Application.Dtos;

public class NewOrderRequest
{
    public Guid[] CartProductsIds { get; }
    public Guid PaymentId { get; }

    public NewOrderRequest(Guid[] cartProductsIds, Guid paymentId)
    {
        CartProductsIds = cartProductsIds;
        PaymentId = paymentId;
    }
}
