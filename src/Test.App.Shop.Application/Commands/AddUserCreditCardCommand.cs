using System;
using MediatR;

namespace Test.App.Shop.Application.Commands;

public class AddUserCreditCardCommand : Command, IRequest
{
    public Guid UserId { get; }
    public int Number { get; }
    public int ExpireMonth { get; }
    public int ExpireYear { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Cvv { get; }

    public AddUserCreditCardCommand(Guid userId, int number, int expireMonth, int expireYear, string firstName, string lastName, string cvv)
    {
        UserId = userId;
        Number = number;
        ExpireMonth = expireMonth;
        ExpireYear = expireYear;
        FirstName = firstName;
        LastName = lastName;
        Cvv = cvv;
    }

    public override bool IsValid()
    {
        throw new System.NotImplementedException();
    }
}
