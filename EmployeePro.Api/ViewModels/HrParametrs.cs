namespace EmployeePro.ViewModels;

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
    public DateTime? Birthday { get; set; }
}

public class EmailMessageViewModel
{
    public string Message { get; set; }
    public string Subject { get; set; }
}

public class HrSignInViewModel
{
    public string Login { get; set; }
    public string Password { get; set; }
}

public class HrSignUpViewModel
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string MasterKey { get; set; }
}

