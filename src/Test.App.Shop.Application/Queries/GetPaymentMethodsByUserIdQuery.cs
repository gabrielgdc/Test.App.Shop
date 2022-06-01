using System;
using System.Collections.Generic;
using Test.App.Shop.Application.Dtos;

namespace Test.App.Shop.Application.Queries;

public class GetPaymentMethodsByUserIdQuery : Query<IEnumerable<PaymentMethodResponse>>
{
    public Guid UserId { get; }

    public GetPaymentMethodsByUserIdQuery(Guid userId)
    {
        UserId = userId;
    }
}
