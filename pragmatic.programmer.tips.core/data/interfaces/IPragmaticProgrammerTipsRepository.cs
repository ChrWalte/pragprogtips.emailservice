using pragmatic.programmer.tips.core.models;

namespace pragmatic.programmer.tips.core.data.interfaces
{
    public interface IPragmaticProgrammerTipsRepository
    {
        Task<IEnumerable<Tip>> ReadFromRawTipsTextFile();

        Task<IEnumerable<Tip>> ReadFromRawTipsJsonFile();
    }
}