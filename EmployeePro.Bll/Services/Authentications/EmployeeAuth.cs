using System.Text;
using EmployeePro.Bll.Dtos;
using EmployeePro.Bll.Extentsions;
using EmployeePro.Bll.Services.Interfaces;
using EmployeePro.Dal.Entities;
using EmployeePro.Dal.Providers.Interfaces;

namespace EmployeePro.Bll.Services.Authentications;

public class EmployeeAuth : IEmployeeAuth
{
    private readonly ITokenService _tokenService;
    private readonly ICrudProvider<EmployeeEntity> _employeeProvider;

    public EmployeeAuth(ITokenService tokenService, ICrudProvider<EmployeeEntity> employeeProvider)
    {
        _tokenService = tokenService;
        _employeeProvider = employeeProvider;
    }
    
    public string GenerateHashPassword(string email, string fullname)
    {
        return BCrypt.Net.BCrypt.HashPassword(new StringBuilder($"{email}{fullname}").ToString());
    }
    
    public async Task<string> SignIn(EmployeeSignInDto employeeSignIn)
    {
        var employeeEntities = await _employeeProvider.GetAll();
        var employeeEntity = employeeEntities.First(x => x.Email == employeeSignIn.Email);
        if (employeeEntity == null) throw new ArgumentException("error, login is not found");

        if (BCrypt.Net.BCrypt.Verify(employeeSignIn.Password, employeeEntity.PasswordHash))
        {
            return _tokenService.GenerateToken(employeeEntity.Email, "Employee", employeeEntity.Id);
        }

        throw new ArgumentException($"error, password is not correct");
    }

    public async Task<EmployeeEntity?> GetEmployeeByHeader(string[] headers)
    {
        var token = headers[0].Replace("Bearer ", "");
        var id =_tokenService.DecryptToken(token).id.StringToGuid();
        return await _employeeProvider.GetById(id);
    }
    
}