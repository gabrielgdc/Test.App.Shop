using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Test.App.Shop.Application.Adapters.Identity;
using Test.App.Shop.Domain.Aggregates.UserAggregate;
using Test.App.Shop.Infra.CrossCutting.Environments.Configurations;

namespace Test.App.Shop.Infra.CrossCutting.Identity.Core;

public class JwtTokenManager : ITokenManager
{
    private readonly JwtSettings _jwtSettings;
    private readonly ILogger<JwtTokenManager> _logger;

    public JwtTokenManager(JwtSettings jwtSettings, ILogger<JwtTokenManager> logger)
    {
        _jwtSettings = jwtSettings;
        _logger = logger;
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey ?? string.Empty);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString())
        };

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            signingCredentials: credentials
        );

        return tokenHandler.WriteToken(token);
    }

    public IEnumerable<Claim> GetTokenClaims(string token)
    {
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey ?? string.Empty);

        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateLifetime = false,
            ValidateAudience = false,
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

        return tokenValid.Claims;
    }

    public bool ValidateToken(string token)
    {
        try
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey ?? string.Empty);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogCritical("Ocorreu um erro ao validar o usuário #### Exception: {0}, StackTrace: {1} ####", e.Message, e.StackTrace);
            return false;
        }
    }
}
