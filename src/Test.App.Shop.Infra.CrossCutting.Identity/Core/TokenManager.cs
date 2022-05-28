using Test.App.Shop.Application.Adapters.Identity;

namespace Test.App.Shop.Infra.CrossCutting.Identity.Core;

public class JwtTokenManager : IJwtTokenManager
{
    public Task<string> GenerateToken() => throw new NotImplementedException();

    public Task<IEnumerable<string>> GetTokenClaims(string token) => throw new NotImplementedException();

    public Task<bool> ValidateToken(string token) => throw new NotImplementedException();
}
