using Application.Services.AI.Models;

namespace Application.Services.AI
{
    public interface IStressPredictionService
    {
        Task<float?> PredictStressAsync(StressPredictionInputModel input);
    }
}
