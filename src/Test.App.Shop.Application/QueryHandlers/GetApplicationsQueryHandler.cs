using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Test.App.Shop.Application.Queries;
using Test.App.Shop.Infra.CrossCutting.Environments.Configurations;

namespace Test.App.Shop.Application.QueryHandlers;

public class GetApplicationsQueryHandler : QueryHandler, IRequestHandler<GetApplicationsQuery>
{
    public GetApplicationsQueryHandler(ApplicationConfiguration applicationConfiguration) : base(applicationConfiguration)
    {
    }

    public async Task<Unit> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}
