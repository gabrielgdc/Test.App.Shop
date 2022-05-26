using System;
using MediatR;

namespace Test.App.Shop.Application.IntegrationEvents;

public abstract class IntegrationEvent : INotification
{
    public DateTime TimeStamp { get; }

    protected IntegrationEvent()
    {
        TimeStamp = DateTime.UtcNow;
    }
}