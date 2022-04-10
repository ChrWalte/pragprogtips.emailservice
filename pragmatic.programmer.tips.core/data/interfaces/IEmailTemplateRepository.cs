namespace pragmatic.programmer.tips.core.data.interfaces
{
    public interface IEmailTemplateRepository
    {
        Task<string> ReadFromRawEmailTemplateFileAsync();
    }
}
