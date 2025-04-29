using Application.Services.Contents.BreathingContents.Models;

namespace Application.Services.Contents.BreathingContents
{
    public interface IBreathingContentService
    {
        Task CreateBreathingContentAsync(CreateBreathingContentRequest request);
    }
}
