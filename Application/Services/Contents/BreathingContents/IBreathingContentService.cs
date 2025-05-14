using Application.DTOs;
using Application.Services.Contents.BreathingContents.Models;

namespace Application.Services.Contents.BreathingContents
{
    public interface IBreathingContentService
    {
        Task CreateBreathingContentAsync(CreateBreathingContentRequest request);
        Task<BreathingContentDto> GetByContentIdAsync(Guid contentId);
        Task UpdateAsync(Guid contentId, UpdateBreathingContentRequest request);
        Task DeleteAsync(Guid contentId);
        Task<List<BreathingContentListDto>> GetAllAsync();
    }
}
