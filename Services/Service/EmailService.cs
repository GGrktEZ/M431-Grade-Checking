using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Services.Service;

public class EmailService : IEmailService
{
    private readonly SmtpSettings _smtp;

    public EmailService(IConfiguration config)
    {
        _smtp = config.GetSection("Smtp").Get<SmtpSettings>()
            ?? throw new InvalidOperationException("Smtp settings are not configured.");
    }

    public async Task SendAsync(string toEmail, string subject, string body)
    {
        using var message = new MailMessage();
        message.From = new MailAddress(_smtp.FromAddress, _smtp.FromName);
        message.To.Add(toEmail);
        message.Subject = subject;
        message.Body = body;
        message.IsBodyHtml = false;

        using var client = new SmtpClient(_smtp.Host, _smtp.Port);
        client.EnableSsl = _smtp.EnableSsl;
        client.Credentials = new NetworkCredential(_smtp.Username, _smtp.Password);

        await client.SendMailAsync(message);
    }
}
