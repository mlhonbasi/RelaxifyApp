using Application.Services.ContentFeedback.Models;

namespace Application.Services.ContentLogs
{
    public interface IContentFeedbackService
    {
        Task CreateAsync(CreateFeedbackRequest request);
    }
}
