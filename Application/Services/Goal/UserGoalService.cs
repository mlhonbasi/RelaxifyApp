using Application.Services.Goal.Models;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services.Goal
{
    public class UserGoalService(IUserGoalRepository userGoalRepository) : IUserGoalService
    {
        public async Task<List<UserGoal>> GetGoalsForUserAsync(Guid userId)
        {
            return await userGoalRepository.GetGoalsForUserAsync(userId);
        }

        public async Task<UserGoal?> GetGoalByCategoryAsync(Guid userId, ContentCategory category)
        {
            return await userGoalRepository.GetGoalByCategoryAsync(userId, category);
        }

        public async Task SetOrUpdateGoalAsync(Guid userId, SetUserGoalRequest request)
        {
            var existingGoal = await GetGoalByCategoryAsync(userId, request.Category);

            if (existingGoal != null)
            {
                existingGoal.TargetDays = request.TargetDays;
                existingGoal.TargetMinutes = request.TargetMinutes;
                await userGoalRepository.UpdateAsync(existingGoal);
            }
            else
            {
                var goal = new UserGoal
                {
                    UserId = userId,
                    Category = request.Category,
                    TargetDays = request.TargetDays,
                    TargetMinutes = request.TargetMinutes
                };
                await userGoalRepository.AddAsync(goal);
            }
        }

    }
}
