using Application.Services.ContentFeedback.Models;
using Application.Services.Users;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models.Queries;

namespace Application.Services.ContentLogs
{
    public class ContentFeedbackService(IContentFeedbackRepository feedbackRepository,IUserContentLogRepository contentLogRepository ,IUserService userService) : IContentFeedbackService
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

            await feedbackRepository.AddAsync(feedback);
        }
        public async Task<ContentFeedbackSummaryDto> GetMusicSummaryAsync(SummaryRange range)
        {
            var userId = await userService.GetUserIdAsync();
            var summary = await feedbackRepository.GetMusicFeedbackSummaryAsync(userId, range);
            summary.LastPlayed = await contentLogRepository.GetLastPlayedMusicAsync(userId);
            return summary;
        }
        public async Task<MusicFeedbackDistributionDto> GetMusicFeedbackDistributionAsync()
        {
            var userId = await userService.GetUserIdAsync();
            return await feedbackRepository.GetMusicFeedbackDistributionAsync(userId);
        }

    }
}
