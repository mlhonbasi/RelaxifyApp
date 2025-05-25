using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserAchievementRepository : IRepository<UserAchievement>
    {
        Task<List<UserAchievement>> GetByUserIdAsync(Guid userId);
        Task<bool> ExistsAsync(Guid userId, string key);
    }
}
