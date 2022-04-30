using Microsoft.AspNetCore.Mvc;
using pragmatic.programmer.tips.core;
using pragmatic.programmer.tips.core.models;
using pragmatic.programmer.tips.core.services.interfaces;

namespace pragmatic.programmer.tips.api.Controllers
{
    /// <summary>
    /// API Controller for email mailing list
    /// </summary>
    [ApiController]
    [Route("mailing-list")]
    [Produces("application/json")]
    public class EmailListController : ControllerBase
    {
        private readonly IMailingListService _mailingListService;
        private readonly Logger _logger;

        public EmailListController(IMailingListService mailingListService, Logger logger)
        {
            _mailingListService = mailingListService;
            _logger = logger;
        }

        /// <summary>
        /// subscribe to the mailing list using the given email
        /// </summary>
        /// <param name="email">a valid email address</param>
        /// <param name="name">optional name to use along with email</param>
        /// <returns>subscription result</returns>
        [HttpGet("subscribe")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddEmailToPragmaticProgrammerTipEmailListAsync(string email, string? name = null)
        {
            await _logger.LogDebug("entered api.AddEmailToPragmaticProgrammerTipEmailListAsync(...)");
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    name = email[..email.IndexOf("@", StringComparison.Ordinal)];

                var emailAddress = new EmailAddress { Email = email, Name = name };
                var addResult = await _mailingListService.AddEmailToMailingListAsync(emailAddress);

                await _logger.LogDebug("exited api.AddEmailToPragmaticProgrammerTipEmailListAsync(...)");
                return Ok(addResult);
            }
            catch (Exception ex)
            {
                await _logger.LogError("api exception", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// unsubscribe from the mailing list using the given email
        /// </summary>
        /// <param name="email">a valid email address</param>
        /// <returns>subscription result</returns>
        [HttpGet("unsubscribe")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveEmailFromPragmaticProgrammerTipEmailListAsync(string email)
        {
            await _logger.LogDebug("entered api.RemoveEmailFromPragmaticProgrammerTipEmailListAsync(...)");
            try
            {
                var removeResult = await _mailingListService.RemoveEmailFromMailingListAsync(email);
                await _logger.LogDebug("exited api.RemoveEmailFromPragmaticProgrammerTipEmailListAsync(...)");
                return Ok(removeResult);
            }
            catch (Exception ex)
            {
                await _logger.LogError("api exception", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}