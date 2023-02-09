using pragmatic.programmer.tips.core.models;

namespace pragmatic.programmer.tips.core.services.interfaces;

/// <summary>
///     interface for the mailing list service.
/// </summary>
public interface IMailingListService
{
    Task<IEnumerable<EmailAddress>> GetEntireMailingListAsync();

    Task<string> AddEmailToMailingListAsync(EmailAddress email);

    Task<string> RemoveEmailFromMailingListAsync(string email);
}