namespace EmployeePro.Bll.Services;

public interface IEmployeeCreatorAndUpdater
{
    public Task CreateEmployee(string url, string email);
}