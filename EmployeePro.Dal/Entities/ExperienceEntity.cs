using EmployeePro.Dal.Entities.Abstract;

namespace EmployeePro.Dal.Entities;

public class ExperienceEntity : BaseEntity
{
    public string? JobTitle { get; set; }
    public string? CompanyName { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public EmployeeEntity EmployeeEntity { get; set; }
    public Guid EmployeeId { get; set; }
}