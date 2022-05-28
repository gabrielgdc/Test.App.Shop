using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.App.Shop.Application.Commands;
using Test.App.Shop.Domain.Aggregates.UserAggregate;
using Test.App.Shop.Domain.Exceptions;
using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Application.CommandHandlers;

public class AddUserPaymentMethodCommandHandler : CommandHandler, IRequestHandler<AddUserPaymentMethodCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AddUserPaymentMethodCommandHandler> _logger;

    public AddUserPaymentMethodCommandHandler(
        IUnitOfWork uow,
        IMediator bus,
        INotificationHandler<ExceptionNotification> notifications,
        IUserRepository userRepository,
        ILogger<AddUserPaymentMethodCommandHandler> logger
    ) : base(uow, bus, notifications)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(AddUserPaymentMethodCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserById(request.UserId);

            if (user is null)
            {
                await Bus.Publish(new ExceptionNotification("4", "Usuário não encontrado"), cancellationToken);
                return default;
            }

            var cardType = CardType.MasterCard;

            user.VerifyOrAddPaymentMethod(cardType.Id, request.Alias, request.CardNumber, request.SecurityNumber, request.CardHolderName, request.ExpireDate);

            return await CommitAsync() is not false ? default : Unit.Value;
        }

        catch (Exception e)
        {
            _logger.LogCritical("Ocorreu um erro ao salvar o cartão de crédito #### Exception: {0} ####", e.ToString());
            await Bus.Publish(new ExceptionNotification("2", "Serviço indisponível"), cancellationToken);
            return Unit.Value;
        }
    }
}
