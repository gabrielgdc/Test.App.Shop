using System;
using System.Collections.Generic;
using System.Linq;
using Test.App.Shop.Domain.Aggregates.PaymentMethodAggregate;
using Test.App.Shop.Domain.Exceptions;
using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Domain.Aggregates.UserAggregate;

public class User : Entity, IAggregateRoot
{
    public string FullName { get; }
    public string Cpf { get; }
    public DateTime BirthDate { get; }
    public UserAddress Address { get; }
    public byte[] Salt { get; }
    public string Password { get; }

    public UserGender Gender { get; private set; }
    private int _genderId;

    public virtual IEnumerable<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();
    private List<PaymentMethod> _paymentMethods;

    protected User()
    {
        _paymentMethods = new List<PaymentMethod>();
    }

    public User(string fullName, string cpf, DateTime birthDate, int genderId, UserAddress address, string password)
    {
        FullName = fullName;
        Cpf = cpf;
        BirthDate = birthDate;
        // Gender = Enumeration.FromId<UserGender>(genderId);
        _genderId = genderId;
        Address = address;
        _paymentMethods = new List<PaymentMethod>();

        (Password, Salt) = PasswordHasher.Hash(password);
    }

    public bool CheckPassword(string password)
    {
        return PasswordHasher.Check(Password, Salt, password);
    }

    public PaymentMethod VerifyOrAddPaymentMethod(int cardTypeId, string alias, long cardNumber, int securityNumber, string cardHolderName, DateTime expirationDate)
    {
        if (expirationDate < DateTime.UtcNow) throw new DomainException("Data de expiração inválida");

        var existingPayment = _paymentMethods?.SingleOrDefault(p => p.IsEqualTo(cardTypeId, cardNumber, expirationDate));

        if (existingPayment is not null) return existingPayment;

        var payment = new PaymentMethod(cardTypeId, alias, cardNumber, securityNumber, cardHolderName, expirationDate);

        _paymentMethods?.Add(payment);

        return payment;
    }

    public void DeletePaymentMethod(Guid paymentMethodId)
    {
        _paymentMethods = _paymentMethods?.Where(p => p.Id.Equals(paymentMethodId)).ToList();
    }
}
