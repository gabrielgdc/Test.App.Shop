using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Domain.Aggregates.UserAggregate;

public class UserAddress : ValueObject
{
    public string Street { get; }
    public string Number { get; }
    public string District { get; }
    public string City { get; }
    public string State { get; }
    public string Country { get; }
    public string Complement { get; }
    public string ZipCode { get; }

    public UserAddress(string street, string number, string district, string city, string state, string country, string complement, string zipCode)
    {
        Street = street;
        Number = number;
        District = district;
        City = city;
        State = state;
        Country = country;
        Complement = complement;
        ZipCode = zipCode;
    }
}
