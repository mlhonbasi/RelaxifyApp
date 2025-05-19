using Application.DTOs;
using Application.Services.Contents.BreathingContents.Models;
using Application.Services.Contents.GameContent.Models;

namespace Application.Services.Contents.GameContent
{
    public interface IGameContentService
    {
        Task CreateGameContentAsync(CreateGameContentRequest request);
        Task<GameContentDto> GetByContentIdAsync(Guid contentId);
        Task UpdateAsync(Guid contentId, UpdateGameContentRequest request);
        Task DeleteAsync(Guid contentId);
        Task<List<GameContentListDto>> GetAllAsync();
        Task<GameContentDto> GetByIdAsync(Guid contentId);
    }
}
