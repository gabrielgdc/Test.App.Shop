using System;
using System.Collections.Generic;
using System.Linq;
using Test.App.Shop.Domain.Aggregates.ApplicationAggregate;
using Test.App.Shop.Domain.Exceptions;
using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Domain.Aggregates.OrdersAggregate;

public class Order : Entity, IAggregateRoot
{
    public Guid UserId { get; }
    public Guid? PaymentMethodId { get; private set; }
    public decimal TotalPrice { get; private set; }
    public DateTime CreatedAt { get; }
    public IEnumerable<Application> OrderItems => _orderItems.AsReadOnly();
    private readonly List<Application> _orderItems;
    public OrderStatus Status { get; }
    private int _statusId { get; set; }

    protected Order()
    {
        _orderItems = new List<Application>();
    }

    public Order(Guid userId)
    {
        UserId = userId;

        _statusId = OrderStatus.Pending.Id;
        _orderItems = new List<Application>();

        CreatedAt = DateTime.UtcNow;
    }

    public void AddItems(IEnumerable<Application> application)
    {
        _orderItems.AddRange(application);
        TotalPrice = _orderItems.Sum(x => x.Price);
    }

    public void Accept(Guid paymentMethodId)
    {
        PaymentMethodId = paymentMethodId;
        _statusId = OrderStatus.Accept.Id;
    }

    public void Reject(Guid paymentMethodId)
    {
        PaymentMethodId = paymentMethodId;
        _statusId = OrderStatus.Rejected.Id;
    }
}
