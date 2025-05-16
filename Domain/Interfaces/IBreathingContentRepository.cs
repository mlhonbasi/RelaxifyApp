using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBreathingContentRepository : IRepository<BreathingContent>
    {
        Task<IList<BreathingContent>> GetWithContentAsync();
        Task<BreathingContent?> GetWithContentByIdAsync(Guid id);
    }
}
