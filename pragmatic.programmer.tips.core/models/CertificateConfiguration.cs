namespace pragmatic.programmer.tips.core.models;

/// <summary>
///     Object to hold CertificateConfiguration information. Used for custom Certificate validation.
/// </summary>
public class CertificateConfiguration
{
    public string? Name { get; set; }
    public string? FingerPrint { get; set; }
    public string? Serial { get; set; }
    public string? Issuer { get; set; }
}