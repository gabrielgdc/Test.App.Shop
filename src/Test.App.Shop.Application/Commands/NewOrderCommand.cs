using System;
using MediatR;

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

    public override bool IsValid() => throw new System.NotImplementedException();
}
