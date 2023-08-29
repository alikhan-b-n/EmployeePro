using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmployeePro.Bll.Dtos;
using EmployeePro.Bll.Extentsions;
using EmployeePro.Dal.Entities;
using EmployeePro.Dal.Providers.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EmployeePro.Bll.Services;

public class AuthentificationService : IAuthentificationService
{
    private readonly IOptions<SecretOptions> _secretOptions;
    private readonly ICrudProvider<EmployeeEntity> _employeeProvider;

    public AuthentificationService(IOptions<SecretOptions> secretOptions,
        ICrudProvider<EmployeeEntity> employeeProvider)
    {
        _secretOptions = secretOptions;
        _employeeProvider = employeeProvider;
    }


    public string GenerateHashPassword(string email, string fullname)
    {
        return BCrypt.Net.BCrypt.HashPassword(new StringBuilder($"{email}_{fullname}123").ToString());
    }


    public async Task<string> SignIn(EmployeeSignInDto employeeSignIn)
    {
        var employeeEntities = await _employeeProvider.GetAll();
        var employeeEntity = employeeEntities.First(x => x.Email == employeeSignIn.Email);
        if (employeeEntity == null) throw new ArgumentException("error, login is not found");

        if (BCrypt.Net.BCrypt.Verify(employeeSignIn.Password, employeeEntity.PasswordHash))
        {
            return GenerateToken(employeeEntity?.Email, employeeEntity?.Fullname, employeeEntity.Id);
        }

        throw new ArgumentException("error, password is not correct");
    }

    private string GenerateToken(string email, string fullname, Guid id)
    {
        var key = Encoding.ASCII.GetBytes(_secretOptions.Value.JwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, fullname),
                new Claim(ClaimTypes.NameIdentifier, email),
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

    private (string email, string fullname, string id) DecryptToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        var tokenS = handler.ReadToken(token) as JwtSecurityToken;

        if (tokenS?.Claims is List<Claim> claims)
        {
            return new ValueTuple<string, string, string>(claims[0].Value, claims[1].Value, claims[2].Value);
        }

        throw new ArgumentException();
    }
    
    public async Task<EmployeeEntity?> GetUserByHeader(string[] headers)
    {
        var token = headers[0].Replace("Bearer ", "");
        var id = DecryptToken(token).id.StringToGuid();
        return await _employeeProvider.GetById(id);
    }
}