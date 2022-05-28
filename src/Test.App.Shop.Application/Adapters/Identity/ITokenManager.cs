using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test.App.Shop.Application.Adapters.Identity;

public interface IJwtTokenManager
{
    public Task<string> GenerateToken();
    public Task<IEnumerable<string>> GetTokenClaims(string token);
    public Task<bool> ValidateToken(string token);
}
