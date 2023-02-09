namespace pragmatic.programmer.tips.core.models;

/// <summary>
///     Object used to store email information. Used in configuration of email and sending of email.
/// </summary>
public class EmailAddress
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}