namespace EmployeePro.Bll.Dtos;

public class HrSignInDto
{
    public string Login { get; set; }
    public string Password { get; set; }
}

public class HrSignUpDto
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string MasterKey { get; set; }
}