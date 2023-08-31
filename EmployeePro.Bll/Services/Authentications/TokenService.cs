using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmployeePro.Bll.Dtos;
using EmployeePro.Bll.Extentsions;
using EmployeePro.Contract.Options;
using EmployeePro.Dal.Entities;
using EmployeePro.Dal.Providers.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EmployeePro.Bll.Services.Authentications;

public class TokenService : ITokenService
{
    private readonly IOptions<SecretOptions> _secretOptions;

    public TokenService(IOptions<SecretOptions> secretOptions)
    {
        _secretOptions = secretOptions;
    }


    public string GenerateToken(string nameIdentifier, string actor, Guid id)
    {
        var key = Encoding.ASCII.GetBytes(_secretOptions.Value.JwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Actor, $"{actor}"),
                new Claim(ClaimTypes.NameIdentifier, nameIdentifier),
                new Claim(ClaimTypes.PrimarySid, id.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }

    public (string nameIdentifier, string actor, string id) DecryptToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        var tokenS = handler.ReadToken(token) as JwtSecurityToken;

        if (tokenS?.Claims is List<Claim> claims)
        {
            return new ValueTuple<string, string, string>(claims[0].Value, claims[1].Value, claims[2].Value);
        }

        throw new ArgumentException();
    }
}