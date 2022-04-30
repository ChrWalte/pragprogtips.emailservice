using pragmatic.programmer.tips.core.models;

namespace pragmatic.programmer.tips.core.data.interfaces
{
    /// <summary>
    /// interface for the pragmatic programmer tips data repository.
    /// </summary>
    public interface IPragmaticProgrammerTipsRepository
    {
        Task<IEnumerable<Tip>> ReadFromRawTipsTextFile();

        Task<IEnumerable<Tip>> ReadFromRawTipsJsonFile();

        Task<IEnumerable<string>> ReadTipIdentifiersFromTextFile();

        Task WriteTipIdentifiersToTextFile(IEnumerable<string> identifiers);

        void DeleteTipIdentifierTextFile();
    }
}