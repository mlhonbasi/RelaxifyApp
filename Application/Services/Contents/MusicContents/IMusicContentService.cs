using Application.DTOs;
using Application.Services.Contents.MusicContents.Models;

namespace Application.Services.Contents.MusicContents
{
    public interface IMusicContentService
    {
        Task CreateMusicContentAsync(CreateMusicContentRequest request);
        Task<MusicContentDto> GetByContentIdAsync(Guid contentId);
        Task UpdateAsync(Guid contentId, UpdateMusicContentRequest request);
        Task DeleteAsync(Guid contentId);
    }
}
