using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.App.Shop.Application.Dtos;
using Test.App.Shop.Application.Queries;
using Test.App.Shop.Domain.Exceptions;
using Test.App.Shop.Infra.CrossCutting.Environments.Configurations;

namespace Test.App.Shop.Application.QueryHandlers;

public class GetOrdersByUserQueryHandler : QueryHandler, IRequestHandler<GetOrdersByUserIdQuery, IEnumerable<OrderResponse>>
{
    private readonly IMediator _bus;
    private readonly ILogger<GetOrdersByUserQueryHandler> _logger;

    public GetOrdersByUserQueryHandler(ApplicationConfiguration applicationConfiguration, IMediator bus, ILogger<GetOrdersByUserQueryHandler> logger) : base(
        applicationConfiguration)
    {
        _bus = bus;
        _logger = logger;
    }

    public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            const string sql = "SELECT Id, CreatedAt, PaymentMethodId, TotalPrice, StatusId FROM [Order] WHERE UserId = @UserId";

            return await Database.QueryAsync<OrderResponse>(sql, new { request.UserId });
        }
        catch (Exception e)
        {
            _logger.LogCritical("Ocorreu um erro ao buscar as ordens #### Exception: {0} ####", e.ToString());
            await _bus.Publish(new ExceptionNotification("21", "Não foi possível buscar suas ordens"), cancellationToken);
            return default;
        }
    }
}
