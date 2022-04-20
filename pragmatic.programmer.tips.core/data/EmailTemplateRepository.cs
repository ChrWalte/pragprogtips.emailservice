using pragmatic.programmer.tips.core.data.interfaces;

namespace pragmatic.programmer.tips.core.data
{
    /// <summary>
    /// Methods for obtaining email templates.
    /// </summary>
    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        public EmailTemplateRepository()
        { }

        /// <summary>
        /// reads a html email template file from disk
        /// </summary>
        /// <returns>raw html code for the email template</returns>
        public async Task<string> ReadFromRawEmailTemplateFileAsync()
        {
            var rootPath = Path.GetDirectoryName(Environment.GetCommandLineArgs().FirstOrDefault()) ?? Constants.ThisDirectory;
            var emailTemplatePath = Path.Join(rootPath, Constants.FileLocationOfRawEmailTemplateHtmlFile);
            var rawHtml = await File.ReadAllTextAsync(emailTemplatePath);
            return rawHtml;
        }
    }
}