using EmployeePro.Dal.Entities.Abstract;

namespace EmployeePro.Dal.Entities.BridgeTables;

public class EmployeeLanguageEntity : BaseEntity
{
    public EmployeeEntity EmployeeEntity { get; set; }
    public Guid EmployeeId { get; set; }
    public LanguageEntity LanguageEntity { get; set; }
    public Guid LanguageId { get; set; }
}