using Application.DTOs;
using Application.Services.Contents.MainContent;
using Application.Services.Contents.MeditationContents.Models;
using Application.Services.Users;
using Domain.Entities;
using Domain.Interfaces;
using System.Diagnostics;
using System.Text.Json;

namespace Application.Services.Contents.MeditationContents
{
    public class MeditationContentService(IContentService contentService, IUserService userService, IMeditationContentRepository meditationContentRepository) : IMeditationContentService
    {
        public async Task CreateMeditationContentAsync(CreateMeditationContentRequest request)
        {
            var contentId = await contentService.CreateContentAsync(request.ContentRequest); //Create the main content first then get the ID
            Debug.WriteLine("Gelen Purpose: " + request.Purpose);
            Debug.WriteLine("Enum sayısal değeri: " + (int)request.Purpose);


            var meditationContent = new MeditationContent
            {
                ContentId = contentId,
                Steps = request.Steps,
                Purpose = request.Purpose
            };
            await meditationContentRepository.AddAsync(meditationContent); // Save the meditation content
        }
        public async Task<MeditationContentDto> GetByContentIdAsync(Guid contentId)
        {
            var meditationContent = await meditationContentRepository.GetWithContentByIdAsync(contentId);
            if (meditationContent == null)
            {
                throw new Exception($"Meditation content with ID {contentId} not found.");
            }
            return new MeditationContentDto
            {
                ContentId = meditationContent.ContentId,
                Steps = meditationContent.Steps,

                Title = meditationContent.Content.Title,
                Description = meditationContent.Content.Description,
                ImagePath = meditationContent.Content.ImagePath,
                IsActive = meditationContent.Content.IsActive,
                Purpose = meditationContent.Purpose// Assuming Purpose is a property of Content    
            };
        }
        public async Task UpdateAsync(Guid contentId, UpdateMeditationContentRequest request)
        {
            var meditationContent = await meditationContentRepository.GetWithContentByIdAsync(contentId);
            if (meditationContent == null)
            {
                throw new Exception($"Meditation content with ID {contentId} not found.");
            }
            meditationContent.Steps = request.Steps;
            meditationContent.Purpose = request.ContentRequest.Purpose; // Assuming Purpose is a property of Content
            await meditationContentRepository.UpdateAsync(meditationContent);
            await contentService.UpdateAsync(contentId, request.ContentRequest);
        }
        public async Task DeleteAsync(Guid contentId)
        {
            await contentService.DeleteAsync(contentId); //Soft delete main content   
        }

        public async Task<List<MeditationContentListDto>> GetAllAsync()
        {
            var favorites = await userService.GetUserFavorites();
            var contents = await meditationContentRepository.GetWithContentAsync();

            return contents.Select(x => new MeditationContentListDto
            {
                ContentId = x.ContentId,
                Title = x.Content.Title,
                Description = x.Content.Description,
                ImagePath = x.Content.ImagePath,
                IsFavorite = favorites.Contains(x.ContentId),
            }).ToList();
        }
        public async Task<MeditationDetailDto> GetByIdAsync(Guid contentId)
        {
            var meditation = await meditationContentRepository
                .GetWithContentByIdAsync(contentId); // Include(Content) yapılmış hali

            if (meditation == null || meditation.Content == null)
                throw new Exception("Meditasyon içeriği bulunamadı");

            var steps = JsonSerializer.Deserialize<List<MeditationStepDto>>(meditation.Steps ?? "[]");

            return new MeditationDetailDto
            {
                ContentId = meditation.ContentId,
                Title = meditation.Content.Title,
                Description = meditation.Content.Description,
                ImagePath = meditation.Content.ImagePath,
                Steps = steps ?? new()
            };
        }
    }

}