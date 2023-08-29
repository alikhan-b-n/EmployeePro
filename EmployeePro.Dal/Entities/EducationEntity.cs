using EmployeePro.Dal.Entities.Abstract;

namespace EmployeePro.Dal.Entities;

public class EducationEntity : BaseEntity
{
    public string? SchoolName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }
    public EmployeeEntity EmployeeEntity { get; set; }
    public Guid EmployeeId { get; set; }
}