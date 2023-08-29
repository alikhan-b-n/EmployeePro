using EmployeePro.Bll.Dtos;
using EmployeePro.Bll.Services.Interfaces;
using EmployeePro.Dal.Entities;
using EmployeePro.Dal.Providers.Interfaces;

namespace EmployeePro.Bll.Services;

public class DepartmentManager : IDepartmentManager
{
    private readonly ICrudProvider<DepartmentEntity> _departmentProvider;
    private readonly ICrudProvider<EmployeeEntity> _employeeProvider;

    public DepartmentManager(ICrudProvider<DepartmentEntity> departmentProvider,
        ICrudProvider<EmployeeEntity> employeeProvider)
    {
        _departmentProvider = departmentProvider;
        _employeeProvider = employeeProvider;
    }

    public async Task CreateDepartment(DepartmentDto departmentDto)
    {
        await _departmentProvider.Create(new DepartmentEntity
        {
            Title = departmentDto.Title
        });
    }

    public async Task DeleteDepartment(Guid id)
    {
        var employeeEntities = await _employeeProvider.GetAll();
        foreach (var guid in employeeEntities.Where(x => x.DepartmentId == id)
                     .Select(x => x.DepartmentId = null)) ;
        await _departmentProvider.Delete(id);
    }

    public async Task<List<DepartmentDto>> GetAllDepartments()
    {
        var departmentEntities = await _departmentProvider.GetAll();
        return departmentEntities.Select(x => new DepartmentDto
        {
            Title = x.Title,
            Id = x.Id
        }).ToList();
    }

    public async Task ChangeDepartment(DepartmentDto? departmentDto)
    {
        var departmentEntity = await _departmentProvider.GetById(departmentDto?.Id);
        departmentEntity.Title = departmentDto?.Title ?? departmentEntity.Title;
        await _departmentProvider.Update(departmentEntity);
    }

    public async Task AddEmployeeToDepartment(Guid employeeId, Guid departmentId)
    {
        var employeeEntity = await _employeeProvider.GetById(employeeId);
        var departmentEntity = await _departmentProvider.GetById(departmentId);
        employeeEntity.DepartmentEntity = departmentEntity;
        employeeEntity.DepartmentId = departmentId;
        await _employeeProvider.Update(employeeEntity);
    }

    public async Task DeleteEmployeeFromDepartment(Guid departmentId, Guid employeeId)
    {
        var employeeEntities = await _employeeProvider.GetById(employeeId);
        employeeEntities.DepartmentId = null;
        employeeEntities.DepartmentEntity = null;
    }
}