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

public class GetApplicationsQueryHandler : QueryHandler, IRequestHandler<GetApplicationsQuery, IEnumerable<ApplicationDto>>
{
    private readonly IMediator _bus;
    private readonly ILogger<GetApplicationsQuery> _logger;

    public GetApplicationsQueryHandler(
        ApplicationConfiguration applicationConfiguration,
        IMediator bus,
        ILogger<GetApplicationsQuery> logger
    ) : base(applicationConfiguration)
    {
        _bus = bus;
        _logger = logger;
    }

    public async Task<IEnumerable<ApplicationDto>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            const string sql = "SELECT Id, Name, Price FROM Application";

            return await Database.QueryAsync<ApplicationDto>(sql);
        }
        catch (Exception e)
        {
            _logger.LogCritical("Ocorreu um erro ao buscar as aplicações #### Exception: {0} ####", e.ToString());
            await _bus.Publish(new ExceptionNotification("20", "Não foi possível buscar as aplicações"), cancellationToken);
            return default;
        }
    }
}
