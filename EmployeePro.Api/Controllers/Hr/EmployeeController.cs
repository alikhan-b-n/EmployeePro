using EmployeePro.Bll;
using EmployeePro.Bll.Dtos;
using EmployeePro.Bll.Services;
using EmployeePro.Bll.Services.Interfaces;
using EmployeePro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePro.Controllers.Hr;

[Authorize(Policy = "HR")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeManager _employeeManager;

    public EmployeeController(IEmployeeManager employeeManager)
    {
        _employeeManager = employeeManager;
    }

    [HttpPost("api/hr/employees")]
    public async Task<IActionResult> CreateEmployeeProfile([FromBody] CreateViewModel createViewModel)
    {
        if (string.IsNullOrWhiteSpace(createViewModel.UrlOfLinkedinEmployee) ||
            string.IsNullOrWhiteSpace(createViewModel.Email))
        {
            return BadRequest("Url and Email are required.");
        }

        await _employeeManager.CreateEmployee(createViewModel.UrlOfLinkedinEmployee, createViewModel.Email);
        return Ok();
    }

    [HttpGet("api/hr/employees/{id:guid}")]
    public async Task<IActionResult> GetByIdEmployeeProfile([FromRoute] Guid id)
    {
        var employee = await _employeeManager.GetByIdEmployeeProfile(id);
        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [HttpGet("api/hr/employees")]
    public async Task<IActionResult> GetAllProfiles()
    {
        var employees = await _employeeManager.GetAllEmployeeProfiles();
        return Ok(employees);
    }

    [HttpDelete("api/hr/employees/{id:guid}")]
    public async Task<IActionResult> DeleteEmployeeProfile([FromRoute] Guid id)
    {
        await _employeeManager.DeleteByIdEmployeeProfile(id);
        return NoContent();
    }

    [HttpPatch("api/hr/employees/{id:guid}")]
    public async Task<IActionResult> ChangeEmployeeProfile([FromBody] EmployeeViewModel employeeViewModel,
        [FromRoute] Guid id)
    {
        var employeeDto = new EmployeeDto
        {
            Fullname = employeeViewModel.Fullname,
            Summary = employeeViewModel.Summary,
            ProfilePicUrl = employeeViewModel.ProfilePicUrl,
            DepartmentId = employeeViewModel.DepartmentId,
            Skills = employeeViewModel.Skills,
            Languages = employeeViewModel.Languages,
            Id = id,
            Birthday = employeeViewModel.Birthday
        };

        await _employeeManager.UpdateEmployeeProfile(employeeDto);
        return NoContent();
    }

    [HttpPost("api/hr/employees/email_broadcast")]
    public async Task<IActionResult> SendEmailToEmployees(
        [FromBody] EmailMessageViewModel emailMessageViewModel)
    {
        await _employeeManager
            .SendEmailToAllEmployee(emailMessageViewModel.Message, emailMessageViewModel.Subject);
        return NoContent();
    }
}