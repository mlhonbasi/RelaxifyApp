using Application.DTOs;
using Application.Services.Contents.MainContent;
using Application.Services.Contents.MusicContents.Models;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services.Contents.MusicContents
{
    public class MusicContentService(IContentService contentService, IMusicContentRepository musicContentRepository) : IMusicContentService
    {
        public async Task CreateMusicContentAsync(CreateMusicContentRequest request)
        {
            var contentId = await contentService.CreateContentAsync(request.ContentRequest); //Create the main content first then get the ID
            var musicContent = new MusicContent
            {
                ContentId = contentId,
                Category = request.Category,
                FilePath = request.FilePath,
                Duration = request.Duration
            };
            await musicContentRepository.AddAsync(musicContent); // Save the music content
        }
        public async Task<MusicContentDto> GetByContentIdAsync(Guid contentId)
        {
            var musicContent = await musicContentRepository.GetByIdAsync(contentId);
            if (musicContent == null)
            {
                throw new Exception($"Music content with ID {contentId} not found.");
            }
            return new MusicContentDto
            {
                ContentId = musicContent.ContentId,
                Category = musicContent.Category,
                FilePath = musicContent.FilePath,
                Duration = musicContent.Duration
            };
        }
        public async Task UpdateAsync(Guid contentId, UpdateMusicContentRequest request)
        {
            var musicContent = await musicContentRepository.GetByIdAsync(contentId);
            if (musicContent == null)
            {
                throw new Exception($"Music content with ID {contentId} not found.");
            }
            musicContent.Duration = request.Duration;
            musicContent.FilePath = request.FilePath;
            musicContent.Category = request.Category;
            await musicContentRepository.UpdateAsync(musicContent);
        }
        public async Task DeleteAsync(Guid contentId)
        {
            await contentService.DeleteAsync(contentId); //Soft delete main content   
        }
    }
}
