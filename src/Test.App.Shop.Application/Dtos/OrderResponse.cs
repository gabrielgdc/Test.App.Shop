using System;
using Test.App.Shop.Domain.Aggregates.OrdersAggregate;

namespace Test.App.Shop.Application.Dtos;

public class OrderResponse
{
    public Guid OrderId { get; }
    public Guid PaymentMethodId { get; }
    public decimal TotalPrice { get; }
    public DateTime CreatedAt { get; }
    public int StatusId { get; }

    public OrderResponse(Guid id, DateTime createdAt, Guid paymentMethodId, decimal totalPrice, int statusId)
    {
        OrderId = id;
        PaymentMethodId = paymentMethodId;
        TotalPrice = totalPrice;
        CreatedAt = createdAt;
        StatusId = statusId;
    }
}
