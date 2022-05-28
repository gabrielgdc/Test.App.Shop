namespace Test.App.Shop.Application.Dtos;

public class UserLoggedInDto
{
    public string Token { get; }

    public UserLoggedInDto(string token)
    {
        Token = token;
    }
}
