using MediatR;

namespace Test.App.Shop.Application.Commands;

public class LoginUserCommand : Command, IRequest
{
    public string Email { get; }

    public LoginUserCommand(string email)
    {
        Email = email;
    }

    public override bool IsValid()
    {
        throw new System.NotImplementedException();
    }
}
