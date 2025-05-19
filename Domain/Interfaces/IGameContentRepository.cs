using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IGameContentRepository : IRepository<GameContent>
    {
        Task<IList<GameContent>> GetWithContentAsync();
        Task<GameContent?> GetWithContentByIdAsync(Guid id);
    }
}
