namespace EmployeePro.Bll.Dtos;

public class EmployeeDto
{
    public string? Fullname { get; set; }
    public string Email { get; set; }
    public string? Summary { get; set; }
    public Guid? DepartmentId { get; set; }
    public string? ProfilePicUrl { get; set; }
    public List<string>? Skills { get; set; } 
    public List<string>? Languages { get; set; }
    public Guid Id { get; set; }
    public string? Department { get; set; }
    public DateTime? Birthday { get; set; }
    public List<ExperienceDto>? Experiences { get; set; }
    public List<EducationDto>? Educations { get; set; }
}