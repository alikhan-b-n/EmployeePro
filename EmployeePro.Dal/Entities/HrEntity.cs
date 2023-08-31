using EmployeePro.Dal.Entities.Abstract;

namespace EmployeePro.Dal.Entities;

public class HrEntity : BaseEntity
{
    public string Login { get; set; }
    public string PasswordHash { get; set; }
}