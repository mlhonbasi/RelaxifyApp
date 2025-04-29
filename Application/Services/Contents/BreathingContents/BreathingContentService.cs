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
    }
}
