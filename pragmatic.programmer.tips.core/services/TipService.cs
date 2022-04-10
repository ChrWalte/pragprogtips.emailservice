using pragmatic.programmer.tips.core.data.interfaces;
using pragmatic.programmer.tips.core.models;
using pragmatic.programmer.tips.core.services.interfaces;
using System.Security.Cryptography;

namespace pragmatic.programmer.tips.core.services
{
    /// <summary>
    /// service used to perform logic on/with Pragmatic Programmer Tips.
    /// </summary>
    public class TipService : ITipService
    {
        private readonly IPragmaticProgrammerTipsRepository _tipsRepository;
        private readonly Logger _logger;

        public TipService(IPragmaticProgrammerTipsRepository tipsRepository, Logger logger)
        {
            _tipsRepository = tipsRepository;
            _logger = logger;
        }

        // TODO: add smart random where it keeps track of what tips have been used and what ones have not been used.
        /// <summary>
        /// TODO: Add summary...
        /// </summary>
        /// <returns>TODO: Add what it returns...</returns>
        /// <exception cref="NotImplementedException">because its not implemented</exception>
        public Task<Tip> GetRandomTipWithRemembranceAsync()
        {
            throw new NotImplementedException(Constants.TodoAddMethodForGettingRandomTipsFromRemembranceCache);
        }

        /// <summary>
        /// gets a random Pragmatic Programmer Tip from the data repository using a random number.
        /// </summary>
        /// <returns>a random Pragmatic Programmer Tip</returns>
        public async Task<Tip> GetRandomTipAsync()
        {
            await _logger.LogDebug("entered GetRandomTipAsync()");
            var tips = (await GetAllTipsAsync()).ToList();
            await _logger.Log($"got {tips.Count} tips using GetAllTipsAsync()");

            var randomNumber = RandomNumberGenerator.GetInt32(tips.Count);
            await _logger.Log($"got {randomNumber} from RandomNumberGenerator.GetInt32(tips.Count)");

            await _logger.LogDebug("exited GetRandomTipAsync()");
            return tips[randomNumber];
        }

        /// <summary>
        /// gets all Pragmatic Programmer Tips from the data repository.
        /// </summary>
        /// <returns>a list of Pragmatic Programmer Tips</returns>
        public async Task<IEnumerable<Tip>> GetAllTipsAsync()
        {
            await _logger.LogDebug("entered GetAllTipsAsync()");

            var tips = (await _tipsRepository.ReadFromRawTipsTextFile()).ToList();
            await _logger.Log($"read {tips.Count} tips using _tipsRepository.ReadFromRawTipsTextFile()");

            await _logger.LogDebug("exited GetAllTipsAsync()");
            return tips;
        }
    }
}