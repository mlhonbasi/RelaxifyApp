using Application.DTOs;
using Application.Services.ContentLogs.Models;
using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IUserContentLogService
    {
        Task LogUsageAsync(Guid userId, Guid contentId, ContentCategory category, int durationInSeconds);
        Task<List<CategoryUsageDto>> GetCategoryUsageAsync(Guid userId);
        Task<List<UserContentLog>> GetUserLogsAsync(Guid userId);
        Task LogFocusLossAsync(Guid userId, Guid contentId, int focusLoss, double rate);
        Task LogMeditationStepsAsync(Guid userId, LogMeditationStepsRequest request);

    }

}
