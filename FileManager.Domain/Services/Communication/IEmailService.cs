namespace FileManager.Domain.Services.Communication;
public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}