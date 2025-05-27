using Application.Services.ContentFeedback.Models;
using Application.Services.Users;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models.Queries;

namespace Application.Services.ContentLogs
{
    public class ContentFeedbackService(IContentFeedbackRepository repository,IUserContentLogRepository contentLogRepository ,IUserService userService) : IContentFeedbackService
    {
        public async Task CreateAsync(CreateFeedbackRequest request)
        {
            var userId = await userService.GetUserIdAsync();

            var feedback = new ContentFeedbackLog
            {
                ContentId = request.ContentId,
                Duration = request.Duration,
                Feedback = request.Feedback,
                Category = (ContentCategory)request.Category,
                UserId = userId
            };

            await repository.AddAsync(feedback);
        }
        public async Task<ContentFeedbackSummaryDto> GetMusicSummaryAsync(SummaryRange range)
        {
            var userId = await userService.GetUserIdAsync();
            var summary = await repository.GetMusicFeedbackSummaryAsync(userId, range);
            summary.LastPlayed = await contentLogRepository.GetLastPlayedMusicAsync(userId);
            return summary;
        }
    }
}
