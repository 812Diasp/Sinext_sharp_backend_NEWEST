namespace Sinext_sharp_backend.Services;
using System.Net;
using System.Net.Mail;


public class EmailService
{
    private readonly SmtpClient _client;

    public EmailService(IConfiguration configuration)
    {
        var smtpSettings = configuration.GetSection("SmtpSettings");

        _client = new SmtpClient(smtpSettings["Server"], int.Parse(smtpSettings["Port"]))
        {
            Credentials = new NetworkCredential(
                smtpSettings["Username"],
                smtpSettings["Password"]),
            EnableSsl = true
        };
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var mailMessage = new MailMessage(
            from: new MailAddress("den_tkachenk0@mail.ru", "SI.WALLET"),
            to: new MailAddress(to)
        )
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        await _client.SendMailAsync(mailMessage);
    }
}