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

        /// <summary>
        /// gets a random Pragmatic Programmer Tip from the data repository using a random number.
        /// </summary>
        /// <returns>a random Pragmatic Programmer Tip</returns>
        public async Task<Tip> GetRandomTipAsync()
        {
            await _logger.LogDebug(Constants.EnteredGetRandomTip);
            var tips = (await GetAllTipsAsync()).ToList();
            await _logger.Log($"got {tips.Count} tips using GetAllTipsAsync()");

            var randomNumber = RandomNumberGenerator.GetInt32(tips.Count);
            await _logger.Log($"got {randomNumber} from RandomNumberGenerator.GetInt32(tips.Count)");

            await _logger.LogDebug(Constants.ExitedGetRandomTip);
            return tips[randomNumber];
        }

        /// <summary>
        /// gets a random Pragmatic Programmer Tip from the data repository using a random number.
        /// this method will also save the random tip and will not select it again until the entire list has been selected.
        /// </summary>
        /// <returns>a random Pragmatic Programmer Tip</returns>
        public async Task<Tip> GetRandomTipWithRemembranceAsync()
        {
            await _logger.LogDebug(Constants.EnteredGetRandomTipWithRemembrance);

            // get all the tips from the data source
            var tips = (await GetAllTipsAsync()).ToList();
            await _logger.Log($"got {tips.Count} tips using GetAllTipsAsync()");

            // get all previously selected tips from data source
            var tipsIdentifiersAlreadyRandomlySelected = (await _tipsRepository.ReadTipIdentifiersFromTextFile()).ToList();
            await _logger.Log($"got {tipsIdentifiersAlreadyRandomlySelected.Count} tips using _tipsRepository.ReadTipIdentifiersFromTextFile()");

            // if all tips have been previously selected, reset data source
            if (tipsIdentifiersAlreadyRandomlySelected.Count >= tips.Count)
            {
                await _tipsRepository.DeleteTipIdentifierTextFile();
                await _logger.LogDebug(Constants.ResetTipIdentifierRemembranceFile);
            }
            else // remove previously selected tips from random tip pool
            {
                var tipsToRemove = tips.Where(tip => tipsIdentifiersAlreadyRandomlySelected.Contains(tip.Number)).ToList();
                foreach (var tip in tipsToRemove)
                    tips.Remove(tip);
            }
            await _logger.Log($"removed {tipsIdentifiersAlreadyRandomlySelected.Count} tips from tip pool");

            // get random number using crypto!
            var randomNumber = RandomNumberGenerator.GetInt32(tips.Count);
            await _logger.Log($"got {randomNumber} from RandomNumberGenerator.GetInt32(tips.Count)");

            // get random tip
            var randomTip = tips[randomNumber];
            await _logger.LogDebug(Constants.GotRandomTipUsingRandomNumber);

            // store selected random tip for next time
            tipsIdentifiersAlreadyRandomlySelected.Add(randomTip.Number);
            await _tipsRepository.WriteTipIdentifiersToTextFile(tipsIdentifiersAlreadyRandomlySelected);
            await _logger.LogDebug(Constants.StoredTipIdentifierInRemembranceFile);

            await _logger.LogDebug(Constants.ExitedGetRandomTipWithRemembrance);
            return randomTip;
        }

        /// <summary>
        /// gets all Pragmatic Programmer Tips from the data repository.
        /// </summary>
        /// <returns>a list of Pragmatic Programmer Tips</returns>
        public async Task<IEnumerable<Tip>> GetAllTipsAsync()
        {
            await _logger.LogDebug(Constants.EnteredGetAllTips);

            var tips = (await _tipsRepository.ReadFromRawTipsJsonFile()).ToList();
            await _logger.Log($"read {tips.Count} tips using _tipsRepository.ReadFromRawTipsTextFile()");

            await _logger.LogDebug(Constants.ExitedGetAllTips);
            return tips;
        }
    }
}