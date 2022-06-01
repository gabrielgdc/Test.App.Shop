using System;
using System.Threading.Tasks;
using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Domain.Aggregates.OrdersAggregate;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order> GetOrderById(Guid orderId);
}
