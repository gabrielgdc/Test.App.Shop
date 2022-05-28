using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Test.App.Shop.Api.Dtos;
using Test.App.Shop.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.App.Shop.Application.Commands;
using Test.App.Shop.Infra.CrossCutting.IoC.Configurations.Authentication;

namespace Test.App.Shop.Api.Controllers.V1;

[ApiVersion("1")]
[ApiController]
[Authorize(AuthenticationSchemes = CustomAuthenticationSchemes.Bearer)]
public class PaymentMethodController : BaseController
{
    private readonly IMediator _bus;

    public PaymentMethodController(INotificationHandler<ExceptionNotification> notifications, IMediator bus) : base(notifications)
    {
        _bus = bus;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddPaymentMethod([FromBody] AddUserCreditCardDto addUserCreditCardDto)
    {
        var userIdentity = (ClaimsIdentity)User.Identity;
        var sub = userIdentity?.Claims.FirstOrDefault(cl => cl.Type.Equals(UserAuthenticationClaims.UserId))?.Value;
        var userId = Guid.TryParse(sub, out var parsedUserId) ? parsedUserId : Guid.Empty;

        var addUserCreditCardCommand = new AddUserPaymentMethodCommand(
            userId,
            addUserCreditCardDto.Alias,
            addUserCreditCardDto.CardNumber,
            addUserCreditCardDto.ExpireDate,
            addUserCreditCardDto.CardHolderName,
            addUserCreditCardDto.SecurityNumber
        );

        await _bus.Send(addUserCreditCardCommand);
        return Response(Created(Request.Path.ToUriComponent(), null));
    }

    [HttpDelete("{paymentMethodId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeletePaymentMethod(Guid paymentMethodId)
    {
        var userIdentity = (ClaimsIdentity)User.Identity;
        var sub = userIdentity?.Claims.FirstOrDefault(cl => cl.Type.Equals(UserAuthenticationClaims.UserId))?.Value;
        var userId = Guid.TryParse(sub, out var parsedUserId) ? parsedUserId : Guid.Empty;

        var deleteUserCreditCardCommand = new DeleteUserPaymentMethodCommand(paymentMethodId, userId);

        await _bus.Send(deleteUserCreditCardCommand);

        return Response(NoContent());
    }
}
