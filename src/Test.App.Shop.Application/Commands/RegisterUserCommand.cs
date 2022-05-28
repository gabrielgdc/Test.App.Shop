using System;
using MediatR;
using Test.App.Shop.Application.Validations;

namespace Test.App.Shop.Application.Commands;

public class RegisterUserCommand : Command, IRequest<Unit>
{
    public string Name;
    public string Cpf;
    public DateTime BirthDate;
    public int GenderId;
    public UserAddressDto Address;

    public override bool IsValid()
    {
        ValidationResult = new RegisterUserCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class UserAddressDto
{
    public string Street { get; }
    public string District { get; }
    public string State { get; }
    public string Country { get; }
    public string City { get; }
    public string Complement { get; }
    public string ZipCode { get; }
    public string Number { get; }

    public UserAddressDto(string street, string district, string state, string country, string city, string complement, string zipCode, string number)
    {
        Street = street;
        District = district;
        State = state;
        Country = country;
        City = city;
        Complement = complement;
        ZipCode = zipCode;
        Number = number;
    }
}
