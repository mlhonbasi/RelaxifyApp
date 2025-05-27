using Application.Services.ContentFeedback.Models;
using Domain.Enums;
using Domain.Models.Queries;

namespace Application.Services.ContentLogs
{
    public interface IContentFeedbackService
    {
        Task CreateAsync(CreateFeedbackRequest request);
        Task<ContentFeedbackSummaryDto> GetMusicSummaryAsync(SummaryRange range);
    }
}
