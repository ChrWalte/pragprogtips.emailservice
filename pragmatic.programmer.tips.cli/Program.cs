using Microsoft.Extensions.Configuration;
using pragmatic.programmer.tips.cli.constants;
using pragmatic.programmer.tips.core;
using pragmatic.programmer.tips.core.data;
using pragmatic.programmer.tips.core.data.interfaces;
using pragmatic.programmer.tips.core.models;
using pragmatic.programmer.tips.core.services;
using pragmatic.programmer.tips.core.services.interfaces;

// initialize logger
// var logger = new Logger(Logger.LogTo.Console, Logger.LogLevel.Debug);
var logger = new Logger(Logger.LogTo.File, "E:\\pragprogtips.emailservice.logs", Logger.LogLevel.Debug);
await logger.LogDebug(LogMessageConstants.EnteredMainMethod);
await logger.LogDebug(LogMessageConstants.InitializedLogger);

// halt on errors
try
{
    // get currentEnvironment from EnvironmentVariable
    var currentEnvironment = Environment.GetEnvironmentVariable(ConfigurationConstants.EnvironmentEnvironmentVariableName)
                             ?? ConfigurationConstants.NoEnvironmentFound;
    await logger.Log($"got {currentEnvironment} from Environment.GetEnvironmentVariable(...)");

    // initialize Configuration
    var configurationBuilder = new ConfigurationBuilder()
        .AddJsonFile("configuration.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"configuration.{currentEnvironment}.json", optional: true, reloadOnChange: true);
    IConfiguration configuration = configurationBuilder.Build();
    await logger.LogDebug(LogMessageConstants.InitializedConfiguration);

    // update logger.LogLevel if it can read it from configuration and if is something other than default Logger.LogLevel.Debug.
    var loggerLogLevelConfigurationAsString = configuration[ConfigurationConstants.LoggerConfigurationKey];
    if (Enum.TryParse<Logger.LogLevel>(loggerLogLevelConfigurationAsString, out var loggerLogLevelConfiguration))
    {
        if (loggerLogLevelConfiguration != Logger.LogLevel.Debug)
        {
            logger.OverrideLogLevel(loggerLogLevelConfiguration);
            await logger.LogWarning(
                $"updated logger.LogLevel [Debug]->[{loggerLogLevelConfigurationAsString}] using logger.OverrideLogLevel(loggerLogLevelConfiguration)");
        }
    }
    else
        await logger.LogWarning(
            $"failed to read Logger.LogLevel from configuration[{ConfigurationConstants.LoggerConfigurationKey}], set to default Logger.LogLevel.Debug");

    // initialize PragmaticProgrammerTipsRepository
    IPragmaticProgrammerTipsRepository tipRepository = new PragmaticProgrammerTipsRepository();
    await logger.LogDebug(LogMessageConstants.InitializedPragmaticProgrammerTipsRepository);

    // initialize Pragmatic Programmer TipService
    ITipService tipService = new TipService(tipRepository, logger);
    await logger.LogDebug(LogMessageConstants.InitializedTipService);

    // get random tip from Pragmatic Programmer TipService
    var theRandomTip = await tipService.GetRandomTipAsync();
    await logger.Log(LogMessageConstants.GotRandomTipFromTipService);

    // initialize EmailTemplateRepository
    IEmailTemplateRepository emailTemplateRepository = new EmailTemplateRepository();
    await logger.LogDebug(LogMessageConstants.InitializedEmailTemplateRepository);

    // get email template to use in emails
    var emailTemplateAsRawHtml = await emailTemplateRepository.ReadFromRawEmailTemplateFileAsync();
    await logger.Log(LogMessageConstants.GotEmailTemplateFromEmailTemplateRepository);

    // initialize EmailService
    var emailConfiguration = configuration.GetSection(ConfigurationConstants.EmailServerConfigurationKey)
        .Get<EmailServerConfiguration>();
    var emailService = new EmailService(emailConfiguration);
    await logger.LogDebug(LogMessageConstants.InitializedEmailService);

    // build email body as mime message
    var emailMessageConfiguration = configuration.GetSection(ConfigurationConstants.EmailMessageConfigurationKey)
        .Get<EmailMessageConfiguration>();
    if (emailMessageConfiguration == null)
        throw new ArgumentNullException(nameof(emailMessageConfiguration));

    var formattedEmailTemplate = string.Format(
        emailTemplateAsRawHtml,
        theRandomTip.Number,
        theRandomTip.Title,
        theRandomTip.Description);
    var formattedEmailSubject = string.Format(emailMessageConfiguration.SubjectTemplate, theRandomTip.Number);
    var email = EmailService.BuildMimeMessageUsingHtml(
        formattedEmailSubject,
        formattedEmailTemplate,
        emailMessageConfiguration.ToEmails,
        emailMessageConfiguration.FromEmails);
    await logger.LogDebug(LogMessageConstants.BuiltMimeMessageUsingEmailService);

    // send email
    await emailService.SendAsync(email);
    await logger.LogDebug(LogMessageConstants.SentEmailUsingEmailService);
}
catch (Exception ex)
{
    // log error
    await logger.LogError(LogMessageConstants.CliException, ex);
}

await logger.LogDebug(LogMessageConstants.ExitedMailMethod);