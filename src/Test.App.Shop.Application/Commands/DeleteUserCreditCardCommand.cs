using System;
using MediatR;

namespace Test.App.Shop.Application.Commands;

public class DeleteUserCreditCardCommand : Command, IRequest
{
    public Guid CreditCardId { get; }

    public DeleteUserCreditCardCommand(Guid creditCardId)
    {
        CreditCardId = creditCardId;
    }

    public override bool IsValid()
    {
        throw new System.NotImplementedException();
    }
}
