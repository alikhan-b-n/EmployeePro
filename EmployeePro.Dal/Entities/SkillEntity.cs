using EmployeePro.Dal.Entities.Abstract;
using EmployeePro.Dal.Entities.BridgeTables;

namespace EmployeePro.Dal.Entities;

public class SkillEntity : BaseEntity
{
    public string Skill { get; set; }
    public List<EmployeeSkillEntity> EmployeesSkills { get; set; }
}