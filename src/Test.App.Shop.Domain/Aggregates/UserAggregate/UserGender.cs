using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Domain.Aggregates.UserAggregate;

public class UserGender : Enumeration
{
    public static readonly UserGender Male = new(1, nameof(Male));
    public static readonly UserGender Female = new(2, nameof(Female));
    public static readonly UserGender Other = new(3, nameof(Other));

    public UserGender(int id, string name) : base(id, name)
    {
    }
}
