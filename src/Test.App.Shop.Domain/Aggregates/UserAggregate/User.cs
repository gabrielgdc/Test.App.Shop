using System;
using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Domain.Aggregates.UserAggregate;

public class User : Entity, IAggregateRoot
{
    public string FullName { get; }
    public string Cpf { get; }
    public DateTime BirthDate { get; }
    public UserGender Gender { get; }
    public UserAddress Address { get; }
    public byte[] Salt { get; }
    public string Password { get; }
    private int GenderId { get; }

    protected User()
    {
    }

    public User(string fullName, string cpf, DateTime birthDate, int genderId, UserAddress address, string password)
    {
        FullName = fullName;
        Cpf = cpf;
        BirthDate = birthDate;
        Gender = Enumeration.FromId<UserGender>(genderId);
        GenderId = Gender.Id;
        Address = address;

        (Password, Salt) = PasswordHasher.Hash(password);
    }

    public bool CheckPassword(string password)
    {
        return PasswordHasher.Check(Password, Salt, password);
    }
}
