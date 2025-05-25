using Application.DTOs;

namespace Application.Services.Achievement
{
    public interface IAchievementService
    {
        Task<List<AchievementDto>> GetUserAchievementsAsync(Guid userId);
    }
}
