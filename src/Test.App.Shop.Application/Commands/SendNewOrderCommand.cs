using System;
using MediatR;
using Test.App.Shop.Application.Validations;

namespace Test.App.Shop.Application.Commands;

public class SendNewOrderCommand : Command, IRequest<Unit>
{
    public Guid[] CartProductsIds { get; }
    public Guid UserId { get; }
    public Guid PaymentId { get; }

    public SendNewOrderCommand(Guid[] cartProductsIds, Guid userId, Guid paymentId)
    {
        CartProductsIds = cartProductsIds;
        UserId = userId;
        PaymentId = paymentId;
    }

    public override bool IsValid()
    {
        ValidationResult = new SendNewOrderCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
