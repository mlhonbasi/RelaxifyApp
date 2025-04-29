using Application.DTOs;
using Application.Services.Contents.MainContent.Models;
using Domain.Interfaces;
using Domain.Entities;

namespace Application.Services.Contents.MainContent
{
    public class ContentService(IContentRepository contentRepository) : IContentService
    {
        public async Task<Guid> CreateContentAsync(CreateContentRequest request)
        {
            var content = new Content
            {
                Title = request.Title,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                IsActive = request.IsActive,
                Category = request.Category,
                ImagePath = request.ImagePath,
            };
            await contentRepository.AddAsync(content);
            return content.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var content = await contentRepository.GetByIdAsync(id);
            if (content == null)
            {
                throw new Exception("Content not found");
            }
            content.IsActive = false; // Soft delete
            await contentRepository.UpdateAsync(content);
        }

        public async Task<List<ContentDto>> GetAllAsync()
        {
            var contents = await contentRepository.GetAllAsync();
            return contents.Select(content => new ContentDto
            {
                Id = content.Id,
                Title = content.Title,
                Description = content.Description,
                CreatedAt = content.CreatedAt,
                IsActive = content.IsActive,
                Category = content.Category,
                ImageUrl = content.ImagePath
            }).ToList();
        }

        public async Task<ContentDto> GetByIdAsync(Guid id)
        {
            var content = await contentRepository.GetByIdAsync(id);

            if (content == null)
            {
                throw new Exception("Content not found");
            }

            return new ContentDto
            {
                Id = content.Id,
                Title = content.Title,
                Description = content.Description,
                CreatedAt = content.CreatedAt,
                IsActive = content.IsActive,
                Category = content.Category,
                ImageUrl = content.ImagePath
            };
        }

        public async Task UpdateAsync(Guid id, UpdateContentRequest request)
        {
            var content = await contentRepository.GetByIdAsync(id);
            if (content == null)
            {
                throw new Exception("Content not found");
            }

            content.Title = request.Title;
            content.Description = request.Description;
            content.IsActive = request.IsActive;
            content.ImagePath = request.ImagePath;
            await contentRepository.UpdateAsync(content);
        }
    }
}
