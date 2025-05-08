using Application.DTOs;
using Application.Services.Contents.BreathingContents.Models;
using Application.Services.Contents.MeditationContents.Models;

namespace Application.Services.Contents.MeditationContents
{
    public interface IMeditationContentService
    {
        Task CreateMeditationContentAsync(CreateMeditationContentRequest request);
        Task<MeditationContentDto> GetByContentIdAsync(Guid contentId);
        Task UpdateAsync(Guid contentId, UpdateMeditationContentRequest request);
        Task DeleteAsync(Guid contentId);
    }
}
