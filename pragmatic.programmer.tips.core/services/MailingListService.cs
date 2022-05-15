using pragmatic.programmer.tips.core.data.interfaces;
using pragmatic.programmer.tips.core.models;
using pragmatic.programmer.tips.core.services.interfaces;

namespace pragmatic.programmer.tips.core.services
{
    /// <summary>
    /// service used to manage the mailing list for the pragmatic programmer tips.
    /// </summary>
    public class MailingListService : IMailingListService
    {
        private readonly IMailingListRepository _mailingListRepository;
        private readonly Logger _logger;

        public MailingListService(IMailingListRepository mailingListRepository, Logger logger)
        {
            _mailingListRepository = mailingListRepository;
            _logger = logger;
        }

        /// <summary>
        /// gets the entire mailing list.
        /// </summary>
        /// <returns>a list of emails on the mailing list</returns>
        public async Task<IEnumerable<EmailAddress>> GetEntireMailingListAsync()
        {
            await _logger.LogDebug(Constants.EnteredGetEntireMailingList);

            var mailingList = (await _mailingListRepository.GetAllEmailsInMailingListAsync()).ToList();
            await _logger.Log($"got {mailingList?.Count} emails from mailing list using _mailingListRepository.GetAllEmailsInMailingListAsync()");

            await _logger.LogDebug(Constants.ExitedGetEntireMailingList);
            return mailingList ?? new List<EmailAddress>();
        }

        /// <summary>
        /// adds the given email to the mailing list.
        /// </summary>
        /// <param name="email">the email to add to the mailing list</param>
        /// <returns>result message describing what happen</returns>
        public async Task<string> AddEmailToMailingListAsync(EmailAddress email)
        {
            await _logger.LogDebug(Constants.EnteredAddEmailToMailingList);

            var addResult = await _mailingListRepository.AddEmailToMailingListAsync(email);
            await _logger.Log($"got [{addResult}] from _mailingListRepository.AddEmailToMailingListAsync(...)");

            await _logger.LogDebug(Constants.ExitedAddEmailToMailingList);
            return addResult;
        }

        /// <summary>
        /// removes the given email to the mailing list.
        /// </summary>
        /// <param name="email">the email to remove from the mailing list</param>
        /// <returns>result message describing what happen</returns>
        public async Task<string> RemoveEmailFromMailingListAsync(string email)
        {
            await _logger.LogDebug(Constants.EnteredRemoveEmailFromMailingList);

            var mailingList = (await _mailingListRepository.GetAllEmailsInMailingListAsync()).ToList();
            await _logger.Log($"got {mailingList?.Count} emails from mailing list using _mailingListRepository.GetAllEmailsInMailingListAsync()");

            var existingEmail = mailingList?.FirstOrDefault(e => e.Email.Equals(email));
            if (existingEmail == null)
            {
                await _logger.Log(Constants.NoEmailInMailingListMatchingEmail);
                return Constants.NoEmailInMailingListMatchingEmail;
            }
            await _logger.Log(Constants.FoundExistingEmailInMailingList);

            var removeResult = await _mailingListRepository.RemoveEmailFromMailingListAsync(existingEmail);
            await _logger.Log($"got [{removeResult}] from _mailingListRepository.RemoveEmailFromMailingListAsync(...)");

            await _logger.LogDebug(Constants.ExitedRemoveEmailFromMailingList);
            return removeResult;
        }
    }
}