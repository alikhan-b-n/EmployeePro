using EmployeePro.Bll.Dtos;

namespace EmployeePro.Bll.Services;

public interface IEmployeeManager
{
    public Task CreateEmployee(string url, string email);
    public Task DeleteByIdEmployeeProfile(Guid id);
    public Task UpdateEmployeeProfile(EmployeeDto employeeDto);
    public Task<List<EmployeeDto>> GetAllEmployeeProfiles();
    public Task<EmployeeDto> GetByIdEmployeeProfile(Guid id);
}