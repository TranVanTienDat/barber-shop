using System.Net;
using System.Net.Mail;
using BarberBookingWeb.Models;
using BarberBookingWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBookingWeb.Services;

public class SmtpEmailService : IEmailService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<SmtpEmailService> _logger;

    public SmtpEmailService(IServiceProvider serviceProvider, ILogger<SmtpEmailService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var settings = await context.AppSettings.ToListAsync();

            var server = settings.FirstOrDefault(s => s.Key == "Email_SmtpServer")?.Value ?? "smtp.gmail.com";
            var port = int.TryParse(settings.FirstOrDefault(s => s.Key == "Email_Port")?.Value, out var p) ? p : 587;
            var username = settings.FirstOrDefault(s => s.Key == "Email_Username")?.Value ?? "";
            var password = settings.FirstOrDefault(s => s.Key == "Email_Password")?.Value ?? "";
            var senderEmail = settings.FirstOrDefault(s => s.Key == "Email_SenderEmail")?.Value ?? "";
            var senderName = settings.FirstOrDefault(s => s.Key == "Email_SenderName")?.Value ?? "Barber Shop";
            var enableSsl = settings.FirstOrDefault(s => s.Key == "Email_EnableSsl")?.Value == "true";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                _logger.LogWarning("⚠️ Cấu hình SMTP chưa hoàn thiện (Thiếu Username hoặc Password). Email KHÔNG được gửi.");
                return;
            }

            // Chuyển đổi xuống dòng thành <br/> để hiển thị đúng trong HTML email
            var htmlBody = body.Replace("\r\n", "<br/>").Replace("\n", "<br/>");

            using var client = new SmtpClient(server, port);
            client.EnableSsl = enableSsl;
            client.Credentials = new NetworkCredential(username, password);

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail, senderName),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };
            mailMessage.To.Add(toEmail);

            await client.SendMailAsync(mailMessage);
            _logger.LogInformation($"✅ Email đã được gửi thành công tới: {toEmail}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"❌ Lỗi khi gửi email tới {toEmail}: {ex.Message}");
        }
    }
}
