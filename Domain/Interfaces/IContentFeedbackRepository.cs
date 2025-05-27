using Domain.Entities;
using Domain.Enums;
using Domain.Models.Queries;

namespace Domain.Interfaces
{
    public interface IContentFeedbackRepository : IRepository<ContentFeedbackLog>
    {
        Task<bool> HasUserGivenFeedbackAsync(Guid userId, Guid contentId);
        Task<ContentFeedbackSummaryDto> GetMusicFeedbackSummaryAsync(Guid userId, SummaryRange range);
        Task<MusicFeedbackDistributionDto> GetMusicFeedbackDistributionAsync(Guid userId);
        Task<List<MusicFeedbackDetailDto>> GetMusicFeedbackDetailsAsync(Guid userId, FeedbackType feedbackType);
    }
}
