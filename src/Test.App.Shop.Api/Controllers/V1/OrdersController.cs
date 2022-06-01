using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.App.Shop.Api.Dtos;
using Test.App.Shop.Application.Commands;
using Test.App.Shop.Application.Dtos;
using Test.App.Shop.Application.Queries;
using Test.App.Shop.Domain.Exceptions;
using Test.App.Shop.Infra.CrossCutting.IoC.Configurations.Authentication;

namespace Test.App.Shop.Api.Controllers.V1;

[ApiVersion("1")]
[ApiController]
[Authorize(AuthenticationSchemes = CustomAuthenticationSchemes.Bearer)]
public class OrdersController : BaseController
{
    private readonly IMediator _bus;

    public OrdersController(INotificationHandler<ExceptionNotification> notifications, IMediator bus) : base(notifications)
    {
        _bus = bus;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrders()
    {
        var userIdentity = (ClaimsIdentity)User.Identity;
        var sub = userIdentity?.Claims.FirstOrDefault(cl => cl.Type.Equals(UserAuthenticationClaims.UserId))?.Value;
        var userId = Guid.TryParse(sub, out var parsedUserId) ? parsedUserId : Guid.Empty;

        var query = new GetOrdersByUserIdQuery(userId);
        var orders = await _bus.Send(query);
        return Response(Ok(new ResponseDto<IEnumerable<OrderResponse>>(orders)));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> NewOrder([FromBody] NewOrderRequest newOrder)
    {
        var userIdentity = (ClaimsIdentity)User.Identity;
        var sub = userIdentity?.Claims.FirstOrDefault(cl => cl.Type.Equals(UserAuthenticationClaims.UserId))?.Value;
        var userId = Guid.TryParse(sub, out var parsedUserId) ? parsedUserId : Guid.Empty;

        var newOrderCommand = new SendNewOrderCommand(newOrder.CartProductsIds, userId, newOrder.PaymentId);
        await _bus.Send(newOrderCommand);
        return Response(Accepted());
    }
}
