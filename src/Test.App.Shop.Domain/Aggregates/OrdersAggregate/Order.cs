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
    public Guid PaymentMethodId { get; private set; }
    public decimal TotalPrice { get; private set; }
    public DateTime CreatedAt { get; }
    public OrderStatus OrderStatus { get; }
    public IEnumerable<Application> OrderItems => _orderItems.AsReadOnly();

    private readonly List<Application> _orderItems;
    private readonly int _orderStatusId;

    protected Order()
    {
        _orderItems = new List<Application>();
    }

    public Order(Guid userId, Guid paymentMethodId)
    {
        UserId = userId;
        PaymentMethodId = paymentMethodId;

        _orderStatusId = OrderStatus.Pending.Id;
        _orderItems = new List<Application>();

        CreatedAt = DateTime.UtcNow;
    }

    public void AddItems(IEnumerable<Application> application)
    {
        _orderItems.AddRange(application);
        TotalPrice = _orderItems.Sum(x => x.Price);
    }

    public void SetPaymentId(Guid paymentMethodId)
    {
        if (_orderStatusId != OrderStatus.Id) throw new DomainException("Ordem não está pendente");

        PaymentMethodId = paymentMethodId;
    }
}
