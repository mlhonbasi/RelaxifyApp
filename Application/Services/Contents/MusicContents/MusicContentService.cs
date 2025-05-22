using Application.DTOs;
using Application.Services.Contents.MainContent;
using Application.Services.Contents.MusicContents.Models;
using Application.Services.Users;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services.Contents.MusicContents
{
    public class MusicContentService(IContentService contentService, IUserService userService, IMusicContentRepository musicContentRepository) : IMusicContentService
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
            var musicContent = await musicContentRepository.GetWithContentByIdAsync(contentId);
            if (musicContent == null)
            {
                throw new Exception($"Music content with ID {contentId} not found.");
            }
            return new MusicContentDto
            {
                ContentId = musicContent.ContentId,
                Category = musicContent.Category,
                FilePath = musicContent.FilePath,
                Duration = musicContent.Duration,

                Title = musicContent.Content.Title,
                Description = musicContent.Content.Description,
                ImagePath = musicContent.Content.ImagePath,
                IsActive = musicContent.Content.IsActive
            };
        }
        public async Task UpdateAsync(Guid contentId, UpdateMusicContentRequest request)
        {
            var musicContent = await musicContentRepository.GetWithContentByIdAsync(contentId);
            if (musicContent == null)
            {
                throw new Exception($"Music content with ID {contentId} not found.");
            }
            musicContent.Duration = request.Duration;
            musicContent.FilePath = request.FilePath;
            musicContent.Category = request.Category;

            await musicContentRepository.UpdateAsync(musicContent);
            await contentService.UpdateAsync(contentId, request.ContentRequest);
        }
        public async Task DeleteAsync(Guid contentId)
        {
            await contentService.DeleteAsync(contentId); //Soft delete main content   
        }

        public async Task<List<MusicListDto>> GetAllAsync()
        {
            var favorites = await userService.GetUserFavorites();
            var contents = await musicContentRepository.GetWithContentAsync();

            return contents.Select(x => new MusicListDto
            {
                ContentId = x.ContentId,
                Title = x.Content.Title,
                ImagePath = x.Content.ImagePath,
                FilePath = x.FilePath,
                IsFavorite = favorites.Contains(x.ContentId),
            }).ToList();
        }
    }
}
