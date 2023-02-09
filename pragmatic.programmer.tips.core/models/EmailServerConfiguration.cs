namespace pragmatic.programmer.tips.core.models;

/// <summary>
///     Object used to hold the EmailServerConfiguration.
///     Allows for the UseCustomCertificateInformationInCertificateValidationCallback for uncommon email service providers.
/// </summary>
public class EmailServerConfiguration
{
    public string? Host { get; set; }
    public int Port { get; set; }
    public string? Username { get; set; }
    public string? PlaintextPassword { get; set; }

    // some email service providers are not included in MailKit by standard. the email service this was implemented for is ProtonMail.
    public bool UseCustomCertificateInformationInCertificateValidationCallback { get; set; } = false;
    public CertificateConfiguration? CustomCertificateInformation { get; set; } = null;
    public bool WriteCertificateInformationToFile { get; set; } = false;
}