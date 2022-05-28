using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Domain.Aggregates.ApplicationAggregate;

public class Application : Entity, IAggregateRoot
{
    public string Name { get; }
    public decimal Price { get; }

    public Application(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}
