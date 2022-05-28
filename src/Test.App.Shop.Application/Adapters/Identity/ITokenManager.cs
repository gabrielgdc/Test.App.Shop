using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Test.App.Shop.Domain.Aggregates.UserAggregate;

namespace Test.App.Shop.Application.Adapters.Identity;

public interface ITokenManager
{
    public string GenerateToken(User user);
    public IEnumerable<Claim> GetTokenClaims(string token);
    public bool ValidateToken(string token);
}
