using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.App.Shop.Application.Adapters.Identity;
using Test.App.Shop.Application.Commands;
using Test.App.Shop.Application.Dtos;
using Test.App.Shop.Domain.Aggregates.UserAggregate;
using Test.App.Shop.Domain.Exceptions;
using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Application.CommandHandlers;

public class LoginUserCommandHandler : CommandHandler, IRequestHandler<LoginUserCommand, UserLoggedInDto>
{
    private readonly ITokenManager _tokenManager;
    private readonly IUserRepository _userRepository;
    private ILogger<LoginUserCommandHandler> _logger;


    public LoginUserCommandHandler(
        IUnitOfWork uow,
        IMediator bus,
        INotificationHandler<ExceptionNotification> notifications,
        IUserRepository userRepository,
        ITokenManager tokenManager,
        ILogger<LoginUserCommandHandler> logger
    ) : base(uow, bus, notifications)
    {
        _userRepository = userRepository;
        _logger = logger;
        _tokenManager = tokenManager;
    }

    public async Task<UserLoggedInDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserByCpf(request.Cpf);

            if (user == null || !user.CheckPassword(request.Password))
            {
                await Bus.Publish(new ExceptionNotification("1", "Usuário ou senha não inválidos"), cancellationToken);
                return default;
            }

            return new UserLoggedInDto(_tokenManager.GenerateToken(user));
        }
        catch (Exception e)
        {
            _logger.LogCritical("Ocorreu um erro ao efetuar o login do usuário #### Exception: {0} ####", e.ToString());
            await Bus.Publish(new ExceptionNotification("2", "Serviço indisponível"), cancellationToken);
            return default;
        }
    }
}
