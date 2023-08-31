namespace EmployeePro.Bll.Services.Interfaces;

public interface IEmployeeCreator
{
    public Task CreateEmployee(string url, string email);
}