using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services.ContentLogs
{
    public class UserContentLogService(IUserContentLogRepository userContentLogRepository) : IUserContentLogService
    {
        public async Task<List<CategoryUsageDto>> GetCategoryUsageAsync(Guid userId)
        {
            var userLogs = await userContentLogRepository.GetCategoryUsageAsync(userId);

            return userLogs.GroupBy(x=> x.Category)
                .Select(g => new CategoryUsageDto
                {
                    Category = g.Key.ToString(),
                    TotalDuration = g.Sum(x => x.DurationInSeconds)
                })
                .ToList();
        }

        public async Task LogUsageAsync(Guid userId, Guid contentId, ContentCategory category, int durationInSeconds)
        {
            var log = new UserContentLog
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ContentId = contentId,
                Category = category,
                DurationInSeconds = durationInSeconds,
                CreatedAt = DateTime.UtcNow
            };
            await userContentLogRepository.AddAsync(log);
        }
        public async Task<List<UserContentLog>> GetUserLogsAsync(Guid userId)
        {
            return await userContentLogRepository.GetLogsByUserIdAsync(userId);
        }
    }
}
