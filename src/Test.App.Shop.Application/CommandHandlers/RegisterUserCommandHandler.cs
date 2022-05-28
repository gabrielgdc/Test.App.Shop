using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.App.Shop.Application.Adapters.Identity;
using Test.App.Shop.Application.Commands;
using Test.App.Shop.Domain.Aggregates.UserAggregate;
using Test.App.Shop.Domain.Exceptions;
using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Application.CommandHandlers;

public class RegisterUserCommandHandler : CommandHandler, IRequestHandler<RegisterUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<RegisterUserCommandHandler> _logger;

    public RegisterUserCommandHandler(
        IUnitOfWork uow,
        IMediator bus,
        INotificationHandler<ExceptionNotification> notifications,
        IUserRepository userRepository,
        ILogger<RegisterUserCommandHandler> logger
    ) : base(uow, bus, notifications)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userAddress = new UserAddress(
                request.Address.Street,
                request.Address.Number,
                request.Address.District,
                request.Address.City,
                request.Address.State,
                request.Address.Country,
                request.Address.Complement,
                request.Address.ZipCode
            );

            var user = new User(request.FullName, request.Cpf, request.BirthDate, request.GenderId, userAddress, request.Password);

            _userRepository.Add(user);

            if (await CommitAsync() is false)
            {
                await Bus.Publish("Não foi possível registrar o usuário", cancellationToken);
                return default;
            }

            return Unit.Value;
        }
        catch (Exception e)
        {
            _logger.LogCritical("Ocorreu um erro ao cadastro o usuário #### Exception: {0} ####", e.ToString());
            return Unit.Value;
        }
    }
}
