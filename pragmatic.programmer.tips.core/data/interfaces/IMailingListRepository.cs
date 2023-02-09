using pragmatic.programmer.tips.core.models;

namespace pragmatic.programmer.tips.core.data.interfaces;

/// <summary>
///     interface for the pragmatic programmer tips mailing list repository.
/// </summary>
public interface IMailingListRepository
{
    Task<IEnumerable<EmailAddress>> GetAllEmailsInMailingListAsync();

    Task<string> AddEmailToMailingListAsync(EmailAddress email);

    Task<string> RemoveEmailFromMailingListAsync(EmailAddress email);
}