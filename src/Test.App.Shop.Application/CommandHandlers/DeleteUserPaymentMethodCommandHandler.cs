using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.App.Shop.Application.Commands;
using Test.App.Shop.Domain.Aggregates.UserAggregate;
using Test.App.Shop.Domain.Exceptions;
using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Application.CommandHandlers;

public class DeleteUserPaymentMethodCommandHandler : CommandHandler, IRequestHandler<DeleteUserPaymentMethodCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<DeleteUserPaymentMethodCommandHandler> _logger;

    public DeleteUserPaymentMethodCommandHandler(
        IUnitOfWork uow,
        IMediator bus,
        INotificationHandler<ExceptionNotification> notifications,
        IUserRepository userRepository,
        ILogger<DeleteUserPaymentMethodCommandHandler> logger
    ) : base(uow, bus, notifications)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteUserPaymentMethodCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserById(request.UserId);

            if (user is null || !user.PaymentMethods.Any(p => p.Id.Equals(request.PaymentMethodId)))
            {
                await Bus.Publish(new ExceptionNotification("6", "Usuário ou método de pagamento não encontrado"), cancellationToken);
                return Unit.Value;
            }

            user.DeletePaymentMethod(request.UserId);

            await CommitAsync();

            return Unit.Value;
        }
        catch (Exception e)
        {
            _logger.LogCritical("Ocorreu um erro ao deletar o método de pagamento #### Exception: {0} ####", e.ToString());
            await Bus.Publish(new ExceptionNotification("2", "Serviço indisponível"), cancellationToken);
            return Unit.Value;
        }
    }
}
