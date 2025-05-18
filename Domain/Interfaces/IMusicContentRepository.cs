using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMusicContentRepository : IRepository<MusicContent>
    {
        Task<IList<MusicContent>> GetWithContentAsync();
    }
}
