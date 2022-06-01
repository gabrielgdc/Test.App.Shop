using System;

namespace Test.App.Shop.Application.Dtos;

public class ApplicationDto
{
    public Guid Id { get; }
    public string Name { get; }
    public decimal Price { get; }

    public ApplicationDto(Guid id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}
