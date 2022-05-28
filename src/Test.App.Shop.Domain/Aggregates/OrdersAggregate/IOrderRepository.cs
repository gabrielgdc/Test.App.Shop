using System;
using System.Threading.Tasks;

namespace Test.App.Shop.Domain.Aggregates.OrdersAggregate;

public interface IOrderRepository
{
    Task<Order> GetOrderById(Guid orderId);
}
