using Application.DTOs;
using Application.Services.Contents.MainContent.Models;

namespace Application.Services.Contents.MainContent
{
    public interface IContentService
    {
        Task<Guid> CreateContentAsync(CreateContentRequest request);
        Task<ContentDto> GetByIdAsync(Guid id);
        Task<List<ContentDto>> GetAllAsync();
        Task UpdateAsync(Guid id, UpdateContentRequest request);
        Task DeleteAsync(Guid id);
        //Soft delete eklenebilir...
    }
}
