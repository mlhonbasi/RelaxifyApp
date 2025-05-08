using Application.DTOs;
using Application.Services.Contents.BreathingContents.Models;
using Application.Services.Contents.MainContent;
using Domain.Entities;
using Domain.Interfaces;



namespace Application.Services.Contents.BreathingContents
{
    public class BreathingContentService(IContentService contentService, IBreathingContentRepository breathingContentRepository) : IBreathingContentService
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
            var breathingContent = await breathingContentRepository.GetByIdAsync(contentId);

            if (breathingContent == null)
            {
                throw new Exception($"Breathing content with ID {contentId} not found.");
            }

            return new BreathingContentDto
            {
                ContentId = breathingContent.ContentId,
                StepCount = breathingContent.StepCount,
                Duration = breathingContent.Duration,
                Steps = breathingContent.Steps
            };
        }
        public async Task UpdateAsync(Guid contentId, UpdateBreathingContentRequest request)
        {
            var breathingContent = await breathingContentRepository.GetByIdAsync(contentId);

            if (breathingContent == null)
            {
                throw new Exception($"Breathing content with ID {contentId} not found.");
            }
            breathingContent.StepCount = request.StepCount;
            breathingContent.Duration = request.Duration;
            breathingContent.Steps = request.Steps;

            await breathingContentRepository.UpdateAsync(breathingContent);
        }
        public async Task DeleteAsync(Guid contentId)
        {
            await contentService.DeleteAsync(contentId); //Soft delete main content
        }
    }
}
