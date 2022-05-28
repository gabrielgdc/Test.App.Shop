using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.App.Shop.Domain.SeedWork;
using Test.App.Shop.Domain.Exceptions;
using Test.App.Shop.Application.Commands;
using Test.App.Shop.Domain.Aggregates.UserAggregate;

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
            var existingUser = await _userRepository.GetUserByCpf(request.Cpf);

            if (existingUser is not null)
            {
                await Bus.Publish(new ExceptionNotification("3", "Usuário já cadastrado"), cancellationToken);
                return default;
            }

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

            return await CommitAsync() is false ? default : Unit.Value;
        }
        catch (Exception e)
        {
            _logger.LogCritical("Ocorreu um erro ao cadastro o usuário #### Exception: {0} ####", e.ToString());
            await Bus.Publish(new ExceptionNotification("2", "Serviço indisponível"), cancellationToken);
            return Unit.Value;
        }
    }
}
