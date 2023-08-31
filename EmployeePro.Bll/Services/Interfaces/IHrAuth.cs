using EmployeePro.Bll.Dtos;
using EmployeePro.Dal.Entities;

namespace EmployeePro.Bll.Services.Interfaces;

public interface IHrAuth
{
    public Task<string> SignIn(HrSignInDto hrSignInDto);
    public Task<string> SignUp(HrSignUpDto hrSignUpDto);
}