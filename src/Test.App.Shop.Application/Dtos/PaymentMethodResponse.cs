using System;

namespace Test.App.Shop.Application.Dtos;

public class PaymentMethodResponse
{
    public Guid PaymentMethodId { get; }
    public string Alias { get; }
    public decimal Limit { get; }

    public PaymentMethodResponse(string alias, Guid id, decimal limit)
    {
        PaymentMethodId = id;
        Alias = alias;
        Limit = limit;
    }
}
