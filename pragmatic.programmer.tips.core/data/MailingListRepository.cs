using Newtonsoft.Json;
using pragmatic.programmer.tips.core.data.interfaces;
using pragmatic.programmer.tips.core.models;

namespace pragmatic.programmer.tips.core.data;

/// <summary>
///     Methods for obtaining Pragmatic Programmer Tip Mailing List Emails.
/// </summary>
public class MailingListRepository : IMailingListRepository
{
    public MailingListRepository()
    {
        var rootPath = Path.GetDirectoryName(Environment.GetCommandLineArgs().FirstOrDefault()) ??
                       Constants.ThisDirectory;
        var mailingListDataPath = Path.Join(rootPath, Constants.FileLocationOfRawMailingListJsonFile);
        if (!File.Exists(mailingListDataPath))
            File.Create(mailingListDataPath);
    }

    /// <summary>
    ///     gets all emails in the mailing list.
    /// </summary>
    /// <returns>a list of emails included in the mailing list</returns>
    public async Task<IEnumerable<EmailAddress>> GetAllEmailsInMailingListAsync()
    {
        var rootPath = Path.GetDirectoryName(Environment.GetCommandLineArgs().FirstOrDefault()) ??
                       Constants.ThisDirectory;
        var mailingListDataPath = Path.Join(rootPath, Constants.FileLocationOfRawMailingListJsonFile);
        var mailingListJson = await File.ReadAllTextAsync(mailingListDataPath);
        var mailingList = JsonConvert.DeserializeObject<IEnumerable<EmailAddress>>(mailingListJson);
        return mailingList ?? new List<EmailAddress>();
    }

    /// <summary>
    ///     adds an email to the mailing list.
    /// </summary>
    /// <param name="email">the email to add to the mailing list</param>
    /// <returns>a message describing the result</returns>
    public async Task<string> AddEmailToMailingListAsync(EmailAddress email)
    {
        var existingMailingList = (await GetAllEmailsInMailingListAsync()).ToList();
        if (existingMailingList.Any(e => e.Email.Equals(email.Email)))
            return Constants.EmailAlreadyIncludedInMailingList;

        existingMailingList.Add(email);
        await WriteMailingListAsync(existingMailingList);
        return Constants.EmailAddedToMailingList;
    }

    /// <summary>
    ///     remove an email from the mailing list
    /// </summary>
    /// <param name="email">the email to remove from the mailing list</param>
    /// <returns>a message describing the result</returns>
    public async Task<string> RemoveEmailFromMailingListAsync(EmailAddress email)
    {
        var existingMailingList = (await GetAllEmailsInMailingListAsync()).ToList();
        existingMailingList.RemoveAll(e => e.Email.Equals(email.Email));
        await WriteMailingListAsync(existingMailingList);
        return Constants.EmailRemovedFromMailingList;
    }

    /// <summary>
    ///     (re-)write the given mailing list at the data location.
    /// </summary>
    /// <param name="mailingList">the mailing list to write to disk</param>
    /// <returns>nothing, but the mailing list is written to disk</returns>
    private static async Task WriteMailingListAsync(IEnumerable<EmailAddress> mailingList)
    {
        var rootPath = Path.GetDirectoryName(Environment.GetCommandLineArgs().FirstOrDefault()) ??
                       Constants.ThisDirectory;
        var mailingListDataPath = Path.Join(rootPath, Constants.FileLocationOfRawMailingListJsonFile);
        var mailingListJson = JsonConvert.SerializeObject(mailingList);
        await File.WriteAllTextAsync(mailingListDataPath, mailingListJson);
    }
}