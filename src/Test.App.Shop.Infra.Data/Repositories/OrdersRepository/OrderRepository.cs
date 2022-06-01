using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Test.App.Shop.Domain.Aggregates.OrdersAggregate;
using Test.App.Shop.Infra.Data.Context;

namespace Test.App.Shop.Infra.Data.Repositories.OrdersRepository;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public async Task<Order> GetOrderById(Guid orderId)
    {
        return await DbSet
            .Include("OrderItems")
            .SingleOrDefaultAsync(order => order.Id.Equals(orderId));
    }
}
