using Application.DTOs;
using Application.Services.Contents.BreathingContents.Models;
using Application.Services.Contents.MainContent;
using Application.Services.Users;
using Domain.Entities;
using Domain.Interfaces;
using System.Text.Json;



namespace Application.Services.Contents.BreathingContents
{
    public class BreathingContentService(IContentService contentService, IUserService userService, IBreathingContentRepository breathingContentRepository) : IBreathingContentService
    {
        public async Task CreateBreathingContentAsync(CreateBreathingContentRequest request)
        {
            var contentId = await contentService.CreateContentAsync(request.ContentRequest); //Create the main content first then get the ID

            var breathingContent = new BreathingContent
            {
                ContentId = contentId,
                StepCount = request.StepCount,
                Duration = request.Duration,
                Steps = request.Steps
            };
            await breathingContentRepository.AddAsync(breathingContent); // Save the breathing content
        }
        public async Task<BreathingContentDto> GetByContentIdAsync(Guid contentId)
        {
            var breathingContent = await breathingContentRepository.GetWithContentByIdAsync(contentId);

            if (breathingContent == null || breathingContent.Content == null)
            {
                throw new Exception($"Breathing content with ID {contentId} not found.");
            }

            return new BreathingContentDto
            {
                ContentId = breathingContent.ContentId,
                StepCount = breathingContent.StepCount,
                Duration = breathingContent.Duration,
                Steps = breathingContent.Steps,

                // Content tablosundan gelenler:
                Title = breathingContent.Content.Title,
                Description = breathingContent.Content.Description,
                ImagePath = breathingContent.Content.ImagePath,
                IsActive = breathingContent.Content.IsActive
            };
        }
        public async Task UpdateAsync(Guid contentId, UpdateBreathingContentRequest request)
        {
            var breathingContent = await breathingContentRepository.GetWithContentByIdAsync(contentId);

            if (breathingContent == null)
            {
                throw new Exception($"Breathing content with ID {contentId} not found.");
            }
            breathingContent.StepCount = request.StepCount;
            breathingContent.Duration = request.Duration;
            breathingContent.Steps = request.Steps;

            await breathingContentRepository.UpdateAsync(breathingContent);
            await contentService.UpdateAsync(contentId, request.ContentRequest);
        }
        public async Task DeleteAsync(Guid contentId)
        {
            await contentService.DeleteAsync(contentId); //Soft delete main content
        }
        public async Task<List<BreathingContentListDto>> GetAllAsync()
        {
            var favorites = await userService.GetUserFavorites();
            var contents = await breathingContentRepository.GetWithContentAsync();

            return contents.Select(b => new BreathingContentListDto
            {
                ContentId = b.ContentId,
                Title = b.Content.Title,
                Description = b.Content.Description,
                ImagePath = b.Content.ImagePath,
                StepCount = b.StepCount,
                DurationInSeconds = b.Duration,
                IsFavorite = favorites.Contains(b.ContentId)
            }).ToList();
        }
        public async Task<BreathingDetailDto> GetByIdAsync(Guid contentId)
        {
            var breathing = await breathingContentRepository
                .GetWithContentByIdAsync(contentId); // Include(Content) yapılmış hali

            if (breathing == null || breathing.Content == null)
                throw new Exception("İçerik bulunamadı");

            var steps = JsonSerializer.Deserialize<List<BreathingStepDto>>(
            breathing.Steps ?? "[]",
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return new BreathingDetailDto
            {
                ContentId = breathing.ContentId,
                Title = breathing.Content.Title,
                Description = breathing.Content.Description,
                StepCount = breathing.StepCount,
                DurationInSeconds = breathing.Duration,
                Steps = steps ?? new()
            };
        }
    }
}
