using MediatR;
using System.Threading.Tasks;
using Test.App.Shop.Domain.SeedWork;
using Test.App.Shop.Domain.Exceptions;
using Test.App.Shop.Application.Commands;

namespace Test.App.Shop.Application.CommandHandlers;

public abstract class CommandHandler
{
	private readonly IUnitOfWork _uow;
	protected readonly IMediator Bus;
	private readonly ExceptionNotificationHandler _notifications;

	protected CommandHandler(IUnitOfWork uow, IMediator bus, INotificationHandler<ExceptionNotification> notifications)
	{
		_uow = uow;
		Bus = bus;
		_notifications = (ExceptionNotificationHandler)notifications;
	}

	protected void NotifyValidationErrors(Command message)
	{
		foreach (var error in message.GetValidationResult().Errors)
		{
			Bus.Publish(new ExceptionNotification("001", error.ErrorMessage, error.PropertyName));
		}
	}

	public async Task<bool> CommitAsync()
	{
		if (_notifications.HasNotifications()) return false;
		if (await _uow.CommitAsync()) return true;

		await Bus.Publish(new ExceptionNotification("002", "We had a problem during saving your data."));

		return false;
	}
}
