using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Test.App.Shop.Application.IntegrationEvents;
using Test.App.Shop.Domain.Aggregates.OrdersAggregate;
using Test.App.Shop.Domain.Aggregates.UserAggregate;
using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Application.Consumers;

public class SubmittedOrderConsumer : IConsumer<OrderSubmittedIntegrationEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _uow;
    private readonly ILogger<SubmittedOrderConsumer> _logger;

    public SubmittedOrderConsumer(IOrderRepository orderRepository, IUserRepository userRepository, IUnitOfWork uow, ILogger<SubmittedOrderConsumer> logger)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _uow = uow;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderSubmittedIntegrationEvent> context)
    {
        try
        {
            var orderSubmitted = context.Message;

            var user = await _userRepository.GetUserById(orderSubmitted.UserId);
            var order = await _orderRepository.GetOrderById(orderSubmitted.OrderId);
            var paymentMethod = user?.PaymentMethods?.SingleOrDefault(x => x.Id.Equals(orderSubmitted.PaymentMethodId));

            if (paymentMethod is null || !paymentMethod.Effect(order.TotalPrice))
            {
                order.Reject(orderSubmitted.PaymentMethodId);
                return;
            }

            order.Accept(orderSubmitted.PaymentMethodId);

            await _uow.CommitAsync();
        }
        catch (Exception e)
        {
            _logger.LogCritical("Ocorreu um erro ao processar a ordem #### Exception: {0} ####", e.ToString());
        }
    }
}
