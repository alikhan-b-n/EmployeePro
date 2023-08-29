using EmployeePro.Bll.Dtos;
using EmployeePro.Dal.Entities;

namespace EmployeePro.Bll.Services;

public interface IAuthentificationService
{
    public Task<EmployeeEntity?> GetUserByHeader(string[] headers);
    public string GenerateHashPassword(string email, string fullname);
    public Task<string> SignIn(EmployeeSignInDto employeeSignIn);
}