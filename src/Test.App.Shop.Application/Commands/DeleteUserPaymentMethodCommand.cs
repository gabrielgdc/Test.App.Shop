using System;
using MediatR;
using Test.App.Shop.Application.Validations;

namespace Test.App.Shop.Application.Commands;

public class DeleteUserPaymentMethodCommand : Command, IRequest
{
    public Guid PaymentMethodId { get; }
    public Guid UserId { get; }

    public DeleteUserPaymentMethodCommand(Guid paymentMethodId, Guid userId)
    {
        PaymentMethodId = paymentMethodId;
        UserId = userId;
    }

    public override bool IsValid()
    {
        ValidationResult = new DeleteUserPaymentMethodCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
