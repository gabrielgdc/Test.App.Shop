using System;

namespace Test.App.Shop.Application.IntegrationEvents;

public class OrderSubmittedIntegrationEvent
{
    public Guid OrderId { get; }
    public Guid UserId { get; }
    public Guid PaymentMethodId { get; }

    public OrderSubmittedIntegrationEvent(Guid orderId, Guid userId, Guid paymentMethodId)
    {
        OrderId = orderId;
        PaymentMethodId = paymentMethodId;
        UserId = userId;
    }
}
