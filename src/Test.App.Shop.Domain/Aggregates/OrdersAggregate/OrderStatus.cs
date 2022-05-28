using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Domain.Aggregates.OrdersAggregate;

public class OrderStatus : Enumeration
{
    public static readonly OrderStatus Pending = new(1, nameof(Pending));
    public static readonly OrderStatus Accept = new(2, nameof(Accept));
    public static readonly OrderStatus Rejected = new(3, nameof(Rejected));
    public static readonly OrderStatus Cancelled = new(4, nameof(Cancelled));

    public OrderStatus(int id, string name) : base(id, name)
    {
    }
}
