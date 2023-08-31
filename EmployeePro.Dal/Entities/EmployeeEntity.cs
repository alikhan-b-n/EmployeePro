using EmployeePro.Dal.Entities.Abstract;
using EmployeePro.Dal.Entities.BridgeTables;

namespace EmployeePro.Dal.Entities;

public class EmployeeEntity : BaseEntity
{
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string? Summary { get; set; }
    public Guid? DepartmentId { get; set; }
    public DepartmentEntity? DepartmentEntity { get; set; }
    public string? ProfilePicUrl { get; set; }
    public List<EmployeeSkillEntity>? EmployeesSkills { get; set; }
    public List<EmployeeLanguageEntity>? EmployeeLanguages { get; set; }
    public DateTime? Birthday { get; set; }
}