using System;
using MediatR;

namespace Test.App.Shop.Domain.Events;

public class Event : INotification
{
	public DateTime Timestamp { get; }

	protected Event()
	{
		Timestamp = DateTime.UtcNow;
	}
}