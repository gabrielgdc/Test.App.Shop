using System;
using System.Collections.Generic;
using Test.App.Shop.Application.Dtos;

namespace Test.App.Shop.Application.Queries;

public class GetOrdersByUserIdQuery : Query<IEnumerable<OrderResponse>>
{
    public Guid UserId { get; }

    public GetOrdersByUserIdQuery(Guid userId)
    {
        UserId = userId;
    }
}
