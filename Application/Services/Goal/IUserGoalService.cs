using Application.Services.Goal.Models;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services.Goal
{
        public interface IUserGoalService
        {
            Task<List<UserGoal>> GetGoalsForUserAsync(Guid userId);

            Task<UserGoal?> GetGoalByCategoryAsync(Guid userId, ContentCategory category);

            Task SetOrUpdateGoalAsync(Guid userId, SetUserGoalRequest request);
        }
}
