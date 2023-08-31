using System.Security.Authentication;
using EmployeePro.Bll.Dtos;
using EmployeePro.Bll.Services;
using EmployeePro.Bll.Services.Interfaces;
using EmployeePro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace EmployeePro.Controllers.Employee;

[Authorize(Policy = "Employee")]
public class ProfileCabinet : ControllerBase
{
    private readonly IEmployeeManager _employeeManager;
    private readonly IEmployeeAuth _employeeAuth;

    public ProfileCabinet(IEmployeeManager employeeManager, IEmployeeAuth employeeAuth)
    {
        _employeeManager = employeeManager;
        _employeeAuth = employeeAuth;
    }

    [HttpPatch("api/profile")]
    public async Task<IActionResult> UpdateYourProfile([FromBody] EmployeeViewModel employeeViewModel)
    {
        var user = await _employeeAuth
            .GetEmployeeByHeader(Request.Headers[HeaderNames.Authorization]!);

        var userExit = user ?? throw new AuthenticationException();

        await _employeeManager.UpdateEmployeeProfile(new EmployeeDto
        {
            Fullname = employeeViewModel.Fullname,
            Summary = employeeViewModel.Summary,
            ProfilePicUrl = employeeViewModel.ProfilePicUrl,
            DepartmentId = employeeViewModel.DepartmentId,
            Skills = employeeViewModel.Skills,
            Languages = employeeViewModel.Languages,
            Id = userExit.Id,
            Birthday = employeeViewModel.Birthday
        });
        return NoContent();
    }

    [HttpGet("api/profile/")]
    public async Task<IActionResult> GetYourProfile()
    {
        var user = await _employeeAuth
            .GetEmployeeByHeader(Request.Headers[HeaderNames.Authorization]!);

        var userExit = user ?? throw new AuthenticationException();
        
        var employeeDto = await _employeeManager.GetByIdEmployeeProfile(userExit.Id);
        return Ok(new EmployeeViewModel
        {
            Fullname = employeeDto.Fullname,
            Summary = employeeDto.Summary,
            ProfilePicUrl = employeeDto.ProfilePicUrl,
            DepartmentId = employeeDto.DepartmentId,
            Skills = employeeDto.Skills,
            Languages = employeeDto.Languages,
        });
    }

    [AllowAnonymous]
    [HttpPost("api/profile/signin")]
    public async Task<IActionResult> Signin([FromBody] EmployeeSignInViewModel employeeSignInViewModel)
    {
        return Ok(await _employeeAuth.SignIn(new EmployeeSignInDto
        {
            Email = employeeSignInViewModel.Email,
            Password = employeeSignInViewModel.Password
        }));
    }
}