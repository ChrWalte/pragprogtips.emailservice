namespace pragmatic.programmer.tips.core.models
{
    /// <summary>
    /// Object to hold EmailMessageConfiguration information. Used when sending emails.
    /// </summary>
    public class EmailMessageConfiguration
    {
        public string? SubjectTemplate { get; set; }
        public EmailAddress[]? ToEmails { get; set; }
        public EmailAddress[]? FromEmails { get; set; }
    }
}