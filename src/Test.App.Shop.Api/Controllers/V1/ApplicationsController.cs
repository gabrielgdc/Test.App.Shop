using System.Collections.Generic;
using System.Threading.Tasks;
using Test.App.Shop.Api.Dtos;
using Test.App.Shop.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.App.Shop.Application.Dtos;
using Test.App.Shop.Application.Queries;
using Test.App.Shop.Infra.CrossCutting.IoC.Configurations.Authentication;

namespace Test.App.Shop.Api.Controllers.V1;

[ApiVersion("1")]
[ApiController]
[Authorize(AuthenticationSchemes = CustomAuthenticationSchemes.Bearer)]
public class ApplicationsController : BaseController
{
    private readonly IMediator _bus;

    public ApplicationsController(INotificationHandler<ExceptionNotification> notifications, IMediator bus) : base(notifications)
    {
        _bus = bus;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ApplicationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetApplications()
    {
        var query = new GetApplicationsQuery();

        var applications = await _bus.Send(query);
        return Response(Ok(new ResponseDto<IEnumerable<ApplicationDto>>(applications)));
    }
}
