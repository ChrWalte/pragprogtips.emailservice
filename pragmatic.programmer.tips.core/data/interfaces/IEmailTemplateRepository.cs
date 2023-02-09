namespace pragmatic.programmer.tips.core.data.interfaces;

/// <summary>
///     interface for the pragmatic programmer tips email template repository.
/// </summary>
public interface IEmailTemplateRepository
{
    Task<string> ReadFromRawEmailTemplateFileAsync();
}