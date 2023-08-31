namespace EmployeePro.Bll.Services.Interfaces;

public interface IEmailSender
{
    public Task SendHappyBirthday(string receiverEmail);
    public Task SendMessage(string receiverEmail, string message, string subject);

}