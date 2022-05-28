using System;
using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Domain.Aggregates.UserAggregate;

public class PaymentMethod : Entity
{
    public Guid UserId { get; }
    public string Alias { get; }
    public long CardNumber { get; }
    public int SecurityNumber { get; }
    public string CardHolderName { get; }
    public DateTime ExpirationDate { get; }
    public CardType CardType { get; }
    private readonly int _cardTypeId;


    protected PaymentMethod()
    {
    }

    public PaymentMethod(int cardTypeId, string alias, long cardNumber, int securityNumber, string cardHolderName, DateTime expirationDate)
    {
        Alias = alias;
        CardNumber = cardNumber;
        SecurityNumber = securityNumber;
        CardHolderName = cardHolderName;
        ExpirationDate = expirationDate;
        _cardTypeId = cardTypeId;
    }

    public bool IsEqualTo(int cardTypeId, long cardNumber, DateTime expiration)
    {
        return _cardTypeId == cardTypeId && CardNumber == cardNumber && ExpirationDate == expiration;
    }

    public CardType GetCardType()
    {
        return Enumeration.FromId<CardType>(_cardTypeId);
    }
}
