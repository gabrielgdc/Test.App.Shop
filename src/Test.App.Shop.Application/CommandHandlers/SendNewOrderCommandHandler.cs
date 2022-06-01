using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.App.Shop.Application.Commands;
using Test.App.Shop.Application.IntegrationEvents;
using Test.App.Shop.Domain.Aggregates.ApplicationAggregate;
using Test.App.Shop.Domain.Aggregates.OrdersAggregate;
using Test.App.Shop.Domain.Aggregates.UserAggregate;
using Test.App.Shop.Domain.Exceptions;
using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Application.CommandHandlers;

public class SendNewOrderCommandHandler : CommandHandler, IRequestHandler<SendNewOrderCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<SendNewOrderCommandHandler> _logger;

    public SendNewOrderCommandHandler(
        IUnitOfWork uow,
        IMediator bus,
        INotificationHandler<ExceptionNotification> notifications,
        IUserRepository userRepository,
        IOrderRepository orderRepository,
        IApplicationRepository applicationRepository,
        IPublishEndpoint publishEndpoint,
        ILogger<SendNewOrderCommandHandler> logger
    ) : base(uow, bus, notifications)
    {
        _userRepository = userRepository;
        _orderRepository = orderRepository;
        _applicationRepository = applicationRepository;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Unit> Handle(SendNewOrderCommand request, CancellationToken cancellationToken)
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

            foreach (var applicationId in request.CartProductsIds)
            {
                applications.Add(await _applicationRepository.GetApplicationById(applicationId));
            }

            var order = new Order(request.UserId);

            order.AddItems(applications);

            _orderRepository.Add(order);

            if (await CommitAsync() is false) return Unit.Value;

            var submittedOrderEvent = new OrderSubmittedIntegrationEvent(order.Id, request.UserId, request.PaymentId);
            await _publishEndpoint.Publish(submittedOrderEvent, cancellationToken);

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
