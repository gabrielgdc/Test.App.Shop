using System.Collections.Generic;
using MediatR;
using Test.App.Shop.Application.Dtos;

namespace Test.App.Shop.Application.Queries;

public class GetApplicationsQuery : Query<IEnumerable<ApplicationDto>>, IRequest<Unit>
{
}
