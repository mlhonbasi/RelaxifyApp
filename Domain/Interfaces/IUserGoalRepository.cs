using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IUserGoalRepository : IRepository<UserGoal>
    {
        Task<List<UserGoal>> GetGoalsForUserAsync(Guid userId);

        Task<UserGoal?> GetGoalByCategoryAsync(Guid userId, ContentCategory category);
    }
}
