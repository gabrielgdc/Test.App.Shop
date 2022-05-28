using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.App.Shop.Application.Commands;
using Test.App.Shop.Domain.Aggregates.ApplicationAggregate;
using Test.App.Shop.Domain.Aggregates.OrdersAggregate;
using Test.App.Shop.Domain.Aggregates.UserAggregate;
using Test.App.Shop.Domain.Exceptions;
using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Application.CommandHandlers;

public class NewOrderCommandHandler : CommandHandler, IRequestHandler<NewOrderCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly ILogger<NewOrderCommandHandler> _logger;

    public NewOrderCommandHandler(
        IUnitOfWork uow,
        IMediator bus,
        INotificationHandler<ExceptionNotification> notifications,
        IUserRepository userRepository,
        IOrderRepository orderRepository,
        IApplicationRepository applicationRepository,
        ILogger<NewOrderCommandHandler> logger
    ) : base(uow, bus, notifications)
    {
        _userRepository = userRepository;
        _orderRepository = orderRepository;
        _applicationRepository = applicationRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(NewOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserById(request.UserId);

            if (user is null)
            {
                await Bus.Publish(new ExceptionNotification("7", "Usuário não encontrado"), cancellationToken);
                return Unit.Value;
            }

            if (!user.PaymentMethods.Any(x => x.Id.Equals(request.PaymentId)))
            {
                await Bus.Publish(new ExceptionNotification("8", "Método de pagamento inválido"), cancellationToken);
                return Unit.Value;
            }

            var applications = new List<Domain.Aggregates.ApplicationAggregate.Application>();

            foreach (var applicationId in request.ApplicationsIds)
            {
                applications.Add(await _applicationRepository.GetApplicationById(applicationId));
            }

            var order = new Order(request.UserId, request.PaymentId);

            order.AddItems(applications);

            // TODO: Publicar pra fila

            return Unit.Value;
        }
        catch (Exception e)
        {
            _logger.LogCritical("Ocorreu um erro ao criar uma ordem #### Exception: {0} ####", e.ToString());
            await Bus.Publish(new ExceptionNotification("2", "Serviço indisponível"), cancellationToken);
            return Unit.Value;
        }
    }
}
