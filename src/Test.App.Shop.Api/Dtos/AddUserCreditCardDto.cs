namespace Test.App.Shop.Api.Dtos;

public class AddUserCreditCardDto
{
    public string Alias { get; }
    public long CardNumber { get; }
    public string ExpireDate { get; }
    public string CardHolderName { get; }
    public int SecurityNumber { get; }

    public AddUserCreditCardDto(string alias, long cardNumber, string expireDate, string cardHolderName, int securityNumber)
    {
        Alias = alias;
        CardNumber = cardNumber;
        ExpireDate = expireDate;
        CardHolderName = cardHolderName;
        SecurityNumber = securityNumber;
    }
}
