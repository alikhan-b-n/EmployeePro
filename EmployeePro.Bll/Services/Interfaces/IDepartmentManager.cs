using EmployeePro.Bll.Dtos;

namespace EmployeePro.Bll.Services.Interfaces;

public interface IDepartmentManager
{
    public Task CreateDepartment(DepartmentDto departmentDto);
    public Task DeleteDepartment(Guid id);

    public Task<List<DepartmentDto>> GetAllDepartments();

    public Task ChangeDepartment(DepartmentDto departmentDto);

    public Task AddEmployeeToDepartment(Guid employeeId, Guid departmentId);

    public Task DeleteEmployeeFromDepartment(Guid departmentId, Guid employeeId);
}