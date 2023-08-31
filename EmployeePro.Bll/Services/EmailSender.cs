using System.Net;
using System.Net.Mail;
using EmployeePro.Bll.Services.Interfaces;

namespace EmployeePro.Bll;

public class EmailSender : IEmailSender
{
    private readonly string congratulations =
        "Happy birthday 🥳. We very grateful for your contribution and effort to this company," +
        " our company wishes you health, future accomplishments and our sincere love." +
        "We are lucky to have you with us.";

    public async Task SendHappyBirthday(string receiverEmail)
    {
        if (!IsValidEmail(receiverEmail))
        {
            return;
        }
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("alinur.ast@gmail.com", "odrblxqmdpxctjwg"),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage("alinur.ast@gmail.com", receiverEmail)
        {
            Subject = "Birthday Greetings",
            Body = $"{congratulations}"
        };

        await smtpClient.SendMailAsync(mailMessage);
    }

    public async Task SendMessage(string receiverEmail, string? message, string? subject)
    {
        if (!IsValidEmail(receiverEmail))
        {
            return;
        }

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("alinur.ast@gmail.com", "odrblxqmdpxctjwg"),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage("alinur.ast@gmail.com", receiverEmail)
        {
            Subject = $"{subject}",
            Body = $"{message}"
        };

        await smtpClient.SendMailAsync(mailMessage);
    }


    private bool IsValidEmail(string email)
    {
        try
        {
            MailAddress m = new MailAddress(email);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}