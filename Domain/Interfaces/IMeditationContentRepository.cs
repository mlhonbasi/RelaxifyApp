using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMeditationContentRepository : IRepository<MeditationContent>
    {
        Task<IList<MeditationContent>> GetWithContentAsync();
        Task<MeditationContent?> GetWithContentByIdAsync(Guid id);
    }
}
