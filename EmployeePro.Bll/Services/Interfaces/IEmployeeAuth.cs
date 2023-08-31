using EmployeePro.Bll.Dtos;
using EmployeePro.Dal.Entities;

namespace EmployeePro.Bll.Services.Interfaces;

public interface IEmployeeAuth
{
    public string GenerateHashPassword(string email, string fullname);
    public Task<string> SignIn(EmployeeSignInDto employeeSignIn);
    public Task<EmployeeEntity?> GetEmployeeByHeader(string[] headers);
}