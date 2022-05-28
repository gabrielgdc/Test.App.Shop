using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.App.Shop.Api.Dtos;
using Test.App.Shop.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.App.Shop.Infra.CrossCutting.IoC.Configurations.Authentication;

namespace Test.App.Shop.Api.Controllers.V1;

[ApiVersion("1")]
[ApiController]
[Authorize(AuthenticationSchemes = CustomAuthenticationSchemes.Bearer)]
public class PaymentController : BaseController
{
    public PaymentController(INotificationHandler<ExceptionNotification> notifications) : base(notifications)
    {
    }

    [HttpPost("credit-card")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IActionResult> AddCreditCard()
    {
        var ipsum = new List<string> { "Nothing", "Here", "Just", "Hello" };
        return Task.FromResult(Response(Ok(new Response<object>(ipsum))));
    }

    [HttpDelete("credit-card")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IActionResult> DeleteCreditCard(Guid creditCardId)
    {
        var ipsum = new List<string> { "Nothing", "Here", "Just", "Hello" };
        return Task.FromResult(Response(Ok(new Response<object>(ipsum))));
    }
}
