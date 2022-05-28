using System.Collections.Generic;
using System.Threading.Tasks;
using Test.App.Shop.Api.Dtos;
using Test.App.Shop.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.App.Shop.Application.Commands;
using Test.App.Shop.Application.Dtos;

namespace Test.App.Shop.Api.Controllers.V1;

[ApiVersion("1")]
[ApiController]
public class AuthenticationController : BaseController
{
    private readonly IMediator _bus;

    public AuthenticationController(INotificationHandler<ExceptionNotification> notifications, IMediator bus) : base(notifications)
    {
        _bus = bus;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(UserLoggedInDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
    {
        var userLoggedIn = await _bus.Send(loginUserCommand);

        return Response(Ok(userLoggedIn));
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
    {
        var registerUserCommand = new RegisterUserCommand(
            registerUserDto.FullName,
            registerUserDto.Cpf,
            registerUserDto.Password,
            registerUserDto.BirthDate,
            registerUserDto.GenderId,
            registerUserDto.Address
        );

        await _bus.Send(registerUserCommand);

        return Response(Ok());
    }
}
