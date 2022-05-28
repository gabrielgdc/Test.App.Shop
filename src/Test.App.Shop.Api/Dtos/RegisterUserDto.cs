using Test.App.Shop.Application.Commands;

namespace Test.App.Shop.Api.Dtos;

public class RegisterUserDto
{
    public string FullName { get; }
    public string Cpf { get; }
    public string Password { get; }
    public string BirthDate { get; }
    public int GenderId { get; }
    public UserAddressDto Address { get; }

    public RegisterUserDto(string fullName, string cpf, string password, string birthDate, int genderId, UserAddressDto address)
    {
        FullName = fullName;
        Cpf = cpf;
        Password = password;
        BirthDate = birthDate;
        GenderId = genderId;
        Address = address;
    }
}
