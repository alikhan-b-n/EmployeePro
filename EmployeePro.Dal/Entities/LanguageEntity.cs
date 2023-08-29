using EmployeePro.Dal.Entities.Abstract;
using EmployeePro.Dal.Entities.BridgeTables;

namespace EmployeePro.Dal.Entities;

public class LanguageEntity : BaseEntity
{
    public string Language { get; set; }
    public List<EmployeeLanguageEntity> EmployeeLanguages { get; set; }

}