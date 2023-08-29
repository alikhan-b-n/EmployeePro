namespace EmployeePro.Controllers.Hr.Params;

public class HrParametrs
{
    
}

public class CreateViewModel
{
    public string UrlOfLinkedinEmployee { get; set; }
    public string Email { get; set; }
}

public class DepartmentViewModel
{
    public string Title { get; set; }
    public Guid Id { get; set; }
}

public class EmployeeViewModel
{
    public string? Fullname { get; set; }
    public string? Summary { get; set; }
    public Guid? DepartmentId { get; set; }
    public string? ProfilePicUrl { get; set; }
    public List<string>? Skills { get; set; } 
    public List<string>? Languages { get; set; }
}

