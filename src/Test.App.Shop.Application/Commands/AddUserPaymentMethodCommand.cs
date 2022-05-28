using System;
using System.Globalization;
using MediatR;
using Test.App.Shop.Application.Validations;

namespace Test.App.Shop.Application.Commands;

public class AddUserPaymentMethodCommand : Command, IRequest
{
    public Guid UserId { get; }
    public string Alias { get; }
    public long CardNumber { get; }
    public DateTime ExpireDate { get; }
    public string CardHolderName { get; }
    public int SecurityNumber { get; }

    public AddUserPaymentMethodCommand(Guid userId, string alias, long cardNumber, string expireDate, string cardHolderName, int securityNumber)
    {
        UserId = userId;
        Alias = alias;
        CardNumber = cardNumber;
        ExpireDate = DateTime.ParseExact(expireDate, "MM/yy", new CultureInfo(CultureInfo.CurrentCulture.LCID));
        CardHolderName = cardHolderName;
        SecurityNumber = securityNumber;
    }

    public override bool IsValid()
    {
        ValidationResult = new AddUserPaymentMethodCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
