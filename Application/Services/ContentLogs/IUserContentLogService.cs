using Application.DTOs;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IUserContentLogService
    {
        Task LogUsageAsync(Guid userId, Guid contentId, ContentCategory category, int durationInSeconds);
        Task<List<CategoryUsageDto>> GetCategoryUsageAsync(Guid userId);

    }

}
