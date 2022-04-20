namespace pragmatic.programmer.tips.cli.constants
{
    /// <summary>
    /// Constants file containing most log messages.
    /// </summary>
    internal static class LogMessageConstants
    {
        public const string EnteredMainMethod = "entered cli.main()";
        public const string InitializedLogger = "initialized logger(Logger.LogTo.Console, Logger.LogLevel.Debug)";
        public const string InitializedConfiguration = "initialized configuration using configurationBuilder.Build()";
        public const string InitializedPragmaticProgrammerTipsRepository = "initialized PragmaticProgrammerTipsRepository()";
        public const string InitializedTipService = "initialized TipService(tipRepository, logger)";
        public const string GotRandomTipFromTipService = "got theRandomTip from tipService.GetRandomTipAsync()";
        public const string InitializedEmailTemplateRepository = "initialized EmailTemplateRepository()";
        public const string GotEmailTemplateFromEmailTemplateRepository = "got email template from emailTemplateRepository.ReadFromRawEmailTemplateFileAsync()";
        public const string InitializedEmailService = "initialized EmailService(emailConfiguration)";
        public const string BuiltMimeMessageUsingEmailService = "built mime message using EmailService.BuildMimeMessageUsingHtml(...)";
        public const string SentEmailUsingEmailService = "sent email using emailService.SendAsync(email)";
        public const string SendingEmailException = "Sending email exception";
        public const string FinishedSendingEmails = "finished sending emails to TO list";
        public const string CliException = "cli exception";
        public const string ExitedMailMethod = "exited cli.main()\n";
    }
}