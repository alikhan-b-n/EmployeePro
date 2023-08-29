using EmployeePro.Dal.Entities.Abstract;

namespace EmployeePro.Dal.Entities.BridgeTables;

public class EmployeeSkillEntity : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public EmployeeEntity EmployeeEntity { get; set; }
    public Guid SkillId { get; set; }
    public SkillEntity SkillEntity { get; set; }
}