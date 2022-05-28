using System;
using MediatR;
using Test.App.Shop.Application.Validations;

namespace Test.App.Shop.Application.Commands;

public class NewOrderCommand : Command, IRequest<Unit>
{
    public Guid[] ApplicationsIds { get; }
    public Guid UserId { get; }
    public Guid PaymentId { get; }

    public NewOrderCommand(Guid[] applicationsIds, Guid userId, Guid paymentId)
    {
        ApplicationsIds = applicationsIds;
        UserId = userId;
        PaymentId = paymentId;
    }

    public override bool IsValid()
    {
        ValidationResult = new NewOrderCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
