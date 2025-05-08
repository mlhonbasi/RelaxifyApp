using Application.DTOs;
using Application.Services.Contents.MainContent;
using Application.Services.Contents.MeditationContents.Models;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services.Contents.MeditationContents
{
    public class MeditationContentService(IContentService contentService, IMeditationContentRepository meditationContentRepository) : IMeditationContentService
    {
        public async Task CreateMeditationContentAsync(CreateMeditationContentRequest request)
        {
            var contentId = await contentService.CreateContentAsync(request.ContentRequest); //Create the main content first then get the ID

            var meditationContent = new MeditationContent
            {
                ContentId = contentId,
                Steps = request.Steps
            };
            await meditationContentRepository.AddAsync(meditationContent); // Save the meditation content
        }
        public async Task<MeditationContentDto> GetByContentIdAsync(Guid contentId)
        {
            var meditationContent = await meditationContentRepository.GetByIdAsync(contentId);
            if (meditationContent == null)
            {
                throw new Exception($"Meditation content with ID {contentId} not found.");
            }
            return new MeditationContentDto
            {
                ContentId = meditationContent.ContentId,
                Steps = meditationContent.Steps
            };
        }
        public async Task UpdateAsync(Guid contentId, UpdateMeditationContentRequest request)
        {
            var meditationContent = await meditationContentRepository.GetByIdAsync(contentId);
            if (meditationContent == null)
            {
                throw new Exception($"Meditation content with ID {contentId} not found.");
            }
            meditationContent.Steps = request.Steps;
            await meditationContentRepository.UpdateAsync(meditationContent);
        }
        public async Task DeleteAsync(Guid contentId)
        {
            await contentService.DeleteAsync(contentId); //Soft delete main content   
        }
    }

}