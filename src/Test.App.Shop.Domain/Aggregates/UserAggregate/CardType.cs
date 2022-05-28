using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Domain.Aggregates.UserAggregate;

public class CardType : Enumeration
{
    public static readonly CardType Visa = new(1, nameof(Visa));
    public static readonly CardType MasterCard = new(2, nameof(MasterCard));

    public CardType(int id, string name) : base(id, name)
    {
    }
}
