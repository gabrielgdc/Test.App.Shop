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

public class GetPaymentByUserIdQueryHandler : QueryHandler, IRequestHandler<GetPaymentMethodsByUserIdQuery, IEnumerable<PaymentMethodResponse>>
{
    private readonly IMediator _bus;
    private readonly ILogger<GetPaymentByUserIdQueryHandler> _logger;

    public GetPaymentByUserIdQueryHandler(
        ApplicationConfiguration applicationConfiguration,
        IMediator bus,
        ILogger<GetPaymentByUserIdQueryHandler> logger
    ) : base(applicationConfiguration)
    {
        _bus = bus;
        _logger = logger;
    }

    public async Task<IEnumerable<PaymentMethodResponse>> Handle(GetPaymentMethodsByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            const string sql = "SELECT Alias, Id, Limit FROM PaymentMethod WHERE UserId = @UserId";

            return await Database.QueryAsync<PaymentMethodResponse>(sql, new { request.UserId });
        }
        catch (Exception e)
        {
            await _bus.Publish(new ExceptionNotification("17", "Não possível buscar os métodos de pagamento"), cancellationToken);
            _logger.LogCritical("Ocorreu um erro ao buscar seus métodos de pagamento #### Exception: {0} ####", e.ToString());
            return default;
        }
    }
}
