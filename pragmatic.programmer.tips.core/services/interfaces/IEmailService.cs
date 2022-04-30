using MimeKit;

namespace pragmatic.programmer.tips.core.services.interfaces
{
    /// <summary>
    /// interface for the email service.
    /// </summary>
    public interface IEmailService
    {
        Task SendAsync(MimeMessage message);
    }
}