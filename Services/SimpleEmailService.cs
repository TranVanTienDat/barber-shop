namespace BarberBookingWeb.Services;

public class SimpleEmailService : IEmailService
{
    private readonly ILogger<SimpleEmailService> _logger;

    public SimpleEmailService(ILogger<SimpleEmailService> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(string toEmail, string subject, string body)
    {
        _logger.LogInformation("--- SENDING EMAIL ---");
        _logger.LogInformation($"To: {toEmail}");
        _logger.LogInformation($"Subject: {subject}");
        _logger.LogInformation($"Body:\n{body}");
        _logger.LogInformation("---------------------");

        // Simulate network delay
        return Task.CompletedTask;
    }
}
