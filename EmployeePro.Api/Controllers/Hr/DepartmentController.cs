using EmployeePro.Bll.Dtos;
using EmployeePro.Bll.Services.Interfaces;
using EmployeePro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePro.Controllers.Hr;

[Authorize(Policy = "HR")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentManager _departmentManager;

    public DepartmentController(IDepartmentManager departmentManager)
    {
        _departmentManager = departmentManager;
    }

    [HttpDelete("api/hr/departments/{id:guid}")]
    public async Task<IActionResult> DeleteDepartment([FromRoute] Guid id)
    {
        await _departmentManager.DeleteDepartment(id);
        return NoContent();
    }

    [HttpPost("api/hr/departments")]
    public async Task<IActionResult> CreateDepartment([FromBody] DepartmentViewModel departmentViewModel)
    {
        await _departmentManager.CreateDepartment(new DepartmentDto
        {
            Title = departmentViewModel.Title
        });
        return NoContent();
    }

    [HttpPatch("api/hr/departments/{id:guid}")]
    public async Task<IActionResult> ChangeDepartment([FromRoute] Guid id,
        [FromBody] DepartmentViewModel departmentViewModel)
    {
        await _departmentManager.ChangeDepartment(new DepartmentDto
        {
            Title = departmentViewModel.Title,
            Id = id
        });
        return NoContent();
    }

    [HttpPost("api/hr/departments/{departmentId:guid}/employees/{employeeId:guid}")]
    public async Task<IActionResult> AddEmployeeToDepartment([FromRoute] Guid employeeId, [FromRoute] Guid departmentId)
    {
        await _departmentManager.AddEmployeeToDepartment(employeeId, departmentId);
        return NoContent();
    }

    [HttpDelete("api/hr/departments/{departmentId:guid}/employees/{employeeId:guid}")]
    public async Task<IActionResult> DeleteEmployeeFromDepartment([FromRoute] Guid employeeId, [FromRoute] Guid departmentId)
    {
        await _departmentManager.DeleteEmployeeFromDepartment(employeeId, departmentId);
        return NoContent();
    }
    
    [HttpGet("api/hr/departments")]
    public async Task<IActionResult> GetAll()
    {
        var departments = await _departmentManager.GetAllDepartments();
        return Ok(departments);
    }
}