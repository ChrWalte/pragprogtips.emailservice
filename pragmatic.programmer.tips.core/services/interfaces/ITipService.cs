using pragmatic.programmer.tips.core.models;

namespace pragmatic.programmer.tips.core.services.interfaces
{
    public interface ITipService
    {
        Task<Tip> GetRandomTipAsync();
        Task<IEnumerable<Tip>> GetAllTipsAsync();
    }
}
