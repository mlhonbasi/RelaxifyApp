using Application.DTOs;
using Application.Services.Contents.BreathingContents.Models;
using Application.Services.Contents.GameContent.Models;
using Application.Services.Contents.MainContent;
using Application.Services.Users;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services.Contents.GameContent
{
    public class GameContentService(IUserService userService, IContentService contentService, IGameContentRepository gameContentRepository) : IGameContentService
    {
        public async Task CreateGameContentAsync(CreateGameContentRequest request)
        {
            var contentId = await contentService.CreateContentAsync(request.ContentRequest);

            var gameContent = new Domain.Entities.GameContent
            {
                ContentId = contentId,
                KeyName = request.KeyName,
                Category = request.GameCategory,
            };
            await gameContentRepository.AddAsync(gameContent); // Save the breathing content
        }

        public async Task DeleteAsync(Guid contentId)
        {
            await contentService.DeleteAsync(contentId);
        }

        public async Task<List<GameContentListDto>> GetAllAsync()
        {
            var favorites = await userService.GetUserFavorites();
            var contents = await gameContentRepository.GetWithContentAsync();

            return contents.Select(x => new GameContentListDto
            {
                ContentId = x.ContentId,
                Title = x.Content.Title,
                Description = x.Content.Description,
                ImagePath = x.Content.ImagePath,
                IsFavorite = favorites.Contains(x.ContentId),
            }).ToList();
        }

        public async Task<GameContentDto> GetByContentIdAsync(Guid contentId)
        {
            var gameContent = await gameContentRepository.GetByIdAsync(contentId);

            if (gameContent == null)
            {
                throw new Exception($"Breathing content with ID {contentId} not found.");
            }

            return new GameContentDto
            {
                ContentId = gameContent.ContentId,
                KeyName = gameContent.KeyName,
            };
        }

        public async Task<GameContentDto> GetByIdAsync(Guid contentId)
        {
            var game = await gameContentRepository.GetWithContentByIdAsync(contentId);

            if (game == null || game.Content == null)
                throw new Exception("Oyun içeriği bulunamadı");
            
            return new GameContentDto
            {
                ContentId = game.ContentId,
                KeyName = game.KeyName,
                Title = game.Content.Title,
            };
        }

        public Task UpdateAsync(Guid contentId, UpdateGameContentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
