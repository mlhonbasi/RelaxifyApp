namespace Application.Services.AI
{
    public interface IAiTrainingService
    {
        Task<bool> RetrainModelAsync();
        Task<bool> RetrainStressModelAsync();
    }
}
