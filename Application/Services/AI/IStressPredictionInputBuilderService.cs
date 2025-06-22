using Application.Services.AI.Models;

namespace Application.Services.AI
{
    public interface IStressPredictionInputBuilderService
    {
        Task<StressPredictionInputModel> BuildInputAsync();
    }
}
